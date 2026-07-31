namespace KlinikGigiV2.Web.Users.List;

public class ListUserRequest
{
    public const string Route = "/klinik/users";

    [QueryParam, BindFrom("search")]
    public string? Search { get; set; }

    [QueryParam, BindFrom("page")]
    public int? Page { get; set; }

    [QueryParam, BindFrom("pagesize")]
    public int? PageSize { get; set; }


}
