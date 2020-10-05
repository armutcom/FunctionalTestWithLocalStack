using FluentValidation;
using System.Threading.Tasks;

namespace ArmutLocalStackSample.Core
{
    public interface IValidatorService
    {
        Task ValidationCheck<T, U>(U model) where T : AbstractValidator<U>, new();
    }
}