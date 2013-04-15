using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using ServiceStack.FluentValidation;

using TheGateService.Database;
using TheGateService.Types;

namespace TheGateService.Validation {
    public class UserRegistrationValidator : AbstractValidator<UserRegister> {
        private static readonly UserDbProvider Users = new UserDbProvider();

        public UserRegistrationValidator() {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.");
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.");
            RuleFor(x => x.Email)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("\"{0}\" is not a valid email address.", x => x.Email)
                .Must((x, y) => !Users.UserExists(x.Email)).WithMessage("A user with that email address already exists.");
            RuleFor(x => x.Password)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Password is required.")
                .Length(6, int.MaxValue).WithMessage("Password must contain at least 6 characters.");
        }
    }
}
