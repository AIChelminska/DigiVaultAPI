using DigiVaultAPI.Features.Admin.Messages.Commands;
using FluentValidation;

namespace DigiVaultAPI.Features.Admin.Validators.Commands;

public class CreateCmsContentValidator : AbstractValidator<CreateCmsContentCommand>
{
    public CreateCmsContentValidator()
    {
        RuleFor(x => x.Key)
            .NotEmpty().WithMessage("Key is required.")
            .MaximumLength(100).WithMessage("Key cannot exceed 100 characters.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title cannot exceed 200 characters.");

        RuleFor(x => x.Value)
            .NotEmpty().WithMessage("Value is required.");
    }
}

public class UpdateCmsContentValidator : AbstractValidator<UpdateCmsContentCommand>
{
    public UpdateCmsContentValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title cannot exceed 200 characters.");

        RuleFor(x => x.Value)
            .NotEmpty().WithMessage("Value is required.");
    }
}
