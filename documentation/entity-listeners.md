# Entity Listeners

`IEntityListener` is the extension point for observing entity reads and writes through an `IDomainContainer`.

Listeners can be used to build access checks, audit trails, change logs, event publication and policy enforcement without coupling directly to Entity Framework or another ORM.

## Registration

Listeners are registered on the active domain container:

```csharp
domainContainer.EntityListeners.Add(new AuditEntityListener());
```

The collection is exposed by `IDomainContainer`:

```csharp
ICollection<IEntityListener> EntityListeners { get; }
```

Provider implementations are responsible for invoking listeners at the appropriate lifecycle points.

## Listener Contract

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

## Lifecycle

`OnRead` is called after an entity is materialized from the data store.

`OnAdding` is called before an added entity is saved.

`OnChanging` is called before a modified entity is saved.

`OnDeleting` is called before a deleted entity is saved.

`OnAdded` is called after an added entity has been saved successfully.

The exact hook mechanism is provider-specific. EF6 uses object materialization and saving events. EF Core implementations should invoke the same logical events from the change tracker and save pipeline.

## Example

```csharp
public sealed class AuditEntityListener : IEntityListener
{
	public void OnRead(object entity)
	{
		// Check read access or record read activity.
	}

	public void OnAdding(object entity)
	{
		// Validate create access.
	}

	public void OnChanging(object entity)
	{
		// Validate update access or capture old/new values.
	}

	public void OnDeleting(object entity)
	{
		// Validate delete access.
	}

	public void OnAdded(object entity)
	{
		// Publish post-create events after database-generated values are available.
	}
}
```

## Usage

Listeners can act as the central policy gateway for entity access and change logging:

```csharp
domainContainer.EntityListeners.Add(
	new AuditEntityListener(currentUser, policyService, auditLog));
```

This keeps authorization and auditing above the persistence provider while still running inside the active unit of work.

## Guidelines

Listeners should avoid causing unrelated query execution during save hooks unless deliberately designed to do so.

Listeners should throw meaningful domain or security exceptions when access is denied.

Listeners should assume they may receive entities of different runtime types, including proxy types.

Listeners should be idempotent for reads: `OnRead` may be called more than once for the same entity within a single read, because the underlying ORM can revisit an instance during graph fix-up or single-result verification. Neither provider deduplicates, so the number of calls is not guaranteed.

Listeners should not depend on EF-specific entry types. Use `IDomainContainer.Entry(entity)` or provider-neutral services when entry information is needed.
