namespace KlinikGigiV2.Core.ContributorAggregate.Specifications;

public class ContributorByIdSpec : Specification<Contributor>
{
  public ContributorByIdSpec(ContributorId contributorId) =>
    Query
        .Where(contributor => contributor.Id == contributorId);
}
