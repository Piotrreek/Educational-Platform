using EducationalPlatform.API.Filters;
using EducationalPlatform.Application.Abstractions.Services;
using EducationalPlatform.Application.Academy.AcademyEntityRequest.Commands.AcceptCreateAcademyEntityRequest;
using EducationalPlatform.Application.Academy.AcademyEntityRequest.Commands.CreateAcademyEntityRequest;
using EducationalPlatform.Application.Academy.AcademyEntityRequest.Commands.RejectCreateAcademyEntityRequest;
using EducationalPlatform.Application.Academy.AcademyEntityRequest.Queries.GetCreateAcademyEntityRequests;
using EducationalPlatform.Application.Academy.Course;
using EducationalPlatform.Application.Academy.Faculty.Commands.CreateFaculty;
using EducationalPlatform.Application.Academy.GetGroupedAcademyEntities;
using EducationalPlatform.Application.Academy.Subject;
using EducationalPlatform.Application.Academy.University.Commands.CreateUniversity;
using EducationalPlatform.Application.Contracts.Academy.AcademyEntityRequest;
using EducationalPlatform.Application.Contracts.Academy.Faculty;
using EducationalPlatform.Application.Contracts.Academy.University;
using EducationalPlatform.Application.Contracts.Academy.UniversityCourse;
using EducationalPlatform.Application.Contracts.Academy.UniversitySubject;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducationalPlatform.API.Controllers;

[ApiController]
[Route("academy")]
[ServiceFilter(typeof(FormatBadRequestResponseFilter))]
public class AcademyController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IUserContextService _userContextService;

    public AcademyController(ISender sender, IUserContextService userContextService)
    {
        _sender = sender;
        _userContextService = userContextService;
    }

    [HttpPost("university")]
    [Authorize(Roles = "Administrator,Employee", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> CreateAcademy([FromBody] CreateUniversityRequestDto request)
    {
        var command = new CreateUniversityCommand(request.UniversityName);
        var result = await _sender.Send(command);

        return result.Match<IActionResult>(
            _ => Ok(),
            badRequest => BadRequest(badRequest.Message)
        );
    }

    [HttpPost("faculty")]
    [Authorize(Roles = "Administrator,Employee", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> CreateFaculty([FromBody] CreateFacultyRequestDto request)
    {
        var command =
            new CreateFacultyCommand(request.FacultyName, request.UniversityId);
        var result = await _sender.Send(command);

        return result.Match<IActionResult>(
            _ => Ok(),
            badRequest => BadRequest(badRequest.Message)
        );
    }

    [HttpPost("subject")]
    [Authorize(Roles = "Administrator,Employee", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> CreateUniversitySubject([FromBody] CreateUniversitySubjectRequestDto request)
    {
        var command = new CreateUniversitySubjectCommand(request.SubjectName, request.SubjectDegree, request.FacultyId);
        var result = await _sender.Send(command);

        return result.Match<IActionResult>(
            _ => Ok(),
            badRequest => BadRequest(badRequest.Message)
        );
    }

    [HttpPost("course")]
    [Authorize(Roles = "Administrator,Employee", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> CreateUniversityCourse([FromBody] CreateUniversityCourseRequestDto request)
    {
        var command =
            new CreateUniversityCourseCommand(request.CourseName, request.CourseSession, request.UniversitySubjectId);
        var result = await _sender.Send(command);

        return result.Match<IActionResult>(
            _ => Ok(),
            badRequest => BadRequest(badRequest.Message)
        );
    }

    [HttpGet("grouped-entities")]
    public async Task<IActionResult> GetGroupedUniversityEntities()
    {
        var result = await _sender.Send(new GetGroupedAcademyEntitiesQuery());

        return Ok(result);
    }

    [HttpPost("request")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> CreateAcademyEntityRequest([FromBody] CreateAcademyEntityRequestDto request)
    {
        var command = new CreateAcademyEntityRequestCommand(request.EntityType, request.EntityName,
            request.AdditionalInformation, request.UniversitySubjectDegree, request.UniversityCourseSession,
            request.UniversityId, request.FacultyId, request.UniversitySubjectId,
            _userContextService.UserId ?? Guid.Empty);
        var result = await _sender.Send(command);

        return result.Match<IActionResult>(
            _ => Ok(),
            badRequest => BadRequest(badRequest.Message)
        );
    }

    [HttpGet("request")]
    [Authorize(Roles = "Administrator,Employee", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> GetAcademyRequests()
    {
        var result = await _sender.Send(new GetCreateAcademyEntityRequestsQuery());

        return Ok(result);
    }

    [HttpPost("request/{id:guid}/accept")]
    [Authorize(Roles = "Administrator,Employee", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> AcceptAcademyRequest([FromRoute] Guid id)
    {
        var result = await _sender.Send(new AcceptCreateAcademyEntityRequestCommand(id));

        return result.Match<IActionResult>(
            ok => Ok(ok.Value),
            _ => NotFound(),
            badRequest => BadRequest(badRequest.Message)
        );
    }

    [HttpPost("request/{id:guid}/reject")]
    [Authorize(Roles = "Administrator,Employee", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> RejectAcademyRequest([FromRoute] Guid id)
    {
        var result = await _sender.Send(new RejectCreateAcademyRequestCommand(id));

        return result.Match<IActionResult>(
            ok => Ok(ok.Value),
            _ => NotFound(),
            badRequest => BadRequest(badRequest.Message)
        );
    }
}