using Ardalis.Specification;

namespace Core.TicketAggregate.Specifications;

public sealed class CommentWithAuthorSpec : Specification<Comment>, ISingleResultSpecification
{
  public CommentWithAuthorSpec(Guid commentId)
  {
    Query
      .Include(comment => comment.Author)
      .Where(comment => comment.Id == commentId);
  }
}
