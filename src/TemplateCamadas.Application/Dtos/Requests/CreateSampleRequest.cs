namespace TemplateCamadas.Application.Dtos.Requests;

public class CreateSampleRequest
{
    public string Name { get; set; } = string.Empty;
    public bool Active { get; set; }
}
