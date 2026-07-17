using MinimalKlinikGigiV2.Web.Domain.CartAggregate;
using MinimalKlinikGigiV2.Web.Domain.GuestUserAggregate;
using MinimalKlinikGigiV2.Web.Domain.OrderAggregate;
using MinimalKlinikGigiV2.Web.Domain.ProductAggregate;
using Vogen;

namespace MinimalKlinikGigiV2.Web.Infrastructure.Data.Config;

[EfCoreConverter<ProductId>]
[EfCoreConverter<CartId>]
[EfCoreConverter<CartItemId>]
[EfCoreConverter<GuestUserId>]
[EfCoreConverter<OrderId>]
[EfCoreConverter<OrderItemId>]
[EfCoreConverter<Quantity>]
[EfCoreConverter<Price>]
internal partial class VogenEfCoreConverters;
