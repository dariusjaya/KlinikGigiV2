using KlinikGigiV2.UseCases.MedicalRecords.Delete;

namespace KlinikGigiV2.Web.MedicalRecords.Delete;

public class Delete(IMediator _mediator) : Endpoint<DeleteMedicalRecordRequest, DeleteMedicalRecordResponse>
{
    public override void Configure()
    {
        Delete(DeleteMedicalRecordRequest.Route);
    }

    public override async Task HandleAsync(DeleteMedicalRecordRequest req, CancellationToken ct)
    {
        var command = new DeleteMedicalRecordCommand(req.PatientId, req.MedicalRecordId);
        var result = await _mediator.Send(command, ct);

        if (result.IsError)
        {
            await Send.ResponseAsync(new DeleteMedicalRecordResponse
            {
                Message = result.FirstError.Description,
                Error = result.FirstError.Code
            }, 400, ct);
            return;
        }

        await Send.OkAsync(new DeleteMedicalRecordResponse
        {
            Message = "Rekam medis berhasil dihapus."
        }, ct);
    }
}