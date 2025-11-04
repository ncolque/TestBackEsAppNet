using Application.Dtos.Payments;
using Domain.Entities;
using Mapster;

namespace Application.Mappings
{
    public class PaymentMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Payment, PaymentResponseDto>()
                .Map(dest => dest.PaymentId, src => src.Id)
                .TwoWays();
        }

    }
}
