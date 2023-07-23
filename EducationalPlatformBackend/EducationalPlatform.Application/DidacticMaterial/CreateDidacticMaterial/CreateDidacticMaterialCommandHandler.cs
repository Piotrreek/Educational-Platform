using EducationalPlatform.Domain.Abstractions.Repositories;
using EducationalPlatform.Domain.Abstractions.Services;
using EducationalPlatform.Domain.Enums;
using EducationalPlatform.Domain.ErrorMessages;
using EducationalPlatform.Domain.Results;
using MediatR;
using OneOf;
using OneOf.Types;

namespace EducationalPlatform.Application.DidacticMaterial.CreateDidacticMaterial;

public class CreateDidacticMaterialCommandHandler : IRequestHandler<CreateDidacticMaterialCommand,
    OneOf<Success, BadRequestResult, ServiceUnavailableResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IDidacticMaterialRepository _didacticMaterialRepository;
    private readonly IAcademyRepository _academyRepository;
    private readonly IAzureBlobStorageService _azureBlobStorageService;

    public CreateDidacticMaterialCommandHandler(IUserRepository userRepository,
        IDidacticMaterialRepository didacticMaterialRepository, IAcademyRepository academyRepository,
        IAzureBlobStorageService azureBlobStorageService)
    {
        _userRepository = userRepository;
        _didacticMaterialRepository = didacticMaterialRepository;
        _academyRepository = academyRepository;
        _azureBlobStorageService = azureBlobStorageService;
    }

    public async Task<OneOf<Success, BadRequestResult, ServiceUnavailableResult>> Handle(
        CreateDidacticMaterialCommand request,
        CancellationToken cancellationToken)
    {
        if (!(Enum.TryParse<DidacticMaterialType>(request.DidacticMaterialType, out var didacticMaterialType) &&
              Enum.IsDefined(didacticMaterialType)))
            return new BadRequestResult(GeneralErrorMessages.WrongDidacticMaterialTypeConversion);

        var userResult = await _userRepository.GetUserByIdAsync(request.AuthorId);
        if (userResult.IsT1)
            return new BadRequestResult(UserErrorMessages.UserWithIdNotExists);

        switch (didacticMaterialType)
        {
            case DidacticMaterialType.File when request.File is null:
                return new BadRequestResult(DidacticMaterialErrorMessages.FileCannotBeEmpty);
            case DidacticMaterialType.Text when request.Content is null:
                return new BadRequestResult(DidacticMaterialErrorMessages.ContentCannotBeEmpty);
        }

        var universityCourseResult = await _academyRepository.GetUniversityCourseByIdAsync(request.UniversityCourseId);
        if (!universityCourseResult.TryPickT0(out var universityCourse, out _))
            return new BadRequestResult(UniversityCourseErrorMessages.CourseWithIdNotExists);

        var didacticMaterial = new Domain.Entities.DidacticMaterial(request.Name, request.UniversityCourseId,
            request.AuthorId, didacticMaterialType, request.KeyWords, request.Description);

        if (didacticMaterialType == DidacticMaterialType.Text)
        {
            didacticMaterial.SetContent(request.Content!);
        }
        else
        {
            try
            {
                await _azureBlobStorageService.UploadBlobAsync(didacticMaterial.Id, request.File!);
            }
            catch (Exception)
            {
                return new ServiceUnavailableResult();
            }
        }

        universityCourse.AddDidacticMaterial(didacticMaterial);

        return new Success();
    }
}