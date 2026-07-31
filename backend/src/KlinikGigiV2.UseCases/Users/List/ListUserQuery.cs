using KlinikGigiV2.Core.Models;
using KlinikGigiV2.Core.UserAggregate;


namespace KlinikGigiV2.UseCases.Users.List;

public record ListUserQuery(string? Search, int? Page, int? PageSize) : IQuery<Result<PaginatedResponse<UserRecord>>>;
