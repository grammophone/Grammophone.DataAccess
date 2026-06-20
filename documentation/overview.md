# Grammophone.DataAccess

`Grammophone.DataAccess` is a provider-independent data access abstraction for .NET applications that use ORM-style unit-of-work systems. It defines contracts for domain containers, entity sets, query execution, change tracking, explicit loading, transactions, exception normalization, entity lifecycle listeners and portable query extensions.

The library is designed to let application and domain logic depend on domain-specific data access interfaces instead of concrete persistence frameworks such as Entity Framework 6, Entity Framework Core or NHibernate.

`Grammophone.DataAccess` is standalone. It can be used only as a data access abstraction layer, and it also serves as the data access foundation for higher-level Grammophone components.

## Core Idea

The central concept is the domain container:

```csharp
public interface IDomainContainer : IDisposable, IContextOwner
```

An `IDomainContainer` represents the active unit of work and repository root. It is analogous to an Entity Framework `DbContext` or an NHibernate `ISession`, but it exposes a provider-neutral contract.

Application code defines domain-specific containers by extending `IDomainContainer`:

```csharp
public interface IMusicDomainContainer : IDomainContainer
{
	IEntitySet<Artist> Artists { get; }
	IEntitySet<Album> Albums { get; }
	IEntitySet<Track> Tracks { get; }
	IEntitySet<Genre> Genres { get; }
}
```

Provider-specific packages implement that contract by adapting concrete ORM contexts.

## Entity Sets And Queries

`IEntitySet<T>` represents a set of entities:

```csharp
public interface IEntitySet<E> : IEntityQuery<E>
	where E : class
```

It supports common set operations:

- `Add`
- `AddRange`
- `Attach`
- `Create`
- `Create<T>`
- `Find`
- `Remove`
- `RemoveRange`

`IEntityQuery<T>` extends `IOrderedQueryable<T>`, allowing standard LINQ composition:

```csharp
var albums = domainContainer.Albums
	.Where(album => album.Name.Contains("Blue"))
	.OrderBy(album => album.Name)
	.ToList();
```

The ordered query contract is intentional. Standard LINQ ordering operators expect provider-created queryables to support `IOrderedQueryable<T>`.

## Entity Creation And Proxies

Entities should be created through the domain container or entity set:

```csharp
var album = domainContainer.Create<Album>();
```

or:

```csharp
var album = domainContainer.Albums.Create();
```

Provider implementations may return proxy instances when supported. This is important for proxy-based lazy loading and relationship behavior. For reliable proxy behavior, mapped entity properties should be virtual, including scalar properties, key properties, reference navigations and collection navigations.

Provider-specific rules may be stricter. For example, EF Core change-tracking proxies also require notification-capable collection navigations.

## Change Tracking And Entries

The abstraction exposes provider-neutral entity tracking through `IChangeTracker` and entry interfaces.

`IDomainContainer.ChangeTracker` provides access to tracked entities:

```csharp
var changedEntries = domainContainer.ChangeTracker.Entries(TrackingState.Modified);
```

`IDomainContainer.Entry(entity)` returns an `IEntityEntry<T>`:

```csharp
var entry = domainContainer.Entry(album);
entry.State = TrackingState.Modified;
```

`IEntityEntry<T>` exposes:

- `Entity`
- `State`
- `PropertiesByName`
- `Reload`
- `ReloadAsync`
- `Property`
- `ComplexProperty`
- `Reference`
- `Collection`

## Explicit Loading

Reference and collection entries abstract explicit loading:

```csharp
var tracksEntry = domainContainer.Entry(album)
	.Collection(a => a.Tracks);

if (!tracksEntry.IsLoaded)
{
	tracksEntry.Load();
}
```

Reference loading follows the same pattern:

```csharp
var artistEntry = domainContainer.Entry(album)
	.Reference(a => a.Artist);

await artistEntry.LoadAsync();
```

The `IsLoaded` property is the correct way to verify whether a relationship was explicitly or eagerly loaded. Enumerating a navigation collection may trigger lazy loading and is therefore not a reliable eager-loading assertion.

## Query Extensions

Portable query extensions are exposed from:

```csharp
Grammophone.DataAccess.QueryExtensions
```

They are normal `IQueryable<T>` extension methods, allowing code to remain LINQ-shaped.

### Eager Loading

The abstraction supports complete eager loading of simple navigation paths:

```csharp
using Grammophone.DataAccess.QueryExtensions;

var album = await domainContainer.Albums
	.Include(a => a.Tracks)
	.ThenInclude(t => t.Genre)
	.SingleAsync(a => a.Name == "Blue Integration");
```

String include paths are also supported:

```csharp
var album = domainContainer.Albums
	.Include("Tracks.Genre")
	.Single(a => a.Name == "Blue Integration");
```

Filtered include is intentionally outside the portable contract. If a query needs filtered child data, use projection rather than partially loading a relationship end.

### No Tracking

No-tracking queries are expressed portably:

```csharp
var album = await domainContainer.Albums
	.AsNoTracking()
	.SingleAsync(a => a.Name == "Blue Integration");
```

Provider implementations adapt this to their native no-tracking mechanism.

### Async Terminal Methods

The library provides async terminal methods over `IQueryable<T>`:

```csharp
await query.ToListAsync();
await query.ToArrayAsync();
await query.CountAsync();
await query.SingleOrDefaultAsync();
```

The terminal method surface includes common operations such as:

- `AllAsync`
- `AnyAsync`
- `CountAsync`
- `LongCountAsync`
- `FirstAsync`
- `FirstOrDefaultAsync`
- `SingleAsync`
- `SingleOrDefaultAsync`
- `ToArrayAsync`
- `ToListAsync`
- `MinAsync`
- `MaxAsync`
- `SumAsync`
- `AverageAsync`

Provider implementations receive native provider queryables and delegate to native async APIs where available.

## Query Functions

`QueryFunctions` contains portable database-query functions intended for expression translation:

```csharp
var albums = await domainContainer.Albums
	.Where(a => QueryFunctions.Like(a.Name, "%Integration%"))
	.ToListAsync();
```

Date and time functions are also included:

```csharp
var comparisonDate = new DateTime(2024, 1, 4);

var albums = domainContainer.Albums
	.Where(a => QueryFunctions.DiffDays(a.ReleaseDate, comparisonDate) == 3)
	.ToList();
```

These functions have no in-memory implementation. They are intended to be translated by a provider implementation. If no provider mapping exists, execution should fail rather than silently changing semantics.

## Set-Based Mutations

The abstraction supports set-based database mutations where provider implementations supply them.

### Delete

```csharp
var deleted = await domainContainer.Tracks
	.Where(t => t.Album.Name == "Blue Integration")
	.ExecuteDeleteAsync();
```

### Update

Portable set-based update follows the EF Core setter-call style:

```csharp
var updated = await domainContainer.Tracks
	.Where(t => t.Album.Name == "Blue Integration")
	.ExecuteUpdateAsync(setters => setters
		.SetProperty(t => t.DurationSeconds, t => t.DurationSeconds + 5));
```

Set-based mutations execute immediately in the database. They do not materialize selected entities, do not use change tracking to update or delete individual entities and do not synchronize already-tracked instances.

EF Core supports these operations natively. EF6 support is available through the optional Entity Framework Plus integration package.

## Query Translation Infrastructure

The query translation system is centered on `QueryTranslator`:

```csharp
public class QueryTranslator
```

It groups provider-specific query behavior:
- `TerminalMethodsAdapter` adapts async terminal methods.
- `ShapingMethodsAdapter` adapts executable non-terminal shaping methods such as `Include` and `AsNoTracking`.
- `SetOperationMethodsAdapter` adapts set-based mutations.
- `MethodMappingsByMethodInfo` maps portable expression functions to native provider methods.

The base library supplies reusable helpers:

- `QueryOperations`
- `TranslatingQueryProvider`
- `MethodMappingExpressionVisitor`
- `IncludeChainNormalizerVisitor`
- `MethodInfoCatalog`
- `QueryExtensionMethodInfos`
- `QueryFunctionsMethodInfos`

Provider implementers should not need to reimplement expression traversal or terminal dispatch. They supply adapters and method mappings.

## Method Mappings

`MethodMapping` describes a translation from a portable method call to a provider-native expression.

Use `IsomorphicMethodMapping` when the native method has the same argument shape and compatible return type:

```csharp
portable DiffDays(start, end)
native   DbFunctions.DiffDays(start, end)
```

Use `ExpressionMethodMapping` when arguments need custom handling, such as inserting an EF Core `EF.Functions` marker argument.

Mappings must preserve type compatibility. A portable method returning `bool` should map to a native expression returning `bool`, so that containing predicate expressions remain valid.

## Nested Transactions

`ITransaction` abstracts transaction scopes and supports nesting:

```csharp
using (var transaction = domainContainer.BeginTransaction())
{
	var artist = domainContainer.Create<Artist>();
	artist.Name = "The Example Band";

	domainContainer.Artists.Add(artist);

	transaction.Commit();
}
```

Nested operations can call `BeginTransaction()` without needing to know whether they are running alone or inside a larger operation.

`TransactionMode.Real` performs real save/commit operations as they occur.

`TransactionMode.Deferred` defers saving until the top-level transaction commits.

`ITransaction.Pass()` marks a transaction scope as successful without saving.

## Exception Normalization

Provider-specific database exceptions can be normalized into portable exception types:

- `DataAccessException`
- `IntegrityViolationException`
- `UniqueConstraintViolationException`
- `ReferentialConstraintViolationException`

Example:

```csharp
try
{
	domainContainer.SaveChanges();
}
catch (UniqueConstraintViolationException)
{
	// Handle duplicate key or unique index violation.
}
```

SQL Server providers map error numbers such as `2601`, `2627` and `547` to the appropriate portable exceptions.

## Entity Listeners

`IEntityListener` observes entity lifecycle events:

```csharp
public interface IEntityListener
{
	void OnAdding(object entity);
	void OnDeleting(object entity);
	void OnChanging(object entity);
	void OnRead(object entity);
	void OnAdded(object entity);
}
```

Listeners are registered on the active domain container:

```csharp
domainContainer.EntityListeners.Add(new AuditEntityListener());
```

They can be used to implement auditing, access checks, change logging, event publication and other cross-cutting policies without binding application code to a concrete ORM.

## Provider Implementations

### Entity Framework 6

`Grammophone.DataAccess.EntityFramework` adapts EF6 `DbContext`, `DbSet<T>` and query APIs.

Use `EFDomainContainer` for the underlying EF6 context and `EFDomainContainerAdapter<T>` to expose a provider-neutral domain contract.

### Entity Framework Core

`Grammophone.DataAccess.EntityFrameworkCore` adapts EF Core 8.

`EFCoreDomainContainer` configures lazy-loading proxies and can optionally configure change-tracking proxies. The `useChangeTracking` constructor argument must be specified consciously because change-tracking proxies impose additional requirements on entity classes.

### Entity Framework Plus

`Grammophone.DataAccess.EntityFramework.Plus` adds EF6 set-based mutation support through Entity Framework Plus.

Use `EFDomainContainerPlus` when EF6 domain containers need `ExecuteDelete`, `ExecuteDeleteAsync`, `ExecuteUpdate` or `ExecuteUpdateAsync`.

## Testing Strategy

The test suite uses a shared music domain model:

- `Artist`
- `Album`
- `Track`
- `Genre`

The same abstract MSTest cases run against EF6 and EF Core implementations. SQL Server LocalDB is used for provider integration tests so that query translation, eager loading, async execution, constraints and SQL exception numbers are exercised against a real relational provider.

Test categories include:

- standard LINQ behavior
- async terminal methods
- portable query extensions
- exception translation
- set-based mutations

## Design Boundaries

`Grammophone.DataAccess` abstracts data access mechanics. It does not attempt to redefine entity modeling itself.

Provider-specific concerns such as spatial type mapping, provider-specific bulk-update details or database-specific exception codes belong in provider implementation packages or higher layers.

Application code should depend on domain-specific `IDomainContainer` interfaces and avoid importing provider query-extension namespaces when portability is desired.
