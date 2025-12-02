using FluentValidation;

namespace OtoMangaStore.Application.UseCases.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.ExternalUserId)
                .NotEmpty().WithMessage("El ID de usuario es obligatorio.");

            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("La orden debe contener al menos un ítem.");

            RuleForEach(x => x.Items).SetValidator(new OrderItemCommandValidator());
        }
    }

    public class OrderItemCommandValidator : AbstractValidator<OrderItemCommand>
    {
        public OrderItemCommandValidator()
        {
            RuleFor(x => x.MangaId)
                .GreaterThan(0).WithMessage("El ID del manga debe ser válido.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("La cantidad debe ser mayor a 0.");
        }
    }
}
