using EducationalPlatform.API.Filters;
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
        var response = await _sender.Send(command);

        return response.IsSuccess switch
        {
            false when response.StatusCode == StatusCodes.Status403Forbidden => StatusCode(
                StatusCodes.Status403Forbidden, response.Error),
            false when response.StatusCode == StatusCodes.Status400BadRequest => BadRequest(response.Error),
            _ => NoContent()
        };
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