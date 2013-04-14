using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using ServiceStack.FluentValidation;
using ServiceStack.ServiceInterface;

using TheGateService.Types;

namespace TheGateService.Validation {
    public class ProductValidator : AbstractValidator<Product> {
        public ProductValidator() {
            RuleSet(ApplyTo.Put, () => {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.Description).NotEmpty();
                RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
                RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0);
            });
        }
    }
}
