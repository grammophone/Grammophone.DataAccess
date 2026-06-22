# Grammophone.DataAccess

`Grammophone.DataAccess` is a .NET Standard 2.0 contract library for building provider-independent data access layers over ORM-style unit-of-work implementations.

It is a standalone data access abstraction library. It is also used as the data access foundation for `Grammophone.Domos` components.

The central abstraction is `IDomainContainer`. It represents the active data access context, similar in role to Entity Framework `DbContext` or NHibernate `ISession`, while exposing provider-neutral operations for entity sets, tracking, explicit loading, transactions, exception normalization and query execution.

## Main Features

- `IDomainContainer` abstracts the unit of work and repository root.
- `IEntitySet<T>` abstracts add, attach, remove, find and query access to entities.
- `IEntityQuery<T>` extends `IOrderedQueryable<T>` so standard LINQ composition remains available.
- `IEntityEntry<T>`, `IPropertyEntry<T, P>`, `IReferenceEntry<T, P>` and `ICollectionEntry<T, I>` abstract change tracking and explicit loading.
- `ITransaction` supports nested transaction scopes over implementations that may or may not have native nested transactions.
- `DataAccessException` and descendants normalize provider exceptions such as unique and referential constraint violations.
- `Grammophone.DataAccess.QueryExtensions` provides portable query extensions such as `Include`, `ThenInclude`, `AsNoTracking`, async terminal methods and query functions.
- Query translation infrastructure maps portable method calls to provider-native APIs while preserving normal `IQueryable<T>` usage.

## Query Example

Given a domain contract such as:

```csharp
public interface IMusicDomainContainer : IDomainContainer
{
	IEntitySet<Artist> Artists { get; }
	IEntitySet<Album> Albums { get; }
	IEntitySet<Track> Tracks { get; }
	IEntitySet<Genre> Genres { get; }
}
```

portable query code can use standard LINQ and Grammophone query extensions:

```csharp
using Grammophone.DataAccess.QueryExtensions;

var albums = await domainContainer.Albums
	.Include(album => album.Tracks)
	.ThenInclude(track => track.Genre)
	.AsNoTracking()
	.Where(album => QueryFunctions.Like(album.Name, "%Blue%"))
	.ToListAsync();
```

The application code does not import Entity Framework or EF Core query-extension namespaces. The provider implementation translates the portable methods to native operations.

## Transactions

`IDomainContainer.BeginTransaction()` returns an `ITransaction`. Nested transactions compose through commit/pass/rollback votes. The implementation may use real commits or deferred commits depending on `TransactionMode`.

```csharp
using (var transaction = domainContainer.BeginTransaction())
{
	var genre = domainContainer.Create<Genre>();
	genre.Name = "Progressive Tests";

	domainContainer.Genres.Add(genre);

	transaction.Commit();
}
```

## Documentation

- [Overview](documentation/overview.md)
- [Abstractions](documentation/abstractions.md)
- [Query extensions and translation](documentation/query-extensions.md)
- [Nested transactions](documentation/transactions.md)
- [Exception normalization](documentation/exception-normalization.md)
- [Entity listeners](documentation/entity-listeners.md)
- [ObservableHashSet](documentation/observable-hash-set.md)
- [Extension points for higher layers](documentation/extension-points.md)

## Implementations

- `Grammophone.DataAccess.EntityFramework` implements the contracts for Entity Framework 6.
- `Grammophone.DataAccess.EntityFrameworkCore` implements the contracts for Entity Framework Core 8.

Both implementations can expose fully provider-neutral `IEntitySet<T>` properties through domain container adapters.
