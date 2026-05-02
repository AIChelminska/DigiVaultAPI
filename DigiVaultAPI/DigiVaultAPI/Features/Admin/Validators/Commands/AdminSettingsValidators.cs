using DigiVaultAPI.Features.Admin.Messages.Commands;
using FluentValidation;

namespace DigiVaultAPI.Features.Admin.Validators.Commands;

public class UpdateSettingsValidator : AbstractValidator<UpdateSettingsCommand>
{
    public UpdateSettingsValidator()
    {
        RuleFor(x => x.CommissionRate)
            .InclusiveBetween(0, 1).WithMessage("CommissionRate must be between 0 and 1.");
    }
}