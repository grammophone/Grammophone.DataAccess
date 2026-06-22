# ObservableHashSet

`ObservableHashSet<T>` is a `HashSet<T>`-based collection that implements `INotifyCollectionChanged` and `INotifyPropertyChanged`.

It is intended for entity collection navigation properties when notification-based change tracking is required, especially with Entity Framework Core change-tracking proxies, without referencing EF Core collection types from the domain model.

## Why It Exists

EF Core change-tracking proxies require collection navigation instances to implement `INotifyCollectionChanged`. The built-in `ObservableCollection<T>` satisfies that requirement, but it has list semantics:

- duplicates are allowed
- membership checks are linear
- removal by value is linear

Many domain models prefer set semantics for relationship collections. `ObservableHashSet<T>` keeps set behavior while providing the notifications required by notification-based tracking.

## Example

```csharp
public class Artist
{
	public virtual int ID { get; set; }

	public virtual string Name { get; set; }

	public virtual ICollection<Album> Albums { get; set; }
		= new ObservableHashSet<Album>();
}
```

This keeps the public property typed as `ICollection<Album>` while using a notification-capable set implementation.

## Behavior

`ObservableHashSet<T>` raises `CollectionChanged` for actual additions and removals. It also raises `PropertyChanged` for `Count` whenever the collection changes.

The implementation forwards mutating `ICollection<T>` and `ISet<T>` interface calls to the notifying methods, so common navigation-property usage such as this raises notifications:

```csharp
artist.Albums.Add(album);
artist.Albums.Remove(album);
```

Bulk set operations such as `UnionWith`, `ExceptWith`, `IntersectWith`, `SymmetricExceptWith` and `RemoveWhere` report the actual items that were added or removed.

## Equality Comparers

Custom equality comparers are supported through the standard `HashSet<T>` constructors:

```csharp
var set = new ObservableHashSet<string>(StringComparer.OrdinalIgnoreCase);
```

Bulk operations use the set's comparer when determining which items are actually added or removed.

## Limitations

`ObservableHashSet<T>` inherits from `HashSet<T>`. `HashSet<T>` methods are not virtual, so notifications can be bypassed if the instance is explicitly cast to `HashSet<T>` and mutated through the base type.

Use it through `ObservableHashSet<T>`, `ICollection<T>` or `ISet<T>` references for notification-aware mutations.

The collection is not thread-safe, matching the behavior of `HashSet<T>`.
