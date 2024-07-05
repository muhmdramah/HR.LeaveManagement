using FluentValidation;

namespace HR.LeaveManagement.Application.DTOs.LeaveType.Validators
{
    public class CreateLeaveTypeDtoValidator : AbstractValidator<CreateLeaveTypeDto>
    {
        public CreateLeaveTypeDtoValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required!")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must be exceed 50 characters!");

            RuleFor(p => p.DefaultDays)
                .NotEmpty().WithMessage("{PropertyName} is required!")
                .GreaterThan(1).WithMessage("{PropertyName} must be at least 1!")
                .LessThan(1).WithMessage("{PropertyName} must be less than 100!");
        }
    }
}
