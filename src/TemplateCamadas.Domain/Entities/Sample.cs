using TemplateCamadas.Domain.Enums;

namespace TemplateCamadas.Domain.Entities;

public class Sample
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public SampleStatus Status { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
