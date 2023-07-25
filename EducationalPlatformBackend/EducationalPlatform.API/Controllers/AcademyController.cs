using EducationalPlatform.API.Filters;
using EducationalPlatform.Application.Academy.Course;
using EducationalPlatform.Application.Academy.Faculty.CreateFaculty;
using EducationalPlatform.Application.Academy.GetGroupedAcademyEntities;
using EducationalPlatform.Application.Academy.Subject;
using EducationalPlatform.Application.Academy.University.CreateUniversity;
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

    public AcademyController(ISender sender)
    {
        _sender = sender;
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
}