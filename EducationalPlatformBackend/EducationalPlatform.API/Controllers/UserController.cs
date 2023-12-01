using EducationalPlatform.API.Filters;
using EducationalPlatform.Application.Abstractions.Services;
using EducationalPlatform.Application.Academy.AssignUser;
using EducationalPlatform.Application.Authentication.Commands.ChangePassword;
using EducationalPlatform.Application.Authentication.Commands.ConfirmAccount;
using EducationalPlatform.Application.Authentication.Commands.LoginUser;
using EducationalPlatform.Application.Authentication.Commands.RegisterUser;
using EducationalPlatform.Application.Authentication.Commands.ResetPassword;
using EducationalPlatform.Application.Authentication.Commands.SendAccountConfirmationLink;
using EducationalPlatform.Application.Authentication.Commands.SendResetPasswordLink;
using EducationalPlatform.Application.Authentication.Queries.GetUser;
using EducationalPlatform.Application.Configuration;
using EducationalPlatform.Application.Contracts.Academy.AssignUser;
using EducationalPlatform.Application.Contracts.Authentication;
using EducationalPlatform.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace EducationalPlatform.API.Controllers;

[ApiController]
[Route("user")]
[ServiceFilter(typeof(FormatBadRequestResponseFilter))]
public class UserController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IUserContextService _userContextService;
    private readonly JwtOptions _jwtOptions;

    public UserController(ISender sender, IUserContextService userContextService, IOptions<JwtOptions> options)
    {
        _sender = sender;
        _userContextService = userContextService;
        _jwtOptions = options.Value;
    }

    [HttpPost("register")]
    [ServiceFilter(typeof(DoNotAllowUserWithUserRole))]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequestDto registerUserRequestDto)
    {
        var command = MapRegisterRequestDtoToRegisterCommand(registerUserRequestDto, _userContextService.UserId);
        var result = await _sender.Send(command);

        return result.Match<IActionResult>(
            _ => NoContent(),
            _ => BadRequest("Ten e-mail jest już w użyciu przez innego użytkownika"),
            notAppropriateRole => BadRequest(notAppropriateRole.Message)
        );
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserRequestDto loginUserRequestDto)
    {
        var command = MapLoginRequestDtoToLoginCommand(loginUserRequestDto, DateTimeOffset.Now);
        var result = await _sender.Send(command);

        return result.Match<IActionResult>(
            success =>
            {
                var splitToken = success.Value.Token.Split(".");
                var cookieExpirationDate = DateTimeOffset.Now.AddHours(_jwtOptions.ExpireHours);

                Response.Cookies.Append(Keys.JwtHeader, splitToken[0], new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.None,
                    Secure = true,
                    Expires = cookieExpirationDate
                });
                Response.Cookies.Append(Keys.JwtSignature, splitToken[2], new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.None,
                    Secure = true,
                    Expires = cookieExpirationDate
                });

                return Ok(new LoginUserResponseDto(splitToken[1]));
            },
            invalidCredentials => Unauthorized(invalidCredentials.Value)
        );
    }

    [HttpPost("{userId:guid}/confirm")]
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

    [HttpPost("{userId:guid}/reset-password")]
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
    [HttpPatch("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestDto request)
    {
        var command = new ChangePasswordCommand(request.OldPassword, request.NewPassword, request.ConfirmNewPassword,
            _userContextService.UserId ?? Guid.Empty);
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
            request.FacultyId, request.UniversitySubjectId);
        var result = await _sender.Send(command);

        return result.Match<IActionResult>(
            _ => Ok(),
            badRequest => BadRequest(badRequest.Message)
        );
    }

    [HttpGet("verify")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public IActionResult VerifyJwt()
    {
        return Ok();
    }

    private static LoginUserCommand MapLoginRequestDtoToLoginCommand(LoginUserRequestDto loginUserRequestDto,
        DateTimeOffset dateTimeOffset)
    {
        return new LoginUserCommand(loginUserRequestDto.Email, loginUserRequestDto.Password, dateTimeOffset);
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> GetUser()
    {
        var userId = _userContextService.UserId;
        var result = await _sender.Send(new GetUserQuery(userId ?? Guid.Empty));

        return result.Match<IActionResult>(
            user => Ok(user),
            _ => NotFound()
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
            registerUserRequestDto.RequestedRoleName,
            userId
        );
    }
}