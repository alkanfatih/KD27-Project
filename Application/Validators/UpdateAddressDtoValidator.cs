using Application.DTOs.AdressDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class UpdateAddressDtoValidator : AbstractValidator<UpdateAddressDto>
    {
        public UpdateAddressDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Geçerli bir adres ID'si gereklidir.");

            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Line)
                .NotEmpty()
                .MaximumLength(255);

            RuleFor(x => x.City)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.District)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage("Geçerli bir kullanıcı ID'si gereklidir.");
        }
    }
}
