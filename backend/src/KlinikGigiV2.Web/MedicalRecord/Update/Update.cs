using KlinikGigiV2.Core.MedicalRecordAggregate;
using KlinikGigiV2.UseCases.MedicalRecords.Update;

namespace KlinikGigiV2.Web.MedicalRecords.Update;

public class Update(IMediator _mediator) : Endpoint<UpdateMedicalRecordRequest, UpdateMedicalRecordResponse>
{
    public override void Configure()
    {
        Put(UpdateMedicalRecordRequest.Route);
    }

    public override async Task HandleAsync(UpdateMedicalRecordRequest req, CancellationToken ct)
    {
        var command = new UpdateMedicalRecordCommand(
            PatientId: req.PatientId,
            MedicalRecordId: req.MedicalRecordId,
            VisitDate: req.VisitDate,
            Diagnosis: req.Diagnosis,
            Therapy: req.Therapy,
            Notes: req.Notes
        );

        var result = await _mediator.Send(command, ct);

        if (result.IsError)
        {
            await Send.ResponseAsync(new UpdateMedicalRecordResponse
            {
                Message = result.FirstError.Description,
                Error = result.FirstError.Code
            }, 400, ct);
            return;
        }

        await Send.OkAsync(new UpdateMedicalRecordResponse
        {
            MedicalRecord = MedicalRecordRecord.MapFromMedicalRecord(result.Value),
            Message = "Rekam medis berhasil diperbarui."
        }, ct);
    }
}