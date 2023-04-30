using System.ComponentModel.DataAnnotations;
using Core.TicketAggregate;
using Core.TicketAggregate.Specifications;
using Kernel.Interfaces;
using MediatR;
using WebApi.Endpoints.TicketEndpoints.V1;

namespace WebApi.Handlers.TicketHandlers;

public class GetByIdHandler : IRequestHandler<GetTicketByIdRequest, GetTicketByIdResponse>
{
  private readonly IRepository<Ticket> _repository;

  public GetByIdHandler(IRepository<Ticket> repository) => _repository = repository;
  public async Task<GetTicketByIdResponse> Handle(GetTicketByIdRequest request, CancellationToken ct)
  {
    var spec = new TicketByIdWithCommentsSpec(request.TicketId);
    var entity = await _repository.FirstOrDefaultAsync(spec, ct);
    if (entity == null)
    {
      //todo move to validator
      throw new ValidationException();
      // return NotFound();
    }

    var response = new GetTicketByIdResponse
    (
      id: entity.Id,
      subject: entity.Subject,
      description: entity.Description,
      comments: entity.Comments.Select(
          item => new CommentRecord(item.Id, 
            item.CommentText,
            item.UserId))
        .ToList()
    );

    return response;
  }
}
