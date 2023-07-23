using EducationalPlatform.API.Filters;
using EducationalPlatform.Application.Contracts.DidacticMaterial;
using EducationalPlatform.Application.DidacticMaterial.CreateDidacticMaterial;
using EducationalPlatform.Domain.Abstractions.Services;
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

    [HttpPost("create")]
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
}