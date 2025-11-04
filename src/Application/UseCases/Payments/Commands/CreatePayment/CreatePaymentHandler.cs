using Application.Abstractions.Messaging;
using Application.Commons.Bases;
using Application.Constants;
using Application.Interfaces.Services;
using Domain.Entities;
using Mapster;

//using ClassPayment = Domain.Entities;  ClassPayment.Payment

namespace Application.UseCases.Payments.Commands.CreatePayment
{
    public class CreatePaymentHandler(IUnitOfWork unitOfWork, HandlerExecutor executor) : ICommandHandler<CreatePaymentCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly HandlerExecutor _executor = executor;

        public async Task<BaseResponse<bool>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            return await _executor.ExecuteAsync(
                request,
                () => CreateUserAsync(request, cancellationToken),
                cancellationToken
            );

            
        }

        private async Task<BaseResponse<bool>> CreateUserAsync(object request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var payment = request.Adapt<Payment>();
                await _unitOfWork.Payment.CreateAsync(payment, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                response.IsSuccess = true;
                response.Message = GlobalMessages.MESSAGE_SAVE;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
