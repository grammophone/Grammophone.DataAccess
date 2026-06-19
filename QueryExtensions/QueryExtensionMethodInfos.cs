using System;
using System.Collections.Generic;
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
		/// Method information for the reference overload of ThenInclude.
		/// </summary>
		public static readonly MethodInfo ThenIncludeReference = GetThenIncludeMethodInfo(null);

		/// <summary>
		/// Method information for the collection overload of ThenInclude.
		/// </summary>
		public static readonly MethodInfo ThenIncludeCollection = GetThenIncludeMethodInfo(typeof(IEnumerable<>));

		/// <summary>
		/// Method information for the collection overload of ThenInclude using <see cref="ICollection{T}"/>.
		/// </summary>
		public static readonly MethodInfo ThenIncludeICollection = GetThenIncludeMethodInfo(typeof(ICollection<>));

		/// <summary>
		/// Method information for <see cref="QueryExtensions.AsNoTracking{T}(IQueryable{T})"/>.
		/// </summary>
		public static readonly MethodInfo AsNoTracking =
			MethodInfoCatalog.GetGenericMethodDefinition(
				typeof(QueryExtensions),
				nameof(QueryExtensions.AsNoTracking),
				typeof(IQueryable<>));

		#endregion

		#region Private methods

		private static MethodInfo GetThenIncludeMethodInfo(Type includedTypeDefinition)
		{
			foreach (var methodInfo in typeof(QueryExtensions).GetMethods(BindingFlags.Public | BindingFlags.Static))
			{
				if (methodInfo.Name != nameof(QueryExtensions.ThenInclude) || !methodInfo.IsGenericMethodDefinition) continue;

				var parameters = methodInfo.GetParameters();

				if (parameters.Length != 2) continue;

				var firstParameterType = parameters[0].ParameterType;

				if (!firstParameterType.IsGenericType || firstParameterType.GetGenericTypeDefinition() != typeof(IIncludableEntityQuery<,>)) continue;

				var includedType = firstParameterType.GetGenericArguments()[1];

				var actualIncludedTypeDefinition = includedType.IsGenericType ? includedType.GetGenericTypeDefinition() : null;

				if (actualIncludedTypeDefinition == includedTypeDefinition) return methodInfo;
			}

			throw new InvalidOperationException("The requested ThenInclude overload was not found.");
		}

		#endregion
	}
}
