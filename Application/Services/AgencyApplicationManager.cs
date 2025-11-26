using Application.DTOs.CustomerDTOs;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.UnitOfWorks;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AgencyApplicationManager : IAgencyApplicationService
    {
        private readonly IUnitOfWork _repo;
        private readonly IMapper _mapper;
        private readonly UserManager<Customer> _userManager;

        public AgencyApplicationManager(IUnitOfWork repo, IMapper mapper, UserManager<Customer> userManager)
        {
            _repo = repo;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<List<AgencyApplicationDto>> GetPendingApplicationsAsync()
        {
            var apps = await _repo.AgencyApplicationRepository.FindConditionAsync(x => x.IsApproved == false); // sadece onaylanmamışları getir
            return _mapper.Map<List<AgencyApplicationDto>>(apps);
        }

        public async Task ApproveAsync(int id)
        {
            var app = await _repo.AgencyApplicationRepository.GetByIdAsync(id);
            if (app is null) throw new NotFoundException("Başvuru bulunamadı");

            app.IsApproved = true;
            _repo.AgencyApplicationRepository.Update(app);

            var user = await _userManager.FindByIdAsync(app.UserId.ToString());
            if (user is not null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, roles);
                await _userManager.AddToRoleAsync(user, "Agency");
            }
        }

        public async Task AddAsync(AgencyApplication agencyApplication)
        {
            await _repo.AgencyApplicationRepository.AddAsync(agencyApplication);
            await _repo.SaveChangesAscyn();
        }

        public async Task<MyAgencyApplicationDto?> GetMyApplicationAsync(int userId)
        {
            var app = await _repo.AgencyApplicationRepository.FirstOrDefaultAsync(x => x.UserId == userId);
            if (app is null) return null;

            return _mapper.Map<MyAgencyApplicationDto>(app);
        }
    }
}
