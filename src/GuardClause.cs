namespace Philiprehberger.FluentGuard;

/// <summary>
/// Core fluent guard clause chain for validating a value of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of the value being validated.</typeparam>
public class GuardClause<T>
{
    private readonly T _value;
    private readonly string _paramName;

    /// <summary>
    /// Gets the name of the parameter being validated.
    /// </summary>
    public string ParamName => _paramName;

    /// <summary>
    /// Initializes a new instance of the <see cref="GuardClause{T}"/> class.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="paramName">The name of the parameter being validated.</param>
    internal GuardClause(T value, string paramName)
    {
        _value = value;
        _paramName = paramName;
    }

    /// <summary>
    /// Gets the validated value.
    /// </summary>
    public T Value => _value;

    /// <summary>
    /// Throws <see cref="ArgumentNullException"/> if the value is null.
    /// </summary>
    /// <returns>This instance for fluent chaining.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the value is null.</exception>
    public GuardClause<T> Null()
    {
        if (_value is null)
        {
            throw new ArgumentNullException(_paramName);
        }

        return this;
    }

    /// <summary>
    /// Throws <see cref="ArgumentException"/> if the value equals the default value for its type.
    /// </summary>
    /// <returns>This instance for fluent chaining.</returns>
    /// <exception cref="ArgumentException">Thrown when the value equals default(<typeparamref name="T"/>).</exception>
    public GuardClause<T> Default()
    {
        if (EqualityComparer<T>.Default.Equals(_value, default!))
        {
            throw new ArgumentException($"Value cannot be the default value for type {typeof(T).Name}.", _paramName);
        }

        return this;
    }

    /// <summary>
    /// Throws <see cref="ArgumentException"/> if the specified predicate returns true for the value.
    /// </summary>
    /// <param name="predicate">A function that returns true when the value is invalid.</param>
    /// <param name="message">The error message to include in the exception.</param>
    /// <returns>This instance for fluent chaining.</returns>
    /// <exception cref="ArgumentException">Thrown when the predicate returns true.</exception>
    public GuardClause<T> Condition(Func<T, bool> predicate, string message)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        if (predicate(_value))
        {
            throw new ArgumentException(message, _paramName);
        }

        return this;
    }

    /// <summary>
    /// Throws <see cref="ArgumentException"/> if the value is negative. Requires <typeparamref name="T"/> to implement <see cref="IComparable{T}"/>.
    /// </summary>
    /// <returns>This instance for fluent chaining.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is negative.</exception>
    /// <exception cref="InvalidOperationException">Thrown when <typeparamref name="T"/> does not implement <see cref="IComparable{T}"/>.</exception>
    public GuardClause<T> Negative()
    {
        EnsureComparable();
        var comparable = (IComparable<T>)_value!;

        if (comparable.CompareTo(default!) < 0)
        {
            throw new ArgumentOutOfRangeException(_paramName, _value, "Value cannot be negative.");
        }

        return this;
    }

    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> if the value is zero. Requires <typeparamref name="T"/> to implement <see cref="IComparable{T}"/>.
    /// </summary>
    /// <returns>This instance for fluent chaining.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is zero.</exception>
    /// <exception cref="InvalidOperationException">Thrown when <typeparamref name="T"/> does not implement <see cref="IComparable{T}"/>.</exception>
    public GuardClause<T> Zero()
    {
        EnsureComparable();
        var comparable = (IComparable<T>)_value!;

        if (comparable.CompareTo(default!) == 0)
        {
            throw new ArgumentOutOfRangeException(_paramName, _value, "Value cannot be zero.");
        }

        return this;
    }

    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> if the value is outside the specified range. Requires <typeparamref name="T"/> to implement <see cref="IComparable{T}"/>.
    /// </summary>
    /// <param name="min">The minimum allowed value (inclusive).</param>
    /// <param name="max">The maximum allowed value (inclusive).</param>
    /// <returns>This instance for fluent chaining.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is outside the specified range.</exception>
    /// <exception cref="InvalidOperationException">Thrown when <typeparamref name="T"/> does not implement <see cref="IComparable{T}"/>.</exception>
    public GuardClause<T> OutOfRange(T min, T max)
    {
        EnsureComparable();
        var comparable = (IComparable<T>)_value!;

        if (comparable.CompareTo(min) < 0 || comparable.CompareTo(max) > 0)
        {
            throw new ArgumentOutOfRangeException(_paramName, _value, $"Value must be between {min} and {max}.");
        }

        return this;
    }

    private void EnsureComparable()
    {
        if (_value is not IComparable<T>)
        {
            throw new InvalidOperationException(
                $"Type {typeof(T).Name} does not implement IComparable<{typeof(T).Name}>. " +
                "Numeric guard methods require a comparable type.");
        }
    }
}
