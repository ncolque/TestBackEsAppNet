using Application.Behaviours;
using Application.Commons.Bases;
using Microsoft.Extensions.Logging;
using ValidationException = Application.Commons.Exceptions.ValidationException;

namespace Application.Abstractions.Messaging;

public class HandlerExecutor(IValidationService validationService, ILogger<HandlerExecutor> logger)
{
    private readonly IValidationService _validationService = validationService;
    private readonly ILogger<HandlerExecutor> _logger = logger;

    public async Task<BaseResponse<T>> ExecuteAsync<TRequest, T>
        (TRequest request, Func<Task<BaseResponse<T>>> action, CancellationToken cancellationToken)
    {
        try
        {
            await _validationService.ValidateAsync(request, cancellationToken);
            return await action();
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning("Validation failed for request {@Request}. Errors: {@Errors}", request, ex.Errors);

            return new BaseResponse<T>
            {
                IsSuccess = false,
                Message = "Errores de validación",
                Errors = ex.Errors
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred while executing handler for request {@Request}", request);

            return new BaseResponse<T>
            {
                IsSuccess = false,
                Message = "Ocurrió un error al procesar la solicitud",
                Errors =
                [
                    new() { PropertyName = "Exception", ErrorMessage = ex.Message }
                ]
            };
        }
    }
}