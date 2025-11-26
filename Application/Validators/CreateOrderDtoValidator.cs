using Application.DTOs.OrderDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class CreateOrderDtoValidator : AbstractValidator<CreateOrderDto>
    {
        public CreateOrderDtoValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage("Geçerli bir kullanıcı ID'si zorunludur.");

            RuleFor(x => x.AddressId)
                .GreaterThan(0).WithMessage("Teslimat adresi seçilmelidir.");

            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("Sipariş en az bir ürün içermelidir.");

            RuleForEach(x => x.Items).SetValidator(new CreateOrderItemDtoValidator());
        }
    }
}
