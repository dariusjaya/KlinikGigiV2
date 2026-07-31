using System.Security.Cryptography;
using System.Text;
using SimpleBase;

namespace KlinikGigiV2.Core;

public record PaginationProps(int Skip, int Take, int Page, int PageSize);
public static class Helper
{
  public static string ToSha256(this string rawData)
  {
    using (var sha256 = SHA256.Create())
    {
      var bytes = Encoding.UTF8.GetBytes(rawData);
      var hash = sha256.ComputeHash(bytes);

      var stringBuilder = new StringBuilder();
      foreach (var b in hash)
      {
        stringBuilder.Append(b.ToString("x2"));
      }

      return stringBuilder.ToString();
    }
  }

  public static string ToMd5(this string data)
  {
    var hash = MD5.HashData(Encoding.UTF8.GetBytes(data));
    return Convert.ToHexString(hash).ToLowerInvariant();
  }

  public static string ToBase58(this string data)
  {
    var bytes = Encoding.UTF8.GetBytes(data);
    return Base58.Bitcoin.Encode(bytes);
  }

  public static string ToBlake3(this string data)
  {
    var hash = Blake3.Hasher.Hash(Encoding.UTF8.GetBytes(data));
    return hash.ToString();
  }

  public static string FromBase58(this string data)
  {
    var bytes = Base58.Bitcoin.Decode(data);
    return Encoding.UTF8.GetString(bytes);
  }

  public static string ToHmac256(this string data, string key)
  {
    using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key));
    var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
    return Convert.ToHexString(hash).ToLowerInvariant();
  }

  public static string ToHmac512Base64(this string data, string key)
  {
    using var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(key));
    var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
    return Convert.ToBase64String(hash);
  }

  public static string ToHmac512Hex(this string data, string key)
  {
    using var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(key));
    var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
    return Convert.ToHexString(hash).ToLowerInvariant();
  }

  // Convert property names from camelCase to snake_case
  public static string ToSnakeCase(this string input)
  {
    if (string.IsNullOrEmpty(input))
    {
      return input;
    }

    var stringBuilder = new StringBuilder();
    for (int i = 0; i < input.Length; i++)
    {
      var c = input[i];
      if (char.IsUpper(c))
      {
        if (i > 0)
        {
          stringBuilder.Append('_');
        }
        stringBuilder.Append(char.ToLower(c));
      }
      else
      {
        stringBuilder.Append(c);
      }
    }
    return stringBuilder.ToString();
  }

  public static PaginationProps PaginationHelper(int maxPageSize, int? page, int? pageSize)
  {
    var currentPage = page ?? 1;
    currentPage = currentPage < 1 ? 1 : currentPage;

    var currentPageSize = pageSize ?? 10;
    currentPageSize = currentPageSize <= 0 ? 10 : currentPageSize;
    currentPageSize = currentPageSize > maxPageSize ? maxPageSize : currentPageSize;

    int skip = (currentPage - 1) * currentPageSize;
    int take = currentPageSize;

    return new PaginationProps(skip, take, currentPage, currentPageSize);
  }

  public static class ScopeContentTypeMapper
  {
    public static readonly Dictionary<string, List<string>> ContentTypeMappings = new(StringComparer.OrdinalIgnoreCase)
    {
      { "Avatar", ["image/jpg", "image/jpeg", "image/png", "image/gif", "image/webp", "image/bmp"] },
      { "Game", ["image/jpg", "image/jpeg", "image/png", "image/webp"] },
      { "Product", ["image/jpg", "image/jpeg", "image/png", "image/webp"] },
      { "AccountLogo", ["image/jpg", "image/jpeg", "image/png", "image/svg", "image/svg+xml", "image/webp" ]},
      { "AppLogo", ["image/jpg", "image/jpeg", "image/png", "image/svg", "image/svg+xml", "image/webp" ]},
      { "Carousel", ["image/jpg", "image/jpeg", "image/png", "image/webp"] },
      { "PaymentMethodLogo", ["image/jpg", "image/jpeg", "image/png", "image/svg", "image/svg+xml", "image/webp" ]},
      { "BankLogo", ["image/jpg", "image/jpeg", "image/png", "image/svg", "image/svg+xml", "image/webp" ]},
      { "Documents", ["image/png", "image/jpg", "image/jpeg", "image/bmp", "image/tiff", "application/pdf", "image/heic", "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "application/msword"] }
    };

    public static List<string> GetContentTypes(string scope)
    {
      return ContentTypeMappings.TryGetValue(scope, out var contentTypes)
        ? contentTypes
        : new List<string>();
    }

    public static bool IsValidContentType(string scope, string contentType)
    {
      if (!ContentTypeMappings.Keys
            .Any(key => key.Equals(scope, StringComparison.OrdinalIgnoreCase)))
      {
        return false; // Invalid scope
      }

      var allowedContentTypes = GetContentTypes(scope);
      return allowedContentTypes.Any(allowedType =>
        allowedType.Equals(contentType, StringComparison.OrdinalIgnoreCase));
    }
  }
}
