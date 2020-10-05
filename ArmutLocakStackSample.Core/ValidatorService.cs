using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;

namespace ArmutLocalStackSample.Core
{
    public class ValidatorService : IValidatorService
    {
        public ValidatorService()
        {
        }

        public async Task ValidationCheck<TValidator, TModel>(TModel model)
            where TValidator : AbstractValidator<TModel>, new()
        {
            var validator = new TValidator();
            ValidationResult validationResult = await validator.ValidateAsync(model);

            var errorDetails = new List<ValidationFailure>();

            if (!validationResult.IsValid)
            {
                errorDetails.AddRange(validationResult.Errors);

                throw new ValidationException(errorDetails);
            }
        }
    }
}