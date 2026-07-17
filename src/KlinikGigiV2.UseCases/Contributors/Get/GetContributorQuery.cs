using KlinikGigiV2.Core.ContributorAggregate;

namespace KlinikGigiV2.UseCases.Contributors.Get;

public record GetContributorQuery(ContributorId ContributorId) : IQuery<Result<ContributorDto>>;
