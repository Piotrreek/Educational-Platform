using EducationalPlatform.Application.Abstractions.Services;
using EducationalPlatform.Application.Contracts;
using EducationalPlatform.Application.Contracts.Exercise;
using EducationalPlatform.Application.Exercise.CreateExercise;
using EducationalPlatform.Application.Exercise.CreateExerciseComment;
using EducationalPlatform.Application.Exercise.CreateExerciseRating;
using EducationalPlatform.Application.Exercise.CreateExerciseSolution;
using EducationalPlatform.Application.Exercise.CreateExerciseSolutionReview;
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

    [HttpPost("{id:guid}/solution")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> CreateSolution([FromForm] CreateExerciseSolutionRequestDto request,
        [FromRoute] Guid id)
    {
        var command = new CreateExerciseSolutionCommand(request.SolutionFile, id,
            _userContextService.UserId ?? Guid.Empty);
        var result = await _sender.Send(command);

        return result.Match<IActionResult>(
            ok => Ok(ok.Value),
            badRequest => BadRequest(badRequest.Message),
            _ => StatusCode(StatusCodes.Status503ServiceUnavailable)
        );
    }

    [HttpPost("{id:guid}/comment")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> CreateComment([FromBody] CreateExerciseCommentRequestDto request,
        [FromRoute] Guid id)
    {
        var command = new CreateExerciseCommentCommand(request.Comment, id,
            _userContextService.UserId ?? Guid.Empty);
        var result = await _sender.Send(command);

        return result.Match<IActionResult>(
            ok => Ok(ok.Value),
            badRequest => BadRequest(badRequest.Message)
        );
    }

    [HttpPost("{id:guid}/rate")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> RateExercise([FromBody] CreateRatingRequestDto request, [FromRoute] Guid id)
    {
        var command = new CreateExerciseRatingCommand(id, _userContextService.UserId ?? Guid.Empty,
            request.Rating);
        var result = await _sender.Send(command);

        return result.Match<IActionResult>(
            success => Ok(success.Value),
            badRequest => BadRequest(badRequest.Message)
        );
    }

    [HttpPost("solution/{id:guid}/review")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> CreateSolutionReview([FromForm] CreateExerciseSolutionReviewRequestDto request,
        [FromRoute] Guid id)
    {
        var command = new CreateExerciseSolutionReviewCommand(request.ReviewFile, request.ReviewContent,
            _userContextService.UserId ?? Guid.Empty, id);
        var result = await _sender.Send(command);

        return result.Match<IActionResult>(
            _ => Ok(),
            badRequest => BadRequest(badRequest.Message),
            _ => StatusCode(StatusCodes.Status503ServiceUnavailable)
        );
    }
}