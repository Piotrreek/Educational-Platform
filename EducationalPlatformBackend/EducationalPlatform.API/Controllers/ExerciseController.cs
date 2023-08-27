using EducationalPlatform.Application.Abstractions.Services;
using EducationalPlatform.Application.Contracts.Exercise;
using EducationalPlatform.Application.Exercise.CreateExercise;
using EducationalPlatform.Application.Exercise.CreateExerciseSolution;
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

    [HttpPost("solution")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> CreateSolution([FromForm] CreateExerciseSolutionRequestDto request)
    {
        var command = new CreateExerciseSolutionCommand(request.SolutionFile, request.ExerciseId,
            _userContextService.UserId ?? Guid.Empty);
        var result = await _sender.Send(command);

        return result.Match<IActionResult>(
            ok => Ok(ok.Value),
            badRequest => BadRequest(badRequest.Message),
            _ => StatusCode(StatusCodes.Status503ServiceUnavailable)
        );
    }
}