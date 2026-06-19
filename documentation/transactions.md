# Nested Transactions

`IDomainContainer.BeginTransaction()` returns an `ITransaction`. The transaction must be committed or passed before disposal. Otherwise it contributes a rollback vote.

```csharp
using (var transaction = domainContainer.BeginTransaction())
{
	var artist = domainContainer.Create<Artist>();
	artist.Name = "The Example Band";

	domainContainer.Artists.Add(artist);

	transaction.Commit();
}
```

## Nested Units Of Work

Nested code can participate in an outer transaction without needing to know whether it is called alone or as part of a larger operation.

```csharp
private void AddAlbum(Album album)
{
	using (var transaction = domainContainer.BeginTransaction())
	{
		bool exists = domainContainer.Albums
			.Any(a => a.ArtistID == album.ArtistID && a.Name == album.Name);

		if (exists)
		{
			throw new InvalidOperationException("The album already exists.");
		}

		domainContainer.Albums.Add(album);

		transaction.Commit();
	}
}
```

An outer operation can call this repeatedly:

```csharp
using (var transaction = domainContainer.BeginTransaction())
{
	var genre = domainContainer.Create<Genre>();
	genre.Name = "Progressive Tests";

	domainContainer.Genres.Add(genre);

	foreach (var album in albums)
	{
		AddAlbum(album);
		album.Genre = genre;
	}

	transaction.Commit();
}
```

If any nested operation fails, the whole operation can roll back.

## Transaction Modes

`TransactionMode.Real` invokes underlying save and commit operations as they occur. This can make generated identifiers available earlier.

`TransactionMode.Deferred` defers save and commit work until the top-level transaction commits. This is useful when a provider or execution strategy needs to retry the whole unit of work.

`ITransaction.Pass()` marks the transaction as successful without saving. It lets a higher-level transaction decide when to save and commit.
