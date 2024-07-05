﻿using FluentValidation;

namespace HR.LeaveManagement.Application.DTOs.LeaveType.Validators
{
    public class ILeaveTypeDtoValidator : AbstractValidator<ILeaveTypeDto>
    {
        public ILeaveTypeDtoValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required!")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must be exceed {ComparisonValue} characters!");

            RuleFor(p => p.DefaultDays)
                .NotEmpty().WithMessage("{PropertyName} is required!")
                .GreaterThan(1).WithMessage("{PropertyName} must be at least 1!")
                .LessThan(1).WithMessage("{PropertyName} must be less than {ComparisonValue}!");
        }
    }
}