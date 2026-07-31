namespace KlinikGigiV2.Core;

public interface IAuditableEntity
{
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
}
