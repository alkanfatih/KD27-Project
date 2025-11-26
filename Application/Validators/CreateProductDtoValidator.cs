using Application.DTOs.ProductDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace Application.Validators
{
    public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ürün adı boş olamaz!")
                .MaximumLength(100).WithMessage("Ürün adı en fazla 100 karakter olabilir!");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Ürün fiyatı sıfırdan büyük olmalıdır!");

            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0).WithMessage("Stok sayısı negatif olamaz!");

            RuleFor(x => x.Discount)
                .InclusiveBetween(0, 1).WithMessage("İnidirim oran 0 ile 1 arasında olmalıdır!");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Kategori seçimi zorunludur!");
        }
    }
}
