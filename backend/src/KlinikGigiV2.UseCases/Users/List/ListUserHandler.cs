using Ardalis.Specification;
using KlinikGigiV2.Core.UserAggregate;
using KlinikGigiV2.Core.UserAggregate.Specifications;
using KlinikGigiV2.Core.Models;
using KlinikGigiV2.Core;
namespace KlinikGigiV2.UseCases.Users.List;

public class ListUserHandler(IRepository<User> repository) : IQueryHandler<ListUserQuery, Result<PaginatedResponse<UserRecord>>>
{
  private sealed class UserListSpec : Specification<User>
  {
    public UserListSpec(string? search, int skip = 0, int take = 0)
    {

      if (!string.IsNullOrWhiteSpace(search))
      {
        var lowerCaseSearch = search.ToLower();

        Query
          .Search(user => user.FullName.ToLower(), $"%{lowerCaseSearch}%")
          .Search(user => user.Email!.ToLower(), $"%{lowerCaseSearch}%");
      }

      if (skip >= 0 && take > 0)
      {
        Query.OrderBy(user => user.CreatedAt).Skip(skip).Take(take);
      }
    }
  }
  public async ValueTask<Result<PaginatedResponse<UserRecord>>> Handle(ListUserQuery req, CancellationToken ct)
  {
    PaginationProps paginatedData = Helper.PaginationHelper(100, req.Page, req.PageSize);

    var userSpec = new UserListSpec(req.Search, paginatedData.Skip, paginatedData.Take);
    var result = await repository.ListAsync(userSpec, ct);

    var countSpec = new UserListSpec(req.Search);
    var count = await repository.CountAsync(countSpec, ct);

    var userRecords = result.Select(UserRecord.MapFromUser).ToList();

    PaginatedResponse<UserRecord> paginatedResponse = new()
    {
      Items = userRecords,
      TotalItems = count,
      Page = paginatedData.Page,
      PageSize = paginatedData.PageSize
    };

    return Result.Success(paginatedResponse, "User list fetched successfully.");
  }
}

