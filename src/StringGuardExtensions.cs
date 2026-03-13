using System.Text.RegularExpressions;

namespace Philiprehberger.FluentGuard;

/// <summary>
/// Extension methods for <see cref="GuardClause{T}"/> when T is <see cref="string"/>.
/// </summary>
public static partial class StringGuardExtensions
{
    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
    private static partial Regex EmailRegex();

    /// <summary>
    /// Throws <see cref="ArgumentNullException"/> if the string is null, or
    /// <see cref="ArgumentException"/> if it is empty.
    /// </summary>
    /// <param name="guard">The guard clause instance.</param>
    /// <returns>The guard clause instance for fluent chaining.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the value is null.</exception>
    /// <exception cref="ArgumentException">Thrown when the value is an empty string.</exception>
    public static GuardClause<string> NullOrEmpty(this GuardClause<string> guard)
    {
        if (guard.Value is null)
        {
            throw new ArgumentNullException(guard.ParamName);
        }

        if (guard.Value.Length == 0)
        {
            throw new ArgumentException("Value cannot be an empty string.", guard.ParamName);
        }

        return guard;
    }

    /// <summary>
    /// Throws <see cref="ArgumentNullException"/> if the string is null, or
    /// <see cref="ArgumentException"/> if it is empty or consists only of white-space characters.
    /// </summary>
    /// <param name="guard">The guard clause instance.</param>
    /// <returns>The guard clause instance for fluent chaining.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the value is null.</exception>
    /// <exception cref="ArgumentException">Thrown when the value is empty or white-space only.</exception>
    public static GuardClause<string> NullOrWhiteSpace(this GuardClause<string> guard)
    {
        if (guard.Value is null)
        {
            throw new ArgumentNullException(guard.ParamName);
        }

        if (string.IsNullOrWhiteSpace(guard.Value))
        {
            throw new ArgumentException(
                "Value cannot be empty or consist only of white-space characters.", guard.ParamName);
        }

        return guard;
    }

    /// <summary>
    /// Throws <see cref="ArgumentException"/> if the string length is less than the specified minimum.
    /// </summary>
    /// <param name="guard">The guard clause instance.</param>
    /// <param name="minLength">The minimum allowed length.</param>
    /// <returns>The guard clause instance for fluent chaining.</returns>
    /// <exception cref="ArgumentException">Thrown when the string is shorter than the minimum length.</exception>
    public static GuardClause<string> ShorterThan(this GuardClause<string> guard, int minLength)
    {
        if (guard.Value is not null && guard.Value.Length < minLength)
        {
            throw new ArgumentException(
                $"Value must be at least {minLength} characters long. Actual length: {guard.Value.Length}.",
                guard.ParamName);
        }

        return guard;
    }

    /// <summary>
    /// Throws <see cref="ArgumentException"/> if the string length exceeds the specified maximum.
    /// </summary>
    /// <param name="guard">The guard clause instance.</param>
    /// <param name="maxLength">The maximum allowed length.</param>
    /// <returns>The guard clause instance for fluent chaining.</returns>
    /// <exception cref="ArgumentException">Thrown when the string is longer than the maximum length.</exception>
    public static GuardClause<string> LongerThan(this GuardClause<string> guard, int maxLength)
    {
        if (guard.Value is not null && guard.Value.Length > maxLength)
        {
            throw new ArgumentException(
                $"Value must be no more than {maxLength} characters long. Actual length: {guard.Value.Length}.",
                guard.ParamName);
        }

        return guard;
    }

    /// <summary>
    /// Throws <see cref="ArgumentException"/> if the string is not a valid email address.
    /// </summary>
    /// <param name="guard">The guard clause instance.</param>
    /// <returns>The guard clause instance for fluent chaining.</returns>
    /// <exception cref="ArgumentException">Thrown when the value is not a valid email address.</exception>
    public static GuardClause<string> InvalidEmail(this GuardClause<string> guard)
    {
        if (guard.Value is not null && !EmailRegex().IsMatch(guard.Value))
        {
            throw new ArgumentException("Value is not a valid email address.", guard.ParamName);
        }

        return guard;
    }

    /// <summary>
    /// Throws <see cref="ArgumentException"/> if the string is not a valid absolute URI.
    /// </summary>
    /// <param name="guard">The guard clause instance.</param>
    /// <returns>The guard clause instance for fluent chaining.</returns>
    /// <exception cref="ArgumentException">Thrown when the value is not a valid URI.</exception>
    public static GuardClause<string> InvalidUri(this GuardClause<string> guard)
    {
        if (guard.Value is not null && !Uri.TryCreate(guard.Value, UriKind.Absolute, out _))
        {
            throw new ArgumentException("Value is not a valid URI.", guard.ParamName);
        }

        return guard;
    }
}
