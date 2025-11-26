using Application.DTOs.ProductImageDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class CreateProductImageDtoValidator : AbstractValidator<CreateProductImageDto>
    {
        public CreateProductImageDtoValidator()
        {
            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("Görsel URL bilgisi boş olamaz.")
                .MaximumLength(255);

            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage("Geçerli bir ürün seçimi gereklidir.");
        }
    }
}
