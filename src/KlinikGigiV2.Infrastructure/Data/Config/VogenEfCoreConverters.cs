using KlinikGigiV2.Core.ContributorAggregate;
using Vogen;

namespace KlinikGigiV2.Infrastructure.Data.Config;

[EfCoreConverter<ContributorId>]
[EfCoreConverter<ContributorName>]
internal partial class VogenEfCoreConverters;
