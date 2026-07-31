using Ardalis.Result;

namespace KlinikGigiV2.Web.Models;

public record ErrorValidation(
  string Code,
  string Identifier,
  string ErrorMessage,
  ValidationSeverity Severity
)
{
  public static List<ErrorValidation> UpdateErrorList(Ardalis.Result.IResult result)
  {
    return result.ValidationErrors.Select(e => new ErrorValidation(
      Code: e.ErrorCode,
      Identifier: e.Identifier,
      ErrorMessage: e.ErrorMessage,
      Severity: e.Severity
      )
    ).ToList();
  }

  public static string GetErrorMessage(Ardalis.Result.IResult result)
  {
    if (result.Errors?.Any() == true)
    {
      return string.Join(", ", result.Errors);
    }

    if (result.ValidationErrors?.Any() == true)
    {
      return string.Join(", ", result.ValidationErrors.Select(e => e.ErrorMessage));
    }

    return "Unknown Error.";
  }
}
