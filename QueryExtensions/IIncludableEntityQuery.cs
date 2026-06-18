namespace Grammophone.DataAccess.QueryExtensions
{
	/// <summary>
	/// Supports typed chaining of portable eager-loading paths.
	/// </summary>
	/// <typeparam name="TEntity">The root entity type.</typeparam>
	/// <typeparam name="TProperty">The last included property type.</typeparam>
	public interface IIncludableEntityQuery<TEntity, TProperty> : IEntityQuery<TEntity>
	{
	}
}
