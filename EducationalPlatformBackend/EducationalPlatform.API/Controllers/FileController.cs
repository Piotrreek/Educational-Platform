using EducationalPlatform.Application.DidacticMaterial.GetDidacticMaterialFile;
using EducationalPlatform.Application.Exercise.GetExerciseFile;
using EducationalPlatform.Application.Exercise.GetExerciseSolutionFile;
using EducationalPlatform.Application.Exercise.GetExerciseSolutionReviewFile;
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

    [HttpGet("material/{id:guid}")]
    public async Task<IActionResult> GetDidacticMaterialFile([FromRoute] Guid id)
    {
        var response = await _sender.Send(new GetDidacticMaterialFileQuery(id));

        return response.Match<IActionResult>(
            _ => NotFound(),
            blob => File(blob.Data, blob.ContentType, blob.FileName)
        );
    }

    [HttpGet("exercise/{id:guid}")]
    public async Task<IActionResult> GetExerciseFile([FromRoute] Guid id)
    {
        var response = await _sender.Send(new GetExerciseFileQuery(id));

        return response.Match<IActionResult>(
            _ => NotFound(),
            blob => File(blob.Data, blob.ContentType, blob.FileName)
        );
    }

    [HttpGet("exercise/solution/{id:guid}")]
    public async Task<IActionResult> GetExerciseSolutionFile([FromRoute] Guid id)
    {
        var response = await _sender.Send(new GetExerciseSolutionFileQuery(id));

        return response.Match<IActionResult>(
            _ => NotFound(),
            blob => File(blob.Data, blob.ContentType, blob.FileName)
        );
    }

    [HttpGet("exercise/solution/review/{id:guid}")]
    public async Task<IActionResult> GetExerciseSolutionReviewFile([FromRoute] Guid id)
    {
        var response = await _sender.Send(new GetExerciseSolutionReviewFileQuery(id));

        return response.Match<IActionResult>(
            ok => File(ok.Value.Data, ok.Value.ContentType, ok.Value.FileName),
            notFound => NotFound(),
            unAvailable => StatusCode(StatusCodes.Status503ServiceUnavailable)
        );
    }
}