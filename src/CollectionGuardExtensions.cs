namespace Philiprehberger.FluentGuard;

/// <summary>
/// Extension methods for <see cref="GuardClause{T}"/> when T is a collection type.
/// </summary>
public static class CollectionGuardExtensions
{
    /// <summary>
    /// Throws <see cref="ArgumentException"/> if the enumerable has no elements.
    /// </summary>
    /// <typeparam name="T">The element type of the enumerable.</typeparam>
    /// <param name="guard">The guard clause instance.</param>
    /// <returns>The guard clause instance for fluent chaining.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the value is null.</exception>
    /// <exception cref="ArgumentException">Thrown when the collection is empty.</exception>
    public static GuardClause<IEnumerable<T>> Empty<T>(this GuardClause<IEnumerable<T>> guard)
    {
        if (guard.Value is null)
        {
            throw new ArgumentNullException(guard.ParamName);
        }

        if (!guard.Value.Any())
        {
            throw new ArgumentException("Collection cannot be empty.", guard.ParamName);
        }

        return guard;
    }

    /// <summary>
    /// Throws <see cref="ArgumentException"/> if the collection has no elements.
    /// </summary>
    /// <typeparam name="T">The element type of the collection.</typeparam>
    /// <param name="guard">The guard clause instance.</param>
    /// <returns>The guard clause instance for fluent chaining.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the value is null.</exception>
    /// <exception cref="ArgumentException">Thrown when the collection is empty.</exception>
    public static GuardClause<ICollection<T>> Empty<T>(this GuardClause<ICollection<T>> guard)
    {
        if (guard.Value is null)
        {
            throw new ArgumentNullException(guard.ParamName);
        }

        if (guard.Value.Count == 0)
        {
            throw new ArgumentException("Collection cannot be empty.", guard.ParamName);
        }

        return guard;
    }

    /// <summary>
    /// Throws <see cref="ArgumentException"/> if the list has no elements.
    /// </summary>
    /// <typeparam name="T">The element type of the list.</typeparam>
    /// <param name="guard">The guard clause instance.</param>
    /// <returns>The guard clause instance for fluent chaining.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the value is null.</exception>
    /// <exception cref="ArgumentException">Thrown when the list is empty.</exception>
    public static GuardClause<IList<T>> Empty<T>(this GuardClause<IList<T>> guard)
    {
        if (guard.Value is null)
        {
            throw new ArgumentNullException(guard.ParamName);
        }

        if (guard.Value.Count == 0)
        {
            throw new ArgumentException("Collection cannot be empty.", guard.ParamName);
        }

        return guard;
    }

    /// <summary>
    /// Throws <see cref="ArgumentException"/> if the read-only collection has no elements.
    /// </summary>
    /// <typeparam name="T">The element type of the collection.</typeparam>
    /// <param name="guard">The guard clause instance.</param>
    /// <returns>The guard clause instance for fluent chaining.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the value is null.</exception>
    /// <exception cref="ArgumentException">Thrown when the collection is empty.</exception>
    public static GuardClause<IReadOnlyCollection<T>> Empty<T>(this GuardClause<IReadOnlyCollection<T>> guard)
    {
        if (guard.Value is null)
        {
            throw new ArgumentNullException(guard.ParamName);
        }

        if (guard.Value.Count == 0)
        {
            throw new ArgumentException("Collection cannot be empty.", guard.ParamName);
        }

        return guard;
    }
}
