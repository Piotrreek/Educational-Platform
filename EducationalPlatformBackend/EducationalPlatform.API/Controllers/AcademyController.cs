using EducationalPlatform.Application.Academy.Faculty.CreateFaculty;
using EducationalPlatform.Application.Academy.Subject;
using EducationalPlatform.Application.Academy.University.CreateUniversity;
using EducationalPlatform.Application.Contracts.Academy.Faculty;
using EducationalPlatform.Application.Contracts.Academy.University;
using EducationalPlatform.Application.Contracts.Academy.UniversitySubject;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducationalPlatform.API.Controllers;

[ApiController]
[Route("academy")]
public class AcademyController : ControllerBase
{
    private readonly ISender _sender;

    public AcademyController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("university")]
    [Authorize(Roles = "Administrator,Employee", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> CreateAcademy(CreateUniversityRequestDto request)
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
    public async Task<IActionResult> CreateFaculty(CreateFacultyRequestDto request)
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
    public async Task<IActionResult> CreateUniversitySubject(CreateUniversitySubjectRequestDto request)
    {
        var command = new CreateUniversitySubjectCommand(request.SubjectName, request.SubjectDegree, request.FacultyId,
            request.UniversityId);
        var result = await _sender.Send(command);

        return result.Match<IActionResult>(
            _ => Ok(),
            badRequest => BadRequest(badRequest.Message)
        );
    }
}