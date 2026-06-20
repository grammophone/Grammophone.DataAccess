# Query Extensions And Translation

The `Grammophone.DataAccess.QueryExtensions` namespace provides portable query extensions over `IQueryable<T>`.

## Query Shaping

Supported query-shaping methods include:

```csharp
Include(...)
ThenInclude(...)
AsNoTracking()
```

Example:

```csharp
using Grammophone.DataAccess.QueryExtensions;

var album = await domainContainer.Albums
	.Include(a => a.Tracks)
	.ThenInclude(t => t.Genre)
	.AsNoTracking()
	.SingleAsync(a => a.Name == "Blue Integration");
```

`ThenInclude` is normalized to a string include path internally. For example:

```csharp
.Include(a => a.Tracks)
.ThenInclude(t => t.Genre)
```

becomes equivalent to:

```csharp
.Include("Tracks.Genre")
```

The abstraction deliberately supports complete eager loading of simple navigation paths. Filtered include is not part of the portable contract.

## Terminal Methods

Async terminal methods are exposed over ordinary `IQueryable<T>`:

```csharp
await query.ToListAsync();
await query.CountAsync();
await query.SingleOrDefaultAsync();
```

They delegate through `TerminalMethodsAdapter`. Provider implementations receive a native provider queryable and can directly call native async APIs.

## Query Functions

Portable query functions are available through `QueryFunctions`:

```csharp
var albums = await domainContainer.Albums
	.Where(a => QueryFunctions.Like(a.Name, "%Integration%"))
	.ToListAsync();
```

Date/time functions are also available:

```csharp
var comparisonDate = new DateTime(2024, 1, 4);

var albums = domainContainer.Albums
	.Where(a => QueryFunctions.DiffDays(a.ReleaseDate, comparisonDate) == 3)
	.ToList();
```

The functions have no in-memory implementation. They are intended to appear inside expression trees and be translated by a provider implementation.

## Set-Based Mutations

Portable set-based mutation methods are available for providers that support them:

```csharp
var deleted = await domainContainer.Tracks
	.Where(t => t.Album.Name == "Blue Integration")
	.ExecuteDeleteAsync();
```

`ExecuteDelete` and `ExecuteDeleteAsync` execute immediately in the database. They do not materialize the selected entities, do not mark individual entities as deleted through the change tracker and do not synchronize entities already tracked by the active domain container.

Portable set-based updates follow the EF Core setter-call style:

```csharp
var updated = await domainContainer.Tracks
	.Where(t => t.Album.Name == "Blue Integration")
	.ExecuteUpdateAsync(setters => setters
		.SetProperty(t => t.DurationSeconds, t => t.DurationSeconds + 5));
```

`ExecuteUpdate` and `ExecuteUpdateAsync` have the same set-based semantics. They bypass change tracking and do not update any already-loaded entity instances in memory.

Provider support differs. EF Core 7+ supports these operations directly. EF6 support is available through the optional `Grammophone.DataAccess.EntityFramework.Plus` integration.

## Translation Components

The base library provides reusable translation mechanics:

- `MethodMapping` describes a portable-to-native method mapping.
- `IsomorphicMethodMapping` maps calls where the native method has the same argument shape.
- `ExpressionMethodMapping` supports custom mapping logic.
- `MethodMappingExpressionVisitor` rewrites mapped scalar/query function calls.
- `IncludeChainNormalizerVisitor` turns portable include chains into string include paths.
- `TerminalMethodsAdapter` adapts async terminal execution.
- `ShapingMethodsAdapter` adapts executable non-terminal shaping operations such as `Include` and `AsNoTracking`.
- `SetOperationMethodsAdapter` adapts set-based mutation methods such as `ExecuteDelete` and `ExecuteUpdate`.
- `QueryOperations` prepares native queryables and translated predicate/selector expressions for adapters.

Provider implementers should not reimplement expression traversal or terminal dispatch. They should provide mappings and adapters.
