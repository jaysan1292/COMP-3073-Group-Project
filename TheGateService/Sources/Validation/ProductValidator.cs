using ServiceStack.FluentValidation;
using ServiceStack.ServiceInterface;

using TheGateService.Types;

namespace TheGateService.Validation {
    public class ProductValidator : AbstractValidator<Product>{
        public ProductValidator() {

            RuleSet(ApplyTo.Get | ApplyTo.Put | ApplyTo.Delete,
                    () => RuleFor(x => x.Id).GreaterThanOrEqualTo(0));
            RuleSet(ApplyTo.Post | ApplyTo.Put, () => {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.Description).NotEmpty();
                RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
                RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0);
            });
        }
    }
}