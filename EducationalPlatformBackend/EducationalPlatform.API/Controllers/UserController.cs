using EducationalPlatform.API.Filters;
using EducationalPlatform.Application.Authentication.RegisterUser;
using EducationalPlatform.Application.Contracts.Authentication;
using EducationalPlatform.Domain.Abstractions.Services;
using EducationalPlatform.Domain.Results.AuthenticationResults;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OneOf;
using NoContentResult = EducationalPlatform.Domain.Results.NoContentResult;


namespace EducationalPlatform.API.Controllers;

[ApiController]
[Route("user")]
[AllowAnonymous]
public class UserController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IUserContextService _userContextService;

    public UserController(ISender sender, IUserContextService userContextService)
    {
        _sender = sender;
        _userContextService = userContextService;
    }

    [HttpPost("register")]
    [ServiceFilter(typeof(DoNotAllowUserWithUserRole))]
    public async Task<IActionResult> Register(RegisterUserRequestDto registerUserRequestDto)
    {
        var command = MapRegisterRequestDtoToRegisterCommand(registerUserRequestDto, _userContextService.UserId);
        var result = await _sender.Send(command);

        return result.Match<IActionResult>(
            _ => NoContent(),
            _ => BadRequest("This email is already used"),
            notAppropriateRole => BadRequest(notAppropriateRole.Message)
        );
    }

    private static RegisterUserCommand MapRegisterRequestDtoToRegisterCommand(
        RegisterUserRequestDto registerUserRequestDto,
        Guid? userId)
    {
        return new RegisterUserCommand(
            registerUserRequestDto.Username,
            registerUserRequestDto.Email,
            registerUserRequestDto.Password,
            registerUserRequestDto.ConfirmPassword,
            registerUserRequestDto.PhoneNumber,
            registerUserRequestDto.RequestedRoleName,
            userId
        );
    }
}