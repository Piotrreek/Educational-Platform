using EducationalPlatform.Application.DidacticMaterial.GetDidacticMaterialFile;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EducationalPlatform.API.Controllers;

[Route("file")]
[ApiController]
public class FileController : ControllerBase
{
    private readonly ISender _sender;

    public FileController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("material/{id:Guid}")]
    public async Task<IActionResult> GetDidacticMaterialFile(Guid id)
    {
        var query = new GetDidacticMaterialFileQuery(id);
        var response = await _sender.Send(query);

        return response.Match<IActionResult>(
            _ => NotFound(),
            blob => File(blob.Data, blob.ContentType, blob.FileName)
        );
    }
}