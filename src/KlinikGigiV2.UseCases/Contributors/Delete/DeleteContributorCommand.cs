using KlinikGigiV2.Core.ContributorAggregate;

namespace KlinikGigiV2.UseCases.Contributors.Delete;

public record DeleteContributorCommand(ContributorId ContributorId) : ICommand<Result>;
