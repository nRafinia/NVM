using FluentValidation;

namespace Dashboard.Application.Extensions;

internal static class FluentValidatorExt
{
    public static IRuleBuilder<T, TClass> OneOfPropertiesMustNotEmpty<T, TClass>(this IRuleBuilder<T, TClass> ruleBuilder)
        where TClass : class
    {
        return ruleBuilder.Must(c => c.OneOfPropertiesMustHaveValue());
    }

    public static IRuleBuilder<T, TClass> AllPropertiesMustNotEmpty<T, TClass>(this IRuleBuilder<T, TClass> ruleBuilder)
        where TClass : class
    {
        return ruleBuilder.Must(c => c.AllPropertiesHaveValue());
    }
}