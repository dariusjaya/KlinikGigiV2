using MinimalKlinikGigiV2.Web.Domain.ProductAggregate;

namespace MinimalKlinikGigiV2.Web.ProductFeatures;
public record ProductDto(ProductId Id, string Name, decimal UnitPrice);
