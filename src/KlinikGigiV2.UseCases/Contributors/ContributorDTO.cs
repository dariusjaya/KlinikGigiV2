using KlinikGigiV2.Core.ContributorAggregate;

namespace KlinikGigiV2.UseCases.Contributors;
public record ContributorDto(ContributorId Id, ContributorName Name, PhoneNumber PhoneNumber);
