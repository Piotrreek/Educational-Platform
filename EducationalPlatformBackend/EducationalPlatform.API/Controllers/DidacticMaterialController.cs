using EducationalPlatform.API.Filters;
using EducationalPlatform.Application.Abstractions.Services;
using EducationalPlatform.Application.Contracts;
using EducationalPlatform.Application.Contracts.DidacticMaterial;
using EducationalPlatform.Application.DidacticMaterial.CreateDidacticMaterial;
using EducationalPlatform.Application.DidacticMaterial.CreateDidacticMaterialOpinion;
using EducationalPlatform.Application.DidacticMaterial.CreateDidacticMaterialRating;
using EducationalPlatform.Application.DidacticMaterial.GetDidacticMaterial;
using EducationalPlatform.Application.DidacticMaterial.GetDidacticMaterialOpinions;
using EducationalPlatform.Application.DidacticMaterial.GetDidacticMaterials;
using EducationalPlatform.Application.DidacticMaterial.RemoveDidacticMaterialRating;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducationalPlatform.API.Controllers;

[ApiController]
[Route("didactic-material")]
[ServiceFilter(typeof(FormatBadRequestResponseFilter))]
public class DidacticMaterialController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IUserContextService _userContextService;

    public DidacticMaterialController(IUserContextService userContextService, ISender sender)
    {
        _userContextService = userContextService;
        _sender = sender;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> CreateDidacticMaterial([FromForm] CreateDidacticMaterialRequestDto request)
    {
        var userId = _userContextService.UserId;
        var command = new CreateDidacticMaterialCommand(
            (request.File is null ? request.Name : request.File.FileName) ?? string.Empty,
            request.DidacticMaterialType,
            request.UniversityCourseId, userId ?? Guid.Empty, request.Keywords, request.Description, request.Content,
            request.File);
        var result = await _sender.Send(command);

        return result.Match<IActionResult>(
            _ => Ok(),
            badRequest => BadRequest(badRequest.Message),
            _ => StatusCode(StatusCodes.Status503ServiceUnavailable)
        );
    }

    [HttpGet]
    public async Task<IActionResult> GetDidacticMaterials([FromQuery] GetDidacticMaterialsRequestDto request)
    {
        var query = new GetDidacticMaterialsQuery(request.UniversityId, request.FacultyId, request.UniversitySubjectId,
            request.UniversityCourseId);
        var result = await _sender.Send(query);

        return Ok(result);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetDidacticMaterial([FromRoute] Guid id)
    {
        var query = new GetDidacticMaterialQuery(id, _userContextService.UserId);
        var result = await _sender.Send(query);

        return result.Match<IActionResult>(
            material => Ok(material),
            _ => NotFound()
        );
    }

    [HttpPost("{id:guid}/rate")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> CreateDidacticMaterialRating(
        [FromBody] CreateRatingRequestDto request, [FromRoute] Guid id)
    {
        var userId = _userContextService.UserId;
        var command =
            new CreateDidacticMaterialRatingCommand(request.Rating, userId ?? Guid.Empty, id);
        var result = await _sender.Send(command);

        return result.Match<IActionResult>(
            ok => Ok(ok.Value),
            badRequest => BadRequest(badRequest.Message)
        );
    }

    [HttpDelete("{id:guid}/rate")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> DeleteDidacticMaterialRating([FromRoute] Guid id)
    {
        var userId = _userContextService.UserId;
        var command = new RemoveDidacticMaterialRatingCommand(userId ?? Guid.Empty, id);
        var result = await _sender.Send(command);

        return result.Match<IActionResult>(
            ok => Ok(ok.Value),
            badRequest => BadRequest(badRequest.Message)
        );
    }

    [HttpPost("{id:guid}/opinion")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> AddDidacticMaterialOpinion(
        [FromBody] CreateDidacticMaterialOpinionRequestDto request, [FromRoute] Guid id)
    {
        var command = new CreateDidacticMaterialOpinionCommand(id,
            _userContextService.UserId ?? Guid.Empty, request.Opinion);
        var result = await _sender.Send(command);

        return result.Match<IActionResult>(
            success => Ok(success.Value),
            badRequest => BadRequest(badRequest.Message)
        );
    }

    [HttpGet("{id:guid}/opinion")]
    public async Task<IActionResult> GetDidacticMaterialOpinions([FromRoute] Guid id)
    {
        var result = await _sender.Send(new GetDidacticMaterialOpinionsQuery(id));

        return result.Match<IActionResult>(
            ok => Ok(ok),
            badRequest => BadRequest(badRequest.Message)
        );
    }
}