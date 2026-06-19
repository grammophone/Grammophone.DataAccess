# Exception Normalization

Database providers expose different exception types and error codes. `Grammophone.DataAccess` normalizes these through `DataAccessException` and descendants.

Important exception types include:

```csharp
DataAccessException
IntegrityViolationException
UniqueConstraintViolationException
ReferentialConstraintViolationException
```

Provider implementations plug in exception transformers. For SQL Server, duplicate key errors map to `UniqueConstraintViolationException`, and foreign key violations map to `ReferentialConstraintViolationException`.

Example:

```csharp
try
{
	var artist = domainContainer.Create<Artist>();
	artist.Name = "The Example Band";

	domainContainer.Artists.Add(artist);
	domainContainer.SaveChanges();
}
catch (UniqueConstraintViolationException)
{
	// A unique artist name already exists.
}
```

This keeps application logic independent from SQL Server error numbers such as `2601`, `2627` and `547`.

Exception transformation belongs to provider implementation packages, not the base contract, because each database provider has its own exception types and error codes.
