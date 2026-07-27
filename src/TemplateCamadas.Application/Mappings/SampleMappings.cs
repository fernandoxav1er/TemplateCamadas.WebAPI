using TemplateCamadas.Application.Dtos.Requests;
using TemplateCamadas.Application.Dtos.Responses;
using TemplateCamadas.Domain.Entities;
using TemplateCamadas.Domain.Enums;

namespace TemplateCamadas.Application.Mappings;

public static class SampleMappings
{
    public static Sample ToEntity(this CreateSampleRequest request) => new()
    {
        Id = Guid.NewGuid(),
        Name = request.Name,
        Status = request.Active ? SampleStatus.Active : SampleStatus.Inactive,
        CreatedAt = DateTimeOffset.UtcNow
    };

    public static SampleResponse ToResponse(this Sample entity) => new()
    {
        Id = entity.Id,
        Name = entity.Name,
        Status = entity.Status.ToString(),
        CreatedAt = entity.CreatedAt
    };
}
