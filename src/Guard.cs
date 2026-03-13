namespace Philiprehberger.FluentGuard;

/// <summary>
/// Static entry point for fluent guard clause validation.
/// </summary>
public static class Guard
{
    /// <summary>
    /// Begins a fluent guard clause chain for the specified value.
    /// </summary>
    /// <typeparam name="T">The type of the value to validate.</typeparam>
    /// <param name="value">The value to validate.</param>
    /// <param name="paramName">The name of the parameter being validated.</param>
    /// <returns>A <see cref="GuardClause{T}"/> for fluent chaining.</returns>
    public static GuardClause<T> Against<T>(T value, string paramName)
    {
        return new GuardClause<T>(value, paramName);
    }
}
