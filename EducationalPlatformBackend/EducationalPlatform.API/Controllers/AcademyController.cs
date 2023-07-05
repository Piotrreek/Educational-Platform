using EducationalPlatform.Application.Academy.University.CreateUniversity;
using EducationalPlatform.Application.Contracts.Academy.University;
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
    public async Task<IActionResult> CreateAcademy(CreateUniversityRequestDto createUniversityRequestDto)
    {
        var command = new CreateUniversityCommand(createUniversityRequestDto.UniversityName);
        var result = await _sender.Send(command);

        return result.Match<IActionResult>(
            _ => Ok(),
            badRequest => BadRequest(badRequest.Message)
        );
    }
}