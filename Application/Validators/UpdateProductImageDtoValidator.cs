using Application.DTOs.ProductImageDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class UpdateProductImageDtoValidator : AbstractValidator<UpdateProductImageDto>
    {
        public UpdateProductImageDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Geçerli bir ID girilmelidir.");

            RuleFor(x => x.ImageUrl)
                .NotEmpty()
                .MaximumLength(255);

            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage("Geçerli bir ürün seçimi gereklidir.");
        }
    }
}
