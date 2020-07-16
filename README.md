# Grammophone.DataAccess
This .NET Standard 2.0 library abstracts data access and reinforces it with higher-level functionality.

At the center of the abstract contract is `IDomainContainer`.
This abstracts the repository class for communicating with the data store, which typically is
handled by `DbContext` descendants in Entity Framework or NHibernate's `ISession`.

Most properties and methods of `IDomainContainer` follow what is common in such frameworks.
In our interfaces derived from `IDomainContainer` which define the data repository, we expect to have
properties of type `IEntitySet<T>` or even Entity Framework's `IDbSet<T>`, depending on the level of 
framework abstraction we desire. 
There is one exception to the similarities: Transactions are leveraged to allow nesting of units of work. Let's see an example.

```C#
private void AddAlbum(Album album)
{
  if (album == null) throw new ArgumentNullException(nameof(album));

  using (var transaction = this.DomainContainer.BeginTransaction())
  {
    bool albumAlreadyAdded =
      this.DomainContainer.Albums.Any(
        a => a.ArtistID == album.artistID && album.Name == a.Name);

    if (albumAlreadyAdded)
      throw new LogicException(
        $"The album '{album.Name}' has already been added to the artist's albums.");

    this.DomainContainer.Albums.Add(album);

    // The following is optional; it is provided for easy transition
    // of existing code of frameworks which require it.
    // this.DomainContainer.Save();

    transaction.Commit();
  }
}
```

The `DomainContainer` property is supposed to be our data repository interface. Use any
preferred dependency injection mechanism to provide your concrete implementation.
The `Commit` method of the `ITransaction` we obtained via `BeginTransaction` method auto-saves any entities if needed
before committing the changes. Else it only commits the changes, falling back to the behavior of existing
frameworks. This permits building higher blocks of transactional works around `ITransaction` interface alone.
Thus we can define a method which uses the `AddAlbum` method several times but running in a single transaction:

```C#
private void AddAlbumsToNewGenre(string genreName, IEnumerable<Album> albums)
{
  if (genreName == null) throw new ArgumentNullException(nameof(genreName));
  if (albums == null) throw new ArgumentNullException(nameof(albums));

  using (var transaction = this.DomainContainer.Begintransaction())
  {
    bool genreAlreadyExists = this.DomainContainer.Genres.Any(g => g.Name == genreName);

    if (genreAlreadyExists)
      throw new LogicException($"The genre `{genreName}' already exists.");

    var genre = this.DomainContainer.Genres.Create();
    this.DomainContainer.Genres.Add(genre);

    genre.Name = genreName;

    foreach (var album in albums)
    {
      AddAlbum(album);
      album.Genre = genre;
    }

    transaction.Commit();
  }
}
```

The method `AddAlbumsToNewGenre` will run in a single transaction. If any of the calls to `AddAlbum` in it
raises an exception, the whole unit of work will roll back.

Concrete implementations may provide any of the following two variations for implementing nesting transactions,
which are defined in the enumeration `TransactionMode` and reported in `IDomainContainer.TransactionMode` property:

<table>
<tbody>
<tr>
<td>
<strong>TransactionMode.Real</strong>
</td>
<td>
Actual underlying 'Commit' and 'SaveChanges' are invoked in corresponding methods.
This causes generated IDs to be obtained early when the transaction is still underway.
</td>
</tr>
<tr>
<td>
<strong>TransactionMode.Deferred</strong>
</td>
<td>
Within transactions, 'Commit' and 'SaveChanges' methods are deferred until a top-level transaction commits.
This means that database-generated columns such as auto-increments are not updated until after the final commit.
This behavior is aimed for transient fault environments to enable automatic retries, such as in the cloud.
</td>
</tr>
</tbody>
</table>

The implementation for Entity Framework,
[Grammophone.DataAccess.EntityFramework](https://github.com/grammophone/Grammophone.DataAccess.EntityFramework),
supports both scenarios by supplying the `TransactionMode` in the corresponding constructors.

When an error in the data store occurs, the exceptions are normalized to be derived from `DataAccessException`.
The descendants `IntegrityViolationException`, `UniqueConstraintViolationException` and
`ReferentialConstraintViolationException` permit universal handling of errors in
different types of data stores.

This library has no dependencies.
