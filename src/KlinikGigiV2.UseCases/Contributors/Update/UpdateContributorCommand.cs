using KlinikGigiV2.Core.ContributorAggregate;

namespace KlinikGigiV2.UseCases.Contributors.Update;

public record UpdateContributorCommand(ContributorId ContributorId, ContributorName NewName) : ICommand<Result<ContributorDto>>;
