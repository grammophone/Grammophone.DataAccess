# Extension Points

`Grammophone.DataAccess` exposes several extension points for provider implementations and higher application layers. The most important extension points are summarized here.

## Domain Container

`IDomainContainer` is the active unit of work. It exposes:

- entity creation through `Create<T>()`
- persistence through `SaveChanges()` and `SaveChangesAsync()`
- transaction scopes through `BeginTransaction()`
- entity entries through `Entry<T>(entity)`
- change tracking through `ChangeTracker`
- lifecycle listeners through `EntityListeners`
- provider query translation through `TryGetQueryTranslator()`
- exception normalization through `TranslateException(SystemException)`

Application layers should depend on domain-specific interfaces derived from `IDomainContainer`, not on provider containers.

## Entity Sets And Queries

`IEntitySet<T>` is the provider-neutral entity set abstraction. It supports creation, addition, removal, attach, find and query composition.

`IEntityQuery<T>` derives from `IOrderedQueryable<T>`. This is important because standard LINQ ordering operators require ordered query support from the provider-created queryable.

Provider implementations should preserve the adapted query wrapper across LINQ composition.

## Entry API

The entry abstractions are useful for explicit loading, tracking-state inspection and change manipulation:

```csharp
var albumEntry = domainContainer.Entry(album);

var tracksEntry = albumEntry.Collection(a => a.Tracks);
tracksEntry.Load();

bool loaded = tracksEntry.IsLoaded;
```

Use `IsLoaded` to assert eager loading. Do not use navigation enumeration alone because it can trigger lazy loading.

## Query Translation

`QueryTranslator` groups provider-specific query behavior:

- `TerminalMethodsAdapter` for async materialization and scalar terminal operations.
- `ShapingMethodsAdapter` for executable non-terminal shaping operations such as `Include` and `AsNoTracking`.
- `SetOperationMethodsAdapter` for set-based mutations such as `ExecuteDelete` and `ExecuteUpdate`.
- `MethodMappingsByMethodInfo` for expression-function mappings such as `QueryFunctions.Like` and date difference functions.

Provider implementations should receive native queryables and delegate to native provider APIs. Base helpers such as `QueryOperations.GetNativeQueryable` and `QueryOperations.TranslateExpression` centralize the unwrapping and translation work.

## Method Mapping

Use `IsomorphicMethodMapping` when a portable query function and native provider method have the same argument shape and compatible return type.

Use `ExpressionMethodMapping` when arguments must be inserted, reordered or otherwise transformed.

Mappings must preserve expression type compatibility. For example, a portable method returning `bool` should map to a native expression returning `bool` so that containing predicate lambdas remain valid.

## Set-Based Mutations

`ExecuteDelete` and `ExecuteUpdate` are set-based database operations. They do not materialize selected entities and do not synchronize already tracked instances.

Application layers should use them only when this bypass behavior is intended.

## Exception Normalization

Provider implementations should translate known database exceptions into portable exceptions:

- `UniqueConstraintViolationException`
- `ReferentialConstraintViolationException`
- `IntegrityViolationException`
- `DataAccessException`

Database-specific transformers should live in provider-specific packages.
