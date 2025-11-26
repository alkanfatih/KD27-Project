using Application.DTOs.ProductDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class UpdateProductDtoValidator : AbstractValidator<UpdateProductDto>
    {
        public UpdateProductDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Ürün ID geçersiz.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ürün adı boş olamaz.")
                .MaximumLength(100).WithMessage("Ürün adı en fazla 100 karakter olabilir.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Ürün fiyatı sıfırdan büyük olmalıdır.");

            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0).WithMessage("Stok sayısı negatif olamaz.");

            RuleFor(x => x.Discount)
                .InclusiveBetween(0, 1).WithMessage("İndirim oranı 0 ile 1 arasında olmalıdır.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Kategori seçimi zorunludur.");
        }
    }
}
