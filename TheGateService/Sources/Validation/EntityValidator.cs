using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System;

using ServiceStack.FluentValidation;
using ServiceStack.ServiceInterface;

using TheGateService.Types;

namespace TheGateService.Validation {
    public class EntityValidator : AbstractValidator<Entity> {
        public EntityValidator() {
            RuleSet(ApplyTo.Get | ApplyTo.Put | ApplyTo.Delete,
                    () => RuleFor(x => x.Id).GreaterThanOrEqualTo(0));
        }
    }
}