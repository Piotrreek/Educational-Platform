using EducationalPlatform.API.Filters;
using EducationalPlatform.Application.Authentication.LoginUser;
using EducationalPlatform.Application.Authentication.RegisterUser;
using EducationalPlatform.Application.Contracts.Authentication;
using EducationalPlatform.Domain.Abstractions.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserRequestDto loginUserRequestDto)
    {
        var command = MapLoginRequestDtoToLoginCommand(loginUserRequestDto, DateTimeOffset.Now);
        var result = await _sender.Send(command);

        return result.Match<IActionResult>(
            success => Ok(success.Value),
            invalidCredentials => BadRequest(invalidCredentials.Value)
        );
    }

    private static LoginUserCommand MapLoginRequestDtoToLoginCommand(LoginUserRequestDto loginUserRequestDto,
        DateTimeOffset dateTimeOffset)
    {
        return new LoginUserCommand(loginUserRequestDto.Email, loginUserRequestDto.Password, dateTimeOffset);
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