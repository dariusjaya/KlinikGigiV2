using KlinikGigiV2.Core.ContributorAggregate.Events;
using KlinikGigiV2.Core.Interfaces;

namespace KlinikGigiV2.Core.ContributorAggregate.Handlers;

public class ContributorDeletedHandler(ILogger<ContributorDeletedHandler> logger,
  IEmailSender emailSender) : INotificationHandler<ContributorDeletedEvent>
{
  public async ValueTask Handle(ContributorDeletedEvent domainEvent, CancellationToken cancellationToken)
  {
    logger.LogInformation("Handling Contributed Deleted event for {contributorId}", domainEvent.ContributorId);

    await emailSender.SendEmailAsync("to@test.com",
                                     "from@test.com",
                                     "Contributor Deleted",
                                     $"Contributor with id {domainEvent.ContributorId} was deleted.");
  }
}
