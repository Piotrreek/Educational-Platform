using EducationalPlatform.API.Filters;
using EducationalPlatform.Application.Academy.AssignUser;
using EducationalPlatform.Application.Authentication.ConfirmAccount;
using EducationalPlatform.Application.Authentication.LoginUser;
using EducationalPlatform.Application.Authentication.RegisterUser;
using EducationalPlatform.Application.Authentication.ResetPassword;
using EducationalPlatform.Application.Authentication.SendAccountConfirmationLink;
using EducationalPlatform.Application.Authentication.SendResetPasswordLink;
using EducationalPlatform.Application.Contracts.Academy.AssignUser;
using EducationalPlatform.Application.Contracts.Authentication;
using EducationalPlatform.Domain.Abstractions.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducationalPlatform.API.Controllers;

[ApiController]
[Route("user")]
[ServiceFilter(typeof(FormatBadRequestResponseFilter))]
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
    public async Task<IActionResult> Register([FromBody] RegisterUserRequestDto registerUserRequestDto)
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
    public async Task<IActionResult> Login([FromBody] LoginUserRequestDto loginUserRequestDto)
    {
        var command = MapLoginRequestDtoToLoginCommand(loginUserRequestDto, DateTimeOffset.Now);
        var result = await _sender.Send(command);

        return result.Match<IActionResult>(
            success => Ok(success.Value),
            invalidCredentials => BadRequest(invalidCredentials.Value)
        );
    }

    [HttpPost("confirm/{userId:guid}")]
    public async Task<IActionResult> Confirm([FromRoute] Guid userId, [FromQuery] string token)
    {
        var command = new ConfirmAccountCommand(userId, token);
        var result = await _sender.Send(command);

        return result.Match<IActionResult>(
            _ => Ok(),
            badRequest => BadRequest(badRequest.Message)
        );
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost("send-confirmation-link")]
    public async Task<IActionResult> SendConfirmationLink()
    {
        var email = _userContextService.Email;
        if (email is null)
            return Unauthorized();

        var command = new SendAccountConfirmationLinkCommand(_userContextService.Email!);
        var result = await _sender.Send(command);

        return result.Match<IActionResult>(
            _ => Ok(),
            _ => NotFound("User with this email was not found"),
            badRequest => BadRequest(badRequest.Message)
        );
    }

    [HttpPost("send-reset-password-link")]
    public async Task<IActionResult> SendResetPasswordLink(
        [FromBody] SendResetPasswordLinkRequestDto sendResetPasswordLinkRequestDto)
    {
        var command = new SendResetPasswordLinkCommand(sendResetPasswordLinkRequestDto.Email);
        var result = await _sender.Send(command);

        return result.Match<IActionResult>(
            _ => Ok(),
            _ => NotFound("User with this email was not found")
        );
    }

    [HttpPost("reset-password/{userId:guid}")]
    public async Task<IActionResult> ResetPassword([FromRoute] Guid userId, [FromQuery] string token,
        [FromBody] ResetPasswordRequestDto resetPasswordRequestDto)
    {
        var command = new ResetPasswordCommand(userId, token, resetPasswordRequestDto.Password,
            resetPasswordRequestDto.ConfirmPassword);
        var result = await _sender.Send(command);

        return result.Match<IActionResult>(
            _ => Ok(),
            badRequest => BadRequest(badRequest.Message)
        );
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost("assign-to-academy-entities")]
    public async Task<IActionResult> AssignToAcademyEntities(
        [FromBody] AssignUserToAcademyEntitiesRequestDto request)
    {
        var command = new AssignUserToAcademyEntitiesCommand(_userContextService.UserId, request.UniversityId,
            request.FacultyId, request.UniversitySubject);
        var result = await _sender.Send(command);

        return result.Match<IActionResult>(
            _ => Ok(),
            badRequest => BadRequest(badRequest.Message)
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