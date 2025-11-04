using Application.Abstractions.Messaging;
using Application.Commons.Bases;
using Application.Dtos.Payments;
using Application.Interfaces.Services;
using Mapster;

namespace Application.UseCases.Payments.Queries.GetPaymentsByCustomer
{
    public class GetPaymentsByCustomerHandler(IUnitOfWork unitOfWork, HandlerExecutor executor)
        : IQueryHandler<GetPaymentsByCustomerQuery, List<PaymentResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly HandlerExecutor _executor = executor;

        public async Task<BaseResponse<List<PaymentResponseDto>>> Handle(GetPaymentsByCustomerQuery query, CancellationToken cancellationToken)
        {

            return await _executor.ExecuteAsync(
                query,
                async () =>
                {
                    var response = new BaseResponse<List<PaymentResponseDto>>();

                    // Llamamos al método específico del repositorio
                    var payments = await _unitOfWork.Payment.GetAllPaymentsByCustomer(query.CustomerId, cancellationToken);

                    response.Data = payments.Adapt<List<PaymentResponseDto>>();
                    response.IsSuccess = true;
                    return response;
                },
                cancellationToken
            );

        }
}
}
