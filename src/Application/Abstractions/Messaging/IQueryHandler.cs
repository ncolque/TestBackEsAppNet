using Application.Commons.Bases;

namespace Application.Abstractions.Messaging
{
    public interface IQueryHandler<in Tquery, TResponse> where Tquery : IQuery<TResponse>
    {
        Task<BaseResponse<TResponse>> Handle(Tquery query, CancellationToken cancellationToken);
    }
}
