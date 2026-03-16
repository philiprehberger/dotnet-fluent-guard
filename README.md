# Philiprehberger.FluentGuard

[![CI](https://github.com/philiprehberger/dotnet-fluent-guard/actions/workflows/ci.yml/badge.svg)](https://github.com/philiprehberger/dotnet-fluent-guard/actions/workflows/ci.yml)
[![NuGet](https://img.shields.io/nuget/v/Philiprehberger.FluentGuard.svg)](https://www.nuget.org/packages/Philiprehberger.FluentGuard)
[![License](https://img.shields.io/github/license/philiprehberger/dotnet-fluent-guard)](LICENSE)

Fluent parameter validation and guard clauses for .NET.

## Install

```bash
dotnet add package Philiprehberger.FluentGuard
```

## Usage

```csharp
using Philiprehberger.FluentGuard;

public class UserService
{
    public void CreateUser(string name, string email, int age)
    {
        Guard.Against(name, nameof(name)).NullOrWhiteSpace().ShorterThan(2).LongerThan(100);
        Guard.Against(email, nameof(email)).NullOrEmpty().InvalidEmail();
        Guard.Against(age, nameof(age)).Negative().Zero().OutOfRange(1, 150);

        // All parameters validated — proceed safely
    }
}
```

### Null and default checks

```csharp
var config = Guard.Against(config, nameof(config)).Null().Value;
Guard.Against(status, nameof(status)).Default();
```

### Custom conditions

```csharp
Guard.Against(orderId, nameof(orderId))
    .Condition(id => id.StartsWith("INVALID"), "Order ID has an invalid prefix.");
```

### Collection checks

```csharp
Guard.Against(items, nameof(items)).Empty();
```

### Extracting the validated value

Every guard method returns the `GuardClause<T>` for chaining. Use the `.Value` property to retrieve the validated value:

```csharp
string validName = Guard.Against(name, nameof(name)).NullOrWhiteSpace().Value;
```

## API

### Core (`GuardClause<T>`)

| Method | Throws | Description |
|--------|--------|-------------|
| `Null()` | `ArgumentNullException` | Value is null |
| `Default()` | `ArgumentException` | Value equals `default(T)` |
| `Negative()` | `ArgumentOutOfRangeException` | Value is negative (requires `IComparable<T>`) |
| `Zero()` | `ArgumentOutOfRangeException` | Value is zero (requires `IComparable<T>`) |
| `OutOfRange(min, max)` | `ArgumentOutOfRangeException` | Value outside inclusive range |
| `Condition(predicate, msg)` | `ArgumentException` | Custom predicate returns true |

### String extensions

| Method | Throws | Description |
|--------|--------|-------------|
| `NullOrEmpty()` | `ArgumentNullException` / `ArgumentException` | Null or empty string |
| `NullOrWhiteSpace()` | `ArgumentNullException` / `ArgumentException` | Null, empty, or white-space only |
| `ShorterThan(min)` | `ArgumentException` | Length below minimum |
| `LongerThan(max)` | `ArgumentException` | Length above maximum |
| `InvalidEmail()` | `ArgumentException` | Fails basic email regex |
| `InvalidUri()` | `ArgumentException` | Fails `Uri.TryCreate` |

### Collection extensions

| Method | Throws | Description |
|--------|--------|-------------|
| `Empty()` | `ArgumentNullException` / `ArgumentException` | Null or has no elements |

## License

[MIT](LICENSE)
