using KlinikGigiV2.Core.MedicalRecordAggregate;
using KlinikGigiV2.UseCases.MedicalRecords.Create;

namespace KlinikGigiV2.Web.MedicalRecords.Create;

public class Create(IMediator _mediator) : Endpoint<CreateMedicalRecordRequest, CreateMedicalRecordResponse>
{
    public override void Configure()
    {
        Post(CreateMedicalRecordRequest.Route);
    }

    public override async Task HandleAsync(CreateMedicalRecordRequest req, CancellationToken ct)
    {
        // Ambil UserId dari token JWT yang sedang login
        var userIdClaim = User.FindFirst("sub")?.Value
                          ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
        {
            await Send.ResponseAsync(new CreateMedicalRecordResponse
            {
                Message = "User tidak teridentifikasi.",
                Error = "UNAUTHORIZED"
            }, 401, ct);
            return;
        }

        var command = new CreateMedicalRecordCommand(
            PatientId: req.PatientId,
            CreatedByUserId: userId,   // ← otomatis dari token, bukan dari body request
            Diagnosis: req.Diagnosis,
            Therapy: req.Therapy,
            VisitDate: req.VisitDate,
            Notes: req.Notes
        );

        var result = await _mediator.Send(command, ct);

        if (result.IsError)
        {
            await Send.ResponseAsync(new CreateMedicalRecordResponse
            {
                Message = result.FirstError.Description,
                Error = result.FirstError.Code
            }, 400, ct);
            return;
        }

        await Send.OkAsync(new CreateMedicalRecordResponse
        {
            MedicalRecord = MedicalRecordRecord.MapFromMedicalRecord(result.Value),
            Message = "Rekam medis berhasil dibuat."
        }, ct);
    }
}