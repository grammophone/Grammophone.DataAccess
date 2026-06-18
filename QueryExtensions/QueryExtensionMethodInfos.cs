using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Grammophone.DataAccess.QueryExtensions
{
	/// <summary>
	/// Method information for portable query extension methods.
	/// </summary>
	public static class QueryExtensionMethodInfos
	{
		#region Public fields

		/// <summary>
		/// Method information for <see cref="QueryExtensions.Include{T}(IQueryable{T}, string)"/>.
		/// </summary>
		public static readonly MethodInfo IncludeString =
			MethodInfoCatalog.GetGenericMethodDefinition(
				typeof(QueryExtensions),
				nameof(QueryExtensions.Include),
				typeof(IQueryable<>),
				typeof(string));

		/// <summary>
		/// Method information for the expression overload of <see cref="QueryExtensions.Include{T, TProperty}"/>.
		/// </summary>
		public static readonly MethodInfo IncludeExpression =
			MethodInfoCatalog.GetGenericMethodDefinition(
				typeof(QueryExtensions),
				nameof(QueryExtensions.Include),
				typeof(IQueryable<>),
				typeof(Expression<>));

		/// <summary>
		/// Method information for <see cref="QueryExtensions.AsNoTracking{T}(IQueryable{T})"/>.
		/// </summary>
		public static readonly MethodInfo AsNoTracking =
			MethodInfoCatalog.GetGenericMethodDefinition(
				typeof(QueryExtensions),
				nameof(QueryExtensions.AsNoTracking),
				typeof(IQueryable<>));

		#endregion
	}
}
