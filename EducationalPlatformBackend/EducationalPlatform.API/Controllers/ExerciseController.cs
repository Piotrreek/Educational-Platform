using EducationalPlatform.Application.Abstractions.Services;
using EducationalPlatform.Application.Contracts;
using EducationalPlatform.Application.Contracts.Exercise;
using EducationalPlatform.Application.Exercise.CreateExercise;
using EducationalPlatform.Application.Exercise.CreateExerciseComment;
using EducationalPlatform.Application.Exercise.CreateExerciseRating;
using EducationalPlatform.Application.Exercise.CreateExerciseSolution;
using EducationalPlatform.Application.Exercise.CreateExerciseSolutionRating;
using EducationalPlatform.Application.Exercise.CreateExerciseSolutionReview;
using EducationalPlatform.Application.Exercise.GetExercise;
using EducationalPlatform.Application.Exercise.GetExerciseComments;
using EducationalPlatform.Application.Exercise.GetExercises;
using EducationalPlatform.Application.Exercise.GetExerciseSolutionRating;
using EducationalPlatform.Application.Exercise.GetExerciseSolutionReviews;
using EducationalPlatform.Application.Exercise.GetExerciseSolutions;
using EducationalPlatform.Application.Exercise.RemoveExerciseRating;
using EducationalPlatform.Application.Exercise.RemoveExerciseSolutionRating;
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

    [HttpGet]
    public async Task<IActionResult> GetExercises([FromQuery] string? exerciseName)
    {
        var result = await _sender.Send(new GetExercisesQuery(exerciseName));

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetExercise([FromRoute] Guid id)
    {
        var result = await _sender.Send(new GetExerciseQuery(id, _userContextService.UserId));

        return result.Match<IActionResult>(
            ok => Ok(ok.Value),
            _ => NotFound()
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
            _ => NoContent(),
            badRequest => BadRequest(badRequest.Message),
            _ => StatusCode(StatusCodes.Status503ServiceUnavailable)
        );
    }

    [HttpGet("{id:guid}/solution")]
    public async Task<IActionResult> GetSolutions([FromRoute] Guid id)
    {
        return Ok(await _sender.Send(new GetExerciseSolutionsQuery(id, _userContextService.UserId)));
    }

    [HttpPost("{id:guid}/opinion")]
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

    [HttpGet("{id:guid}/opinion")]
    public async Task<IActionResult> GetComments([FromRoute] Guid id)
    {
        var result = await _sender.Send(new GetExerciseCommentsQuery(id));

        return result.Match<IActionResult>(
            ok => Ok(ok),
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

    [HttpDelete("{id:guid}/rate")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> DeleteExerciseRate([FromRoute] Guid id)
    {
        var command = new RemoveExerciseRatingCommand(id, _userContextService.UserId ?? Guid.Empty);
        var result = await _sender.Send(command);

        return result.Match<IActionResult>(
            success => Ok(success.Value),
            badRequest => BadRequest(badRequest.Message)
        );
    }

    [HttpGet("solution/{id:guid}/review")]
    public async Task<IActionResult> GetSolutionReviews([FromRoute] Guid id)
    {
        var result = await _sender.Send(new GetExerciseSolutionReviewsQuery(id));

        return result.Match<IActionResult>(
            ok => Ok(ok),
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

    [HttpPost("solution/{id:guid}/rate")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> CreateSolutionRating([FromBody] CreateRatingRequestDto request,
        [FromRoute] Guid id)
    {
        var command =
            new CreateExerciseSolutionRatingCommand(request.Rating, _userContextService.UserId ?? Guid.Empty, id);
        var result = await _sender.Send(command);

        return result.Match<IActionResult>(
            _ => NoContent(),
            badRequest => BadRequest(badRequest.Message)
        );
    }

    [HttpGet("solution/{id:guid}/rate")]
    public async Task<IActionResult> GetSolutionRating([FromRoute] Guid id)
    {
        var result = await _sender.Send(new GetExerciseSolutionRatingQuery(id, _userContextService.UserId));

        return result.Match<IActionResult>(
            ok => Ok(ok),
            badRequest => BadRequest(badRequest.Message)
        );
    }

    [HttpDelete("solution/{id:guid}/rate")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> RemoveSolutionRating([FromRoute] Guid id)
    {
        var command =
            new RemoveExerciseSolutionRatingCommand(id, _userContextService.UserId ?? Guid.Empty);
        var result = await _sender.Send(command);

        return result.Match<IActionResult>(
            success => Ok(success.Value),
            badRequest => BadRequest(badRequest.Message)
        );
    }
}