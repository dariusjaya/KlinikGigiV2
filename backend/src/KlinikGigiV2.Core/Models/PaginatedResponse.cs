namespace KlinikGigiV2.Core.Models;

public class PaginatedResponse<T> : GenericResponse
{
  public List<T>? Items { get; set; }
  public int TotalItems { get; set; }
  public int Page { get; set; }
  public int PageSize { get; set; }
}
