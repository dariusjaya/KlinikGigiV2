namespace KlinikGigiV2.Core.Models;

public class GenericResponse
{
  public string Message { get; set; } = default!;
  public object Error { get; set; } = default!;
}
