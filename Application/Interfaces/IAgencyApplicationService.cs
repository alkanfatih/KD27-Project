using Application.DTOs.CustomerDTOs;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAgencyApplicationService
    {
        Task<List<AgencyApplicationDto>> GetPendingApplicationsAsync();
        Task ApproveAsync(int id);
        Task AddAsync(AgencyApplication agencyApplication);
        Task<MyAgencyApplicationDto?> GetMyApplicationAsync(int userId);
    }
}
