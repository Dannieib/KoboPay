using FluentValidation;

namespace KoboPay.Logic.Helpers
{
    internal class ValidationHelper<TValidatorClass, TRequestClass>
        where TValidatorClass : AbstractValidator<TRequestClass>, new()
        where TRequestClass : class, new()
    {
        public static async Task Validate(TRequestClass request)
        {
            // Validate
            var validate = await new TValidatorClass().ValidateAsync(request);
            if (!validate.IsValid)
                throw new ArgumentException(validate.ToString().Replace("\n", string.Empty));
        }
    }
}