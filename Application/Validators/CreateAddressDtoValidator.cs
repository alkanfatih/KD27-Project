using Application.DTOs.AdressDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class CreateAddressDtoValidator : AbstractValidator<CreateAddressDto>
    {
        public CreateAddressDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Adres başlığı boş olamaz.")
                .MaximumLength(50);

            RuleFor(x => x.Line)
                .NotEmpty().WithMessage("Adres içeriği boş olamaz.")
                .MaximumLength(255);

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("Şehir bilgisi zorunludur.")
                .MaximumLength(50);

            RuleFor(x => x.District)
                .NotEmpty().WithMessage("İlçe bilgisi zorunludur.")
                .MaximumLength(50);

            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage("Geçerli bir kullanıcı ID'si gereklidir.");
        }
    }
}
