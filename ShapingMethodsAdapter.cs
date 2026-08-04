using System;
using System.Linq;
using System.Linq.Expressions;

namespace Grammophone.DataAccess
{
	/// <summary>
	/// Contract for adapting non-terminal query shaping methods.
	/// </summary>
	public class ShapingMethodsAdapter
	{
		#region Public methods

		/// <summary>
		/// Specifies the related objects to include in the query results.
		/// </summary>
		public virtual IQueryable<T> Include<T>(IQueryable<T> nativeQuery, string path)
			where T : class
		{
			throw CreateNotSupportedException(nameof(Include));
		}

		/// <summary>
		/// Specifies the related objects to include in the query results.
		/// </summary>
		public virtual IQueryable<T> Include<T, TProperty>(
			IQueryable<T> nativeQuery,
			Expression<Func<T, TProperty>> pathExpression)
			where T : class
		{
			throw CreateNotSupportedException(nameof(Include));
		}

		/// <summary>
		/// Returns a new query where the entities returned will not be cached in the container.
		/// </summary>
		public virtual IQueryable<T> AsNoTracking<T>(IQueryable<T> nativeQuery)
			where T : class
		{
			throw CreateNotSupportedException(nameof(AsNoTracking));
		}

		#endregion

		#region Private methods

		private static DataAccessException CreateNotSupportedException(string methodName)
		{
			return new DataAccessException(
				$"Query shaping method '{methodName}' is not supported by the current query provider.");
		}

		#endregion
	}
}
