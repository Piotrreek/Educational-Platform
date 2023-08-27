using EducationalPlatform.Application.Abstractions.Services;
using EducationalPlatform.Application.Contracts.Exercise;
using EducationalPlatform.Application.Exercise.CreateExercise;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducationalPlatform.API.Controllers;

[ApiController]
[Route("exercise")]
public class ExerciseController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IUserContextService _userContextService;

    public ExerciseController(ISender sender, IUserContextService userContextService)
    {
        _sender = sender;
        _userContextService = userContextService;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> CreateExercise([FromForm] CreateExerciseRequestDto request)
    {
        var command = new CreateExerciseCommand(request.Name, request.ExerciseFile, request.Description,
            _userContextService.UserId ?? Guid.Empty);
        var result = await _sender.Send(command);

        return result.Match<IActionResult>(
            _ => Ok(),
            badRequest => BadRequest(badRequest.Message),
            _ => StatusCode(StatusCodes.Status503ServiceUnavailable)
        );
    }
}