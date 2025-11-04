using FluentValidation;

namespace Application.UseCases.Payments.Commands.CreatePayment
{
    public class CreatePaymentValidator : AbstractValidator<CreatePaymentCommand>
    {
        public CreatePaymentValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("El cliente es obligatorio.");

            RuleFor(x => x.ServiceProvider)
                .NotEmpty().WithMessage("El proveedor de servicio es obligatorio.");

            RuleFor(x => x.Amount)
                .NotNull().WithMessage("El monto es obligatorio.")
                .GreaterThan(0).WithMessage("El monto debe ser mayor a 0 expresado en bolivianos (Bs) y en formato decimal válido. Ejemplo: 120.50.")
                .LessThanOrEqualTo(1500).WithMessage("No se permiten pagos mayores a 1500 Bs.").Must(BeBolivianos).WithMessage("Solo se aceptan montos expresados en bolivianos (Bs) y en formato decimal válido. Ejemplo: 120.50");
        }

        private bool BeBolivianos(decimal amount)
        {
            if (decimal.Round(amount, 2) != amount)
                return false;

            return true;
        }
    }
}
