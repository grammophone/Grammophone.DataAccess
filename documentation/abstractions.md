# Abstractions

`Grammophone.DataAccess` separates application logic from a concrete ORM by exposing provider-neutral contracts.

## Domain Container

`IDomainContainer` is the root unit-of-work abstraction. It owns the active persistence context and exposes operations for saving, transactions, tracking, entity creation and exception translation.

Typical domain-specific contracts derive from it:

```csharp
public interface IMusicDomainContainer : IDomainContainer
{
	IEntitySet<Artist> Artists { get; }
	IEntitySet<Album> Albums { get; }
	IEntitySet<Track> Tracks { get; }
	IEntitySet<Genre> Genres { get; }
}
```

`IDomainContainer.Create<T>()` creates a new entity instance. Implementations may create proxy instances when supported by the underlying provider.

For effective proxy creation, every mapped property must be `virtual` without exception, including scalar properties, key properties, reference navigations and collection navigations. This requirement is especially important when an implementation uses change-tracking proxies. Collection navigation implementations may also need to support change notifications, depending on the provider.

Application code should call `IDomainContainer.Create<T>()` or `IEntitySet<T>.Create()` rather than provider-specific factory APIs.

When notification-capable set semantics are needed for collection navigations, `Grammophone.GenericContentModel.ObservableHashSet<T>` can be used as a provider-neutral alternative to ORM-specific observable set types.

## Entity Sets

`IEntitySet<T>` combines entity-set operations with `IEntityQuery<T>`:

```csharp
var album = domainContainer.Albums.Create();
album.Name = "Blue Integration";

domainContainer.Albums.Add(album);
domainContainer.SaveChanges();
```

`IEntityQuery<T>` derives from `IOrderedQueryable<T>`. This is intentional because standard LINQ ordering operators expect provider-created queryables to be assignable to `IOrderedQueryable<T>`.

## Tracking And Explicit Loading

The entry abstractions expose provider-neutral tracking and loading state:

```csharp
var album = domainContainer.Albums.Single(a => a.Name == "Blue Integration");

var tracksEntry = domainContainer.Entry(album)
	.Collection(a => a.Tracks);

if (!tracksEntry.IsLoaded)
{
	tracksEntry.Load();
}
```

This is also how tests should verify eager loading. Avoid using `album.Tracks.Any()` as the proof of eager loading, because that can trigger lazy loading and hide a failed include.

## Provider-Specific Adapters

Provider implementations typically expose an adapter domain container. The EF6 implementation follows this shape:

```csharp
public class EFMusicDomainContainerAdapter :
	EFDomainContainerAdapter<EFMusicDomainContainer>,
	IMusicDomainContainer
{
	private IEntitySet<Album> albums;

	public IEntitySet<Album> Albums =>
		albums ??= new EFSet<Album>(this.InnerDomainContainer.Albums, this);
}
```

This explicit mapping is verbose but reliable. It keeps provider-specific `DbSet<T>` members out of the application-facing contract.
