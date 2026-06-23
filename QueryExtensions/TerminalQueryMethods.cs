using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Grammophone.DataAccess.QueryExtensions
{
	/// <summary>
	/// Extension methods for producing results from queries.
	/// </summary>
	public static class TerminalQueryMethods
	{
		#region Private fields

		/// <summary>
		/// A fallback adapter when the implementation does not provide one.
		/// </summary>
		private static readonly TerminalMethodsAdapter DefaultTerminalMethodsAdapter = new DefaultTerminalMethodsAdapter();

		#endregion

		#region Public methods

		/// <summary>
		/// Asynchronously determines whether all the elements of a sequence satisfy a condition.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements to test for a condition.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <returns>
		/// A task that represents the asynchronous operation.
		/// The task result contains <see langword="true"/> if every element passes the test in <paramref name="predicate"/>; otherwise, <see langword="false"/>.
		/// </returns>
		public static Task<bool> AllAsync<T>(this IQueryable<T> query, Expression<Func<T, bool>> predicate)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (predicate == null) throw new ArgumentNullException(nameof(predicate));

			return GetTerminalMethodsAdapter(query).AllAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, predicate));
		}

		/// <summary>
		/// Asynchronously determines whether all the elements of a sequence satisfy a condition.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements to test for a condition.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>
		/// A task that represents the asynchronous operation.
		/// The task result contains <see langword="true"/> if every element passes the test in <paramref name="predicate"/>; otherwise, <see langword="false"/>.
		/// </returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<bool> AllAsync<T>(this IQueryable<T> query, Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (predicate == null) throw new ArgumentNullException(nameof(predicate));

			return GetTerminalMethodsAdapter(query).AllAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, predicate), cancellationToken);
		}

		/// <summary>
		/// Asynchronously determines whether a sequence contains any elements.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> to check for being empty.</param>
		/// <returns>
		/// A task that represents the asynchronous operation.
		/// The task result contains <see langword="true"/> if the sequence contains any elements; otherwise, <see langword="false"/>.
		/// </returns>
		public static Task<bool> AnyAsync<T>(this IQueryable<T> query)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));

			return GetTerminalMethodsAdapter(query).AnyAsync(QueryOperations.GetNativeQueryable(query));
		}

		/// <summary>
		/// Asynchronously determines whether a sequence contains any elements.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> to check for being empty.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>
		/// A task that represents the asynchronous operation.
		/// The task result contains <see langword="true"/> if the sequence contains any elements; otherwise, <see langword="false"/>.
		/// </returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<bool> AnyAsync<T>(this IQueryable<T> query, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));

			return GetTerminalMethodsAdapter(query).AnyAsync(QueryOperations.GetNativeQueryable(query), cancellationToken);
		}

		/// <summary>
		/// Asynchronously determines whether any element of a sequence satisfies a condition.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements to test for a condition.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <returns>
		/// A task that represents the asynchronous operation.
		/// The task result contains <see langword="true"/> if any element passes the test in <paramref name="predicate"/>; otherwise, <see langword="false"/>.
		/// </returns>
		public static Task<bool> AnyAsync<T>(this IQueryable<T> query, Expression<Func<T, bool>> predicate)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (predicate == null) throw new ArgumentNullException(nameof(predicate));

			return GetTerminalMethodsAdapter(query).AnyAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, predicate));
		}

		/// <summary>
		/// Asynchronously determines whether any element of a sequence satisfies a condition.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements to test for a condition.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>
		/// A task that represents the asynchronous operation.
		/// The task result contains <see langword="true"/> if any element passes the test in <paramref name="predicate"/>; otherwise, <see langword="false"/>.
		/// </returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<bool> AnyAsync<T>(this IQueryable<T> query, Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (predicate == null) throw new ArgumentNullException(nameof(predicate));

			return GetTerminalMethodsAdapter(query).AnyAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, predicate), cancellationToken);
		}

		/// <summary>
		/// Asynchronously returns the number of elements in a sequence.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> that contains the elements to be counted.</param>
		/// <returns>
		/// A task that represents the asynchronous operation.
		/// The task result contains the number of elements in the input sequence.
		/// </returns>
		public static Task<int> CountAsync<T>(this IQueryable<T> query)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));

			return GetTerminalMethodsAdapter(query).CountAsync(QueryOperations.GetNativeQueryable(query));
		}

		/// <summary>
		/// Asynchronously returns the number of elements in a sequence.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> that contains the elements to be counted.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>
		/// A task that represents the asynchronous operation.
		/// The task result contains the number of elements in the input sequence.
		/// </returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<int> CountAsync<T>(this IQueryable<T> query, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));

			return GetTerminalMethodsAdapter(query).CountAsync(QueryOperations.GetNativeQueryable(query), cancellationToken);
		}

		/// <summary>
		/// Asynchronously returns the number of elements in a sequence that satisfy a condition.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> that contains the elements to be counted.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <returns>
		/// A task that represents the asynchronous operation.
		/// The task result contains the number of elements in the sequence that satisfy the condition in <paramref name="predicate"/>.
		/// </returns>
		public static Task<int> CountAsync<T>(this IQueryable<T> query, Expression<Func<T, bool>> predicate)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (predicate == null) throw new ArgumentNullException(nameof(predicate));

			return GetTerminalMethodsAdapter(query).CountAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, predicate));
		}

		/// <summary>
		/// Asynchronously returns the number of elements in a sequence that satisfy a condition.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> that contains the elements to be counted.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>
		/// A task that represents the asynchronous operation.
		/// The task result contains the number of elements in the sequence that satisfy the condition in <paramref name="predicate"/>.
		/// </returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<int> CountAsync<T>(this IQueryable<T> query, Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (predicate == null) throw new ArgumentNullException(nameof(predicate));

			return GetTerminalMethodsAdapter(query).CountAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, predicate), cancellationToken);
		}

		/// <summary>
		/// Asynchronously returns a <see cref="long"/> that represents the total number of elements in a sequence.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> that contains the elements to be counted.</param>
		/// <returns>
		/// A task that represents the asynchronous operation.
		/// The task result contains the number of elements in the input sequence.
		/// </returns>
		public static Task<long> LongCountAsync<T>(this IQueryable<T> query)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));

			return GetTerminalMethodsAdapter(query).LongCountAsync(QueryOperations.GetNativeQueryable(query));
		}

		/// <summary>
		/// Asynchronously returns a <see cref="long"/> that represents the total number of elements in a sequence.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> that contains the elements to be counted.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>
		/// A task that represents the asynchronous operation.
		/// The task result contains the number of elements in the input sequence.
		/// </returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<long> LongCountAsync<T>(this IQueryable<T> query, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));

			return GetTerminalMethodsAdapter(query).LongCountAsync(QueryOperations.GetNativeQueryable(query), cancellationToken);
		}

		/// <summary>
		/// Asynchronously returns a <see cref="long"/> that represents the number of elements in a sequence that satisfy a condition.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> that contains the elements to be counted.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <returns>
		/// A task that represents the asynchronous operation.
		/// The task result contains the number of elements in the sequence that satisfy the condition in <paramref name="predicate"/>.
		/// </returns>
		public static Task<long> LongCountAsync<T>(this IQueryable<T> query, Expression<Func<T, bool>> predicate)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (predicate == null) throw new ArgumentNullException(nameof(predicate));

			return GetTerminalMethodsAdapter(query).LongCountAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, predicate));
		}

		/// <summary>
		/// Asynchronously returns a <see cref="long"/> that represents the number of elements in a sequence that satisfy a condition.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> that contains the elements to be counted.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>
		/// A task that represents the asynchronous operation.
		/// The task result contains the number of elements in the sequence that satisfy the condition in <paramref name="predicate"/>.
		/// </returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<long> LongCountAsync<T>(this IQueryable<T> query, Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (predicate == null) throw new ArgumentNullException(nameof(predicate));

			return GetTerminalMethodsAdapter(query).LongCountAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, predicate), cancellationToken);
		}

		/// <summary>
		/// Asynchronously returns the first element of a sequence.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> to return the first element of.</param>
		/// <returns>
		/// A task that represents the asynchronous operation.
		/// The task result contains the first element in <paramref name="query"/>.
		/// </returns>
		/// <exception cref="InvalidOperationException"><paramref name="query"/> contains no elements.</exception>
		public static Task<T> FirstAsync<T>(this IQueryable<T> query)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));

			return GetTerminalMethodsAdapter(query).FirstAsync(QueryOperations.GetNativeQueryable(query));
		}

		/// <summary>
		/// Asynchronously returns the first element of a sequence.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> to return the first element of.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>
		/// A task that represents the asynchronous operation.
		/// The task result contains the first element in <paramref name="query"/>.
		/// </returns>
		/// <exception cref="InvalidOperationException"><paramref name="query"/> contains no elements.</exception>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<T> FirstAsync<T>(this IQueryable<T> query, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));

			return GetTerminalMethodsAdapter(query).FirstAsync(QueryOperations.GetNativeQueryable(query), cancellationToken);
		}

		/// <summary>
		/// Asynchronously returns the first element of a sequence that satisfies a specified condition.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> to return the first element of.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <returns>
		/// A task that represents the asynchronous operation.
		/// The task result contains the first element in <paramref name="query"/> that passes the test in <paramref name="predicate"/>.
		/// </returns>
		/// <exception cref="InvalidOperationException">No element satisfies the condition in <paramref name="predicate"/>.</exception>
		public static Task<T> FirstAsync<T>(this IQueryable<T> query, Expression<Func<T, bool>> predicate)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (predicate == null) throw new ArgumentNullException(nameof(predicate));

			return GetTerminalMethodsAdapter(query).FirstAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, predicate));
		}

		/// <summary>
		/// Asynchronously returns the first element of a sequence that satisfies a specified condition.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> to return the first element of.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>
		/// A task that represents the asynchronous operation.
		/// The task result contains the first element in <paramref name="query"/> that passes the test in <paramref name="predicate"/>.
		/// </returns>
		/// <exception cref="InvalidOperationException">No element satisfies the condition in <paramref name="predicate"/>.</exception>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<T> FirstAsync<T>(this IQueryable<T> query, Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (predicate == null) throw new ArgumentNullException(nameof(predicate));

			return GetTerminalMethodsAdapter(query).FirstAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, predicate), cancellationToken);
		}

		/// <summary>
		/// Asynchronously returns the first element of a sequence, or a default value if the sequence contains no elements.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> to return the first element of.</param>
		/// <returns>
		/// A task that represents the asynchronous operation.
		/// The task result contains <see langword="default"/> if <paramref name="query"/> is empty; otherwise, the first element in <paramref name="query"/>.
		/// </returns>
		public static Task<T> FirstOrDefaultAsync<T>(this IQueryable<T> query)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));

			return GetTerminalMethodsAdapter(query).FirstOrDefaultAsync(QueryOperations.GetNativeQueryable(query));
		}

		/// <summary>
		/// Asynchronously returns the first element of a sequence, or a default value if the sequence contains no elements.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> to return the first element of.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>
		/// A task that represents the asynchronous operation.
		/// The task result contains <see langword="default"/> if <paramref name="query"/> is empty; otherwise, the first element in <paramref name="query"/>.
		/// </returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<T> FirstOrDefaultAsync<T>(this IQueryable<T> query, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));

			return GetTerminalMethodsAdapter(query).FirstOrDefaultAsync(QueryOperations.GetNativeQueryable(query), cancellationToken);
		}

		/// <summary>
		/// Asynchronously returns the first element of a sequence that satisfies a specified condition, or a default value if no such element is found.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> to return the first element of.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <returns>
		/// A task that represents the asynchronous operation.
		/// The task result contains <see langword="default"/> if no element passes the test in <paramref name="predicate"/>; otherwise, the first matching element.
		/// </returns>
		public static Task<T> FirstOrDefaultAsync<T>(this IQueryable<T> query, Expression<Func<T, bool>> predicate)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (predicate == null) throw new ArgumentNullException(nameof(predicate));

			return GetTerminalMethodsAdapter(query).FirstOrDefaultAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, predicate));
		}

		/// <summary>
		/// Asynchronously returns the first element of a sequence that satisfies a specified condition, or a default value if no such element is found.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> to return the first element of.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>
		/// A task that represents the asynchronous operation.
		/// The task result contains <see langword="default"/> if no element passes the test in <paramref name="predicate"/>; otherwise, the first matching element.
		/// </returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<T> FirstOrDefaultAsync<T>(this IQueryable<T> query, Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (predicate == null) throw new ArgumentNullException(nameof(predicate));

			return GetTerminalMethodsAdapter(query).FirstOrDefaultAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, predicate), cancellationToken);
		}

		/// <summary>
		/// Asynchronously returns the only element of a sequence, and throws an exception if there is not exactly one element.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> to return the single element of.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the single element of the input sequence.</returns>
		/// <exception cref="InvalidOperationException"><paramref name="query"/> does not contain exactly one element.</exception>
		public static Task<T> SingleAsync<T>(this IQueryable<T> query)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));

			return GetTerminalMethodsAdapter(query).SingleAsync(QueryOperations.GetNativeQueryable(query));
		}

		/// <summary>
		/// Asynchronously returns the only element of a sequence, and throws an exception if there is not exactly one element.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> to return the single element of.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the single element of the input sequence.</returns>
		/// <exception cref="InvalidOperationException"><paramref name="query"/> does not contain exactly one element.</exception>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<T> SingleAsync<T>(this IQueryable<T> query, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));

			return GetTerminalMethodsAdapter(query).SingleAsync(QueryOperations.GetNativeQueryable(query), cancellationToken);
		}

		/// <summary>
		/// Asynchronously returns the only element of a sequence that satisfies a specified condition, and throws an exception if more than one such element exists.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> to return the single element of.</param>
		/// <param name="predicate">A function to test an element for a condition.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the single matching element.</returns>
		/// <exception cref="InvalidOperationException">The sequence does not contain exactly one matching element.</exception>
		public static Task<T> SingleAsync<T>(this IQueryable<T> query, Expression<Func<T, bool>> predicate)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (predicate == null) throw new ArgumentNullException(nameof(predicate));

			return GetTerminalMethodsAdapter(query).SingleAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, predicate));
		}

		/// <summary>
		/// Asynchronously returns the only element of a sequence that satisfies a specified condition, and throws an exception if more than one such element exists.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> to return the single element of.</param>
		/// <param name="predicate">A function to test an element for a condition.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the single matching element.</returns>
		/// <exception cref="InvalidOperationException">The sequence does not contain exactly one matching element.</exception>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<T> SingleAsync<T>(this IQueryable<T> query, Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (predicate == null) throw new ArgumentNullException(nameof(predicate));

			return GetTerminalMethodsAdapter(query).SingleAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, predicate), cancellationToken);
		}

		/// <summary>
		/// Asynchronously returns the only element of a sequence, or a default value if the sequence is empty; this method throws if more than one element exists.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> to return the single element of.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the single element, or <see langword="default"/> if the sequence is empty.</returns>
		/// <exception cref="InvalidOperationException"><paramref name="query"/> contains more than one element.</exception>
		public static Task<T> SingleOrDefaultAsync<T>(this IQueryable<T> query)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));

			return GetTerminalMethodsAdapter(query).SingleOrDefaultAsync(QueryOperations.GetNativeQueryable(query));
		}

		/// <summary>
		/// Asynchronously returns the only element of a sequence, or a default value if the sequence is empty; this method throws if more than one element exists.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> to return the single element of.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the single element, or <see langword="default"/> if the sequence is empty.</returns>
		/// <exception cref="InvalidOperationException"><paramref name="query"/> contains more than one element.</exception>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<T> SingleOrDefaultAsync<T>(this IQueryable<T> query, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));

			return GetTerminalMethodsAdapter(query).SingleOrDefaultAsync(QueryOperations.GetNativeQueryable(query), cancellationToken);
		}

		/// <summary>
		/// Asynchronously returns the only element of a sequence that satisfies a specified condition, or a default value if no such element exists.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> to return the single element of.</param>
		/// <param name="predicate">A function to test an element for a condition.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the single matching element, or <see langword="default"/> if no such element exists.</returns>
		/// <exception cref="InvalidOperationException">More than one element satisfies the condition in <paramref name="predicate"/>.</exception>
		public static Task<T> SingleOrDefaultAsync<T>(this IQueryable<T> query, Expression<Func<T, bool>> predicate)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (predicate == null) throw new ArgumentNullException(nameof(predicate));

			return GetTerminalMethodsAdapter(query).SingleOrDefaultAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, predicate));
		}

		/// <summary>
		/// Asynchronously returns the only element of a sequence that satisfies a specified condition, or a default value if no such element exists.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> to return the single element of.</param>
		/// <param name="predicate">A function to test an element for a condition.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the single matching element, or <see langword="default"/> if no such element exists.</returns>
		/// <exception cref="InvalidOperationException">More than one element satisfies the condition in <paramref name="predicate"/>.</exception>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<T> SingleOrDefaultAsync<T>(this IQueryable<T> query, Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (predicate == null) throw new ArgumentNullException(nameof(predicate));

			return GetTerminalMethodsAdapter(query).SingleOrDefaultAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, predicate), cancellationToken);
		}

		/// <summary>
		/// Asynchronously creates an array from a query.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> to create an array from.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains an array that contains elements from the input sequence.</returns>
		public static Task<T[]> ToArrayAsync<T>(this IQueryable<T> query)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));

			return GetTerminalMethodsAdapter(query).ToArrayAsync(QueryOperations.GetNativeQueryable(query));
		}

		/// <summary>
		/// Asynchronously creates an array from a query.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> to create an array from.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains an array that contains elements from the input sequence.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<T[]> ToArrayAsync<T>(this IQueryable<T> query, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));

			return GetTerminalMethodsAdapter(query).ToArrayAsync(QueryOperations.GetNativeQueryable(query), cancellationToken);
		}

		/// <summary>
		/// Asynchronously creates a <see cref="List{T}"/> from a query.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> to create a list from.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="List{T}"/> that contains elements from the input sequence.</returns>
		public static Task<List<T>> ToListAsync<T>(this IQueryable<T> query)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));

			return GetTerminalMethodsAdapter(query).ToListAsync(QueryOperations.GetNativeQueryable(query));
		}

		/// <summary>
		/// Asynchronously creates a <see cref="List{T}"/> from a query.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> to create a list from.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="List{T}"/> that contains elements from the input sequence.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<List<T>> ToListAsync<T>(this IQueryable<T> query, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));

			return GetTerminalMethodsAdapter(query).ToListAsync(QueryOperations.GetNativeQueryable(query), cancellationToken);
		}

		/// <summary>
		/// Asynchronously creates a <see cref="Dictionary{TKey, TValue}"/> from a query according to a specified key selector function.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> to create a dictionary from.</param>
		/// <param name="keySelector">A function to extract a key from each element.</param>
		/// <returns>
		/// A task that represents the asynchronous operation.
		/// The task result contains a <see cref="Dictionary{TKey, TValue}"/> that contains values of type <typeparamref name="T"/> selected from the input sequence.
		/// </returns>
		/// <exception cref="ArgumentNullException"><paramref name="query"/> or <paramref name="keySelector"/> is null.</exception>
		/// <exception cref="ArgumentException"><paramref name="keySelector"/> produces duplicate keys for two elements.</exception>
		public static Task<Dictionary<TKey, T>> ToDictionaryAsync<T, TKey>(this IQueryable<T> query, Func<T, TKey> keySelector)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));

			return GetTerminalMethodsAdapter(query).ToDictionaryAsync(QueryOperations.GetNativeQueryable(query), keySelector);
		}

		/// <summary>
		/// Asynchronously creates a <see cref="Dictionary{TKey, TValue}"/> from a query according to a specified key selector function.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> to create a dictionary from.</param>
		/// <param name="keySelector">A function to extract a key from each element.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>
		/// A task that represents the asynchronous operation.
		/// The task result contains a <see cref="Dictionary{TKey, TValue}"/> that contains values of type <typeparamref name="T"/> selected from the input sequence.
		/// </returns>
		/// <exception cref="ArgumentNullException"><paramref name="query"/> or <paramref name="keySelector"/> is null.</exception>
		/// <exception cref="ArgumentException"><paramref name="keySelector"/> produces duplicate keys for two elements.</exception>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<Dictionary<TKey, T>> ToDictionaryAsync<T, TKey>(this IQueryable<T> query, Func<T, TKey> keySelector, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));

			return GetTerminalMethodsAdapter(query).ToDictionaryAsync(QueryOperations.GetNativeQueryable(query), keySelector, cancellationToken);
		}

		/// <summary>
		/// Asynchronously returns the minimum value of a sequence.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> that contains the elements to determine the minimum of.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the minimum value in the sequence.</returns>
		public static Task<T> MinAsync<T>(this IQueryable<T> query)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));

			return GetTerminalMethodsAdapter(query).MinAsync(QueryOperations.GetNativeQueryable(query));
		}

		/// <summary>
		/// Asynchronously returns the minimum value of a sequence.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> that contains the elements to determine the minimum of.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the minimum value in the sequence.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<T> MinAsync<T>(this IQueryable<T> query, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));

			return GetTerminalMethodsAdapter(query).MinAsync(QueryOperations.GetNativeQueryable(query), cancellationToken);
		}

		/// <summary>
		/// Asynchronously invokes a projection function on each element of a sequence and returns the minimum resulting value.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <typeparam name="TResult">The type of the projected value.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before computing the minimum.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the minimum projected value.</returns>
		public static Task<TResult> MinAsync<T, TResult>(this IQueryable<T> query, Expression<Func<T, TResult>> selector)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (selector == null) throw new ArgumentNullException(nameof(selector));

			return GetTerminalMethodsAdapter(query).MinAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, selector));
		}

		/// <summary>
		/// Asynchronously invokes a projection function on each element of a sequence and returns the minimum resulting value.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <typeparam name="TResult">The type of the projected value.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before computing the minimum.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the minimum projected value.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<TResult> MinAsync<T, TResult>(this IQueryable<T> query, Expression<Func<T, TResult>> selector, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (selector == null) throw new ArgumentNullException(nameof(selector));

			return GetTerminalMethodsAdapter(query).MinAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, selector), cancellationToken);
		}

		/// <summary>
		/// Asynchronously returns the maximum value of a sequence.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> that contains the elements to determine the maximum of.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the maximum value in the sequence.</returns>
		public static Task<T> MaxAsync<T>(this IQueryable<T> query)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));

			return GetTerminalMethodsAdapter(query).MaxAsync(QueryOperations.GetNativeQueryable(query));
		}

		/// <summary>
		/// Asynchronously returns the maximum value of a sequence.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> that contains the elements to determine the maximum of.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the maximum value in the sequence.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<T> MaxAsync<T>(this IQueryable<T> query, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));

			return GetTerminalMethodsAdapter(query).MaxAsync(QueryOperations.GetNativeQueryable(query), cancellationToken);
		}

		/// <summary>
		/// Asynchronously invokes a projection function on each element of a sequence and returns the maximum resulting value.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <typeparam name="TResult">The type of the projected value.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before computing the maximum.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the maximum projected value.</returns>
		public static Task<TResult> MaxAsync<T, TResult>(this IQueryable<T> query, Expression<Func<T, TResult>> selector)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (selector == null) throw new ArgumentNullException(nameof(selector));

			return GetTerminalMethodsAdapter(query).MaxAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, selector));
		}

		/// <summary>
		/// Asynchronously invokes a projection function on each element of a sequence and returns the maximum resulting value.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <typeparam name="TResult">The type of the projected value.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before computing the maximum.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the maximum projected value.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<TResult> MaxAsync<T, TResult>(this IQueryable<T> query, Expression<Func<T, TResult>> selector, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (selector == null) throw new ArgumentNullException(nameof(selector));

			return GetTerminalMethodsAdapter(query).MaxAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, selector), cancellationToken);
		}

		/// <summary>
		/// Asynchronously computes the sum of a sequence of <see cref="int"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of <see cref="int"/> values to calculate the sum of.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the values in the sequence.</returns>
		public static Task<int> SumAsync(this IQueryable<int> query)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			return GetTerminalMethodsAdapter(query).SumAsync(QueryOperations.GetNativeQueryable(query));
		}

		/// <summary>
		/// Asynchronously computes the sum of a sequence of <see cref="int"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of <see cref="int"/> values to calculate the sum of.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the values in the sequence.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<int> SumAsync(this IQueryable<int> query, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			return GetTerminalMethodsAdapter(query).SumAsync(QueryOperations.GetNativeQueryable(query), cancellationToken);
		}

		/// <summary>
		/// Asynchronously computes the sum of a sequence of nullable <see cref="int"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of nullable <see cref="int"/> values to calculate the sum of.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the values in the sequence.</returns>
		public static Task<int?> SumAsync(this IQueryable<int?> query)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			return GetTerminalMethodsAdapter(query).SumAsync(QueryOperations.GetNativeQueryable(query));
		}

		/// <summary>
		/// Asynchronously computes the sum of a sequence of nullable <see cref="int"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of nullable <see cref="int"/> values to calculate the sum of.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the values in the sequence.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<int?> SumAsync(this IQueryable<int?> query, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			return GetTerminalMethodsAdapter(query).SumAsync(QueryOperations.GetNativeQueryable(query), cancellationToken);
		}

		/// <summary>
		/// Asynchronously computes the sum of a sequence of <see cref="long"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of <see cref="long"/> values to calculate the sum of.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the values in the sequence.</returns>
		public static Task<long> SumAsync(this IQueryable<long> query)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			return GetTerminalMethodsAdapter(query).SumAsync(QueryOperations.GetNativeQueryable(query));
		}

		/// <summary>
		/// Asynchronously computes the sum of a sequence of <see cref="long"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of <see cref="long"/> values to calculate the sum of.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the values in the sequence.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<long> SumAsync(this IQueryable<long> query, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			return GetTerminalMethodsAdapter(query).SumAsync(QueryOperations.GetNativeQueryable(query), cancellationToken);
		}

		/// <summary>
		/// Asynchronously computes the sum of a sequence of nullable <see cref="long"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of nullable <see cref="long"/> values to calculate the sum of.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the values in the sequence.</returns>
		public static Task<long?> SumAsync(this IQueryable<long?> query)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			return GetTerminalMethodsAdapter(query).SumAsync(QueryOperations.GetNativeQueryable(query));
		}

		/// <summary>
		/// Asynchronously computes the sum of a sequence of nullable <see cref="long"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of nullable <see cref="long"/> values to calculate the sum of.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the values in the sequence.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<long?> SumAsync(this IQueryable<long?> query, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			return GetTerminalMethodsAdapter(query).SumAsync(QueryOperations.GetNativeQueryable(query), cancellationToken);
		}

		/// <summary>
		/// Asynchronously computes the sum of a sequence of <see cref="float"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of <see cref="float"/> values to calculate the sum of.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the values in the sequence.</returns>
		public static Task<float> SumAsync(this IQueryable<float> query)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			return GetTerminalMethodsAdapter(query).SumAsync(QueryOperations.GetNativeQueryable(query));
		}

		/// <summary>
		/// Asynchronously computes the sum of a sequence of <see cref="float"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of <see cref="float"/> values to calculate the sum of.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the values in the sequence.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<float> SumAsync(this IQueryable<float> query, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			return GetTerminalMethodsAdapter(query).SumAsync(QueryOperations.GetNativeQueryable(query), cancellationToken);
		}

		/// <summary>
		/// Asynchronously computes the sum of a sequence of nullable <see cref="float"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of nullable <see cref="float"/> values to calculate the sum of.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the values in the sequence.</returns>
		public static Task<float?> SumAsync(this IQueryable<float?> query)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			return GetTerminalMethodsAdapter(query).SumAsync(QueryOperations.GetNativeQueryable(query));
		}

		/// <summary>
		/// Asynchronously computes the sum of a sequence of nullable <see cref="float"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of nullable <see cref="float"/> values to calculate the sum of.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the values in the sequence.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<float?> SumAsync(this IQueryable<float?> query, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			return GetTerminalMethodsAdapter(query).SumAsync(QueryOperations.GetNativeQueryable(query), cancellationToken);
		}

		/// <summary>
		/// Asynchronously computes the sum of a sequence of <see cref="double"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of <see cref="double"/> values to calculate the sum of.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the values in the sequence.</returns>
		public static Task<double> SumAsync(this IQueryable<double> query)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			return GetTerminalMethodsAdapter(query).SumAsync(QueryOperations.GetNativeQueryable(query));
		}

		/// <summary>
		/// Asynchronously computes the sum of a sequence of <see cref="double"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of <see cref="double"/> values to calculate the sum of.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the values in the sequence.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<double> SumAsync(this IQueryable<double> query, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			return GetTerminalMethodsAdapter(query).SumAsync(QueryOperations.GetNativeQueryable(query), cancellationToken);
		}

		/// <summary>
		/// Asynchronously computes the sum of a sequence of nullable <see cref="double"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of nullable <see cref="double"/> values to calculate the sum of.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the values in the sequence.</returns>
		public static Task<double?> SumAsync(this IQueryable<double?> query)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			return GetTerminalMethodsAdapter(query).SumAsync(QueryOperations.GetNativeQueryable(query));
		}

		/// <summary>
		/// Asynchronously computes the sum of a sequence of nullable <see cref="double"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of nullable <see cref="double"/> values to calculate the sum of.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the values in the sequence.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<double?> SumAsync(this IQueryable<double?> query, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			return GetTerminalMethodsAdapter(query).SumAsync(QueryOperations.GetNativeQueryable(query), cancellationToken);
		}

		/// <summary>
		/// Asynchronously computes the sum of a sequence of <see cref="decimal"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of <see cref="decimal"/> values to calculate the sum of.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the values in the sequence.</returns>
		public static Task<decimal> SumAsync(this IQueryable<decimal> query)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			return GetTerminalMethodsAdapter(query).SumAsync(QueryOperations.GetNativeQueryable(query));
		}

		/// <summary>
		/// Asynchronously computes the sum of a sequence of <see cref="decimal"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of <see cref="decimal"/> values to calculate the sum of.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the values in the sequence.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<decimal> SumAsync(this IQueryable<decimal> query, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			return GetTerminalMethodsAdapter(query).SumAsync(QueryOperations.GetNativeQueryable(query), cancellationToken);
		}

		/// <summary>
		/// Asynchronously computes the sum of a sequence of nullable <see cref="decimal"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of nullable <see cref="decimal"/> values to calculate the sum of.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the values in the sequence.</returns>
		public static Task<decimal?> SumAsync(this IQueryable<decimal?> query)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			return GetTerminalMethodsAdapter(query).SumAsync(QueryOperations.GetNativeQueryable(query));
		}

		/// <summary>
		/// Asynchronously computes the sum of a sequence of nullable <see cref="decimal"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of nullable <see cref="decimal"/> values to calculate the sum of.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the values in the sequence.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<decimal?> SumAsync(this IQueryable<decimal?> query, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			return GetTerminalMethodsAdapter(query).SumAsync(QueryOperations.GetNativeQueryable(query), cancellationToken);
		}

		/// <summary>
		/// Asynchronously computes the sum of a projected sequence of <see cref="int"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the sum.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the projected values.</returns>
		public static Task<int> SumAsync<T>(this IQueryable<T> query, Expression<Func<T, int>> selector)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (selector == null) throw new ArgumentNullException(nameof(selector));
			return GetTerminalMethodsAdapter(query).SumAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, selector));
		}

		/// <summary>
		/// Asynchronously computes the sum of a projected sequence of <see cref="int"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the sum.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the projected values.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<int> SumAsync<T>(this IQueryable<T> query, Expression<Func<T, int>> selector, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (selector == null) throw new ArgumentNullException(nameof(selector));
			return GetTerminalMethodsAdapter(query).SumAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, selector), cancellationToken);
		}

		/// <summary>
		/// Asynchronously computes the sum of a projected sequence of nullable <see cref="int"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the sum.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the projected values.</returns>
		public static Task<int?> SumAsync<T>(this IQueryable<T> query, Expression<Func<T, int?>> selector)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (selector == null) throw new ArgumentNullException(nameof(selector));
			return GetTerminalMethodsAdapter(query).SumAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, selector));
		}

		/// <summary>
		/// Asynchronously computes the sum of a projected sequence of nullable <see cref="int"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the sum.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the projected values.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<int?> SumAsync<T>(this IQueryable<T> query, Expression<Func<T, int?>> selector, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (selector == null) throw new ArgumentNullException(nameof(selector));
			return GetTerminalMethodsAdapter(query).SumAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, selector), cancellationToken);
		}

		/// <summary>
		/// Asynchronously computes the sum of a projected sequence of <see cref="long"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the sum.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the projected values.</returns>
		public static Task<long> SumAsync<T>(this IQueryable<T> query, Expression<Func<T, long>> selector)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (selector == null) throw new ArgumentNullException(nameof(selector));
			return GetTerminalMethodsAdapter(query).SumAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, selector));
		}

		/// <summary>
		/// Asynchronously computes the sum of a projected sequence of <see cref="long"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the sum.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the projected values.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<long> SumAsync<T>(this IQueryable<T> query, Expression<Func<T, long>> selector, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (selector == null) throw new ArgumentNullException(nameof(selector));
			return GetTerminalMethodsAdapter(query).SumAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, selector), cancellationToken);
		}

		/// <summary>
		/// Asynchronously computes the sum of a projected sequence of nullable <see cref="long"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the sum.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the projected values.</returns>
		public static Task<long?> SumAsync<T>(this IQueryable<T> query, Expression<Func<T, long?>> selector)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (selector == null) throw new ArgumentNullException(nameof(selector));
			return GetTerminalMethodsAdapter(query).SumAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, selector));
		}

		/// <summary>
		/// Asynchronously computes the sum of a projected sequence of nullable <see cref="long"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the sum.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the projected values.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<long?> SumAsync<T>(this IQueryable<T> query, Expression<Func<T, long?>> selector, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (selector == null) throw new ArgumentNullException(nameof(selector));
			return GetTerminalMethodsAdapter(query).SumAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, selector), cancellationToken);
		}

		/// <summary>
		/// Asynchronously computes the sum of a projected sequence of <see cref="float"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the sum.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the projected values.</returns>
		public static Task<float> SumAsync<T>(this IQueryable<T> query, Expression<Func<T, float>> selector)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (selector == null) throw new ArgumentNullException(nameof(selector));
			return GetTerminalMethodsAdapter(query).SumAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, selector));
		}

		/// <summary>
		/// Asynchronously computes the sum of a projected sequence of <see cref="float"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the sum.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the projected values.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<float> SumAsync<T>(this IQueryable<T> query, Expression<Func<T, float>> selector, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (selector == null) throw new ArgumentNullException(nameof(selector));
			return GetTerminalMethodsAdapter(query).SumAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, selector), cancellationToken);
		}

		/// <summary>
		/// Asynchronously computes the sum of a projected sequence of nullable <see cref="float"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the sum.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the projected values.</returns>
		public static Task<float?> SumAsync<T>(this IQueryable<T> query, Expression<Func<T, float?>> selector)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (selector == null) throw new ArgumentNullException(nameof(selector));
			return GetTerminalMethodsAdapter(query).SumAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, selector));
		}

		/// <summary>
		/// Asynchronously computes the sum of a projected sequence of nullable <see cref="float"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the sum.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the projected values.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<float?> SumAsync<T>(this IQueryable<T> query, Expression<Func<T, float?>> selector, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (selector == null) throw new ArgumentNullException(nameof(selector));
			return GetTerminalMethodsAdapter(query).SumAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, selector), cancellationToken);
		}

		/// <summary>
		/// Asynchronously computes the sum of a projected sequence of <see cref="double"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the sum.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the projected values.</returns>
		public static Task<double> SumAsync<T>(this IQueryable<T> query, Expression<Func<T, double>> selector)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (selector == null) throw new ArgumentNullException(nameof(selector));
			return GetTerminalMethodsAdapter(query).SumAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, selector));
		}

		/// <summary>
		/// Asynchronously computes the sum of a projected sequence of <see cref="double"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the sum.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the projected values.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<double> SumAsync<T>(this IQueryable<T> query, Expression<Func<T, double>> selector, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (selector == null) throw new ArgumentNullException(nameof(selector));
			return GetTerminalMethodsAdapter(query).SumAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, selector), cancellationToken);
		}

		/// <summary>
		/// Asynchronously computes the sum of a projected sequence of nullable <see cref="double"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the sum.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the projected values.</returns>
		public static Task<double?> SumAsync<T>(this IQueryable<T> query, Expression<Func<T, double?>> selector)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (selector == null) throw new ArgumentNullException(nameof(selector));
			return GetTerminalMethodsAdapter(query).SumAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, selector));
		}

		/// <summary>
		/// Asynchronously computes the sum of a projected sequence of nullable <see cref="double"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the sum.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the projected values.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<double?> SumAsync<T>(this IQueryable<T> query, Expression<Func<T, double?>> selector, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (selector == null) throw new ArgumentNullException(nameof(selector));
			return GetTerminalMethodsAdapter(query).SumAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, selector), cancellationToken);
		}

		/// <summary>
		/// Asynchronously computes the sum of a projected sequence of <see cref="decimal"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the sum.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the projected values.</returns>
		public static Task<decimal> SumAsync<T>(this IQueryable<T> query, Expression<Func<T, decimal>> selector)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (selector == null) throw new ArgumentNullException(nameof(selector));
			return GetTerminalMethodsAdapter(query).SumAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, selector));
		}

		/// <summary>
		/// Asynchronously computes the sum of a projected sequence of <see cref="decimal"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the sum.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the projected values.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<decimal> SumAsync<T>(this IQueryable<T> query, Expression<Func<T, decimal>> selector, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (selector == null) throw new ArgumentNullException(nameof(selector));
			return GetTerminalMethodsAdapter(query).SumAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, selector), cancellationToken);
		}

		/// <summary>
		/// Asynchronously computes the sum of a projected sequence of nullable <see cref="decimal"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the sum.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the projected values.</returns>
		public static Task<decimal?> SumAsync<T>(this IQueryable<T> query, Expression<Func<T, decimal?>> selector)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (selector == null) throw new ArgumentNullException(nameof(selector));
			return GetTerminalMethodsAdapter(query).SumAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, selector));
		}

		/// <summary>
		/// Asynchronously computes the sum of a projected sequence of nullable <see cref="decimal"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the sum.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the projected values.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<decimal?> SumAsync<T>(this IQueryable<T> query, Expression<Func<T, decimal?>> selector, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (selector == null) throw new ArgumentNullException(nameof(selector));
			return GetTerminalMethodsAdapter(query).SumAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, selector), cancellationToken);
		}

		/// <summary>
		/// Asynchronously computes the average of a sequence of <see cref="int"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of <see cref="int"/> values to calculate the average of.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the values in the sequence.</returns>
		public static Task<double> AverageAsync(this IQueryable<int> query)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			return GetTerminalMethodsAdapter(query).AverageAsync(QueryOperations.GetNativeQueryable(query));
		}

		/// <summary>
		/// Asynchronously computes the average of a sequence of <see cref="int"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of <see cref="int"/> values to calculate the average of.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the values in the sequence.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<double> AverageAsync(this IQueryable<int> query, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			return GetTerminalMethodsAdapter(query).AverageAsync(QueryOperations.GetNativeQueryable(query), cancellationToken);
		}

		/// <summary>
		/// Asynchronously computes the average of a sequence of nullable <see cref="int"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of nullable <see cref="int"/> values to calculate the average of.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the values in the sequence.</returns>
		public static Task<double?> AverageAsync(this IQueryable<int?> query)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			return GetTerminalMethodsAdapter(query).AverageAsync(QueryOperations.GetNativeQueryable(query));
		}

		/// <summary>
		/// Asynchronously computes the average of a sequence of nullable <see cref="int"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of nullable <see cref="int"/> values to calculate the average of.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the values in the sequence.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<double?> AverageAsync(this IQueryable<int?> query, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			return GetTerminalMethodsAdapter(query).AverageAsync(QueryOperations.GetNativeQueryable(query), cancellationToken);
		}

		/// <summary>
		/// Asynchronously computes the average of a sequence of <see cref="long"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of <see cref="long"/> values to calculate the average of.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the values in the sequence.</returns>
		public static Task<double> AverageAsync(this IQueryable<long> query)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			return GetTerminalMethodsAdapter(query).AverageAsync(QueryOperations.GetNativeQueryable(query));
		}

		/// <summary>
		/// Asynchronously computes the average of a sequence of <see cref="long"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of <see cref="long"/> values to calculate the average of.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the values in the sequence.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<double> AverageAsync(this IQueryable<long> query, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			return GetTerminalMethodsAdapter(query).AverageAsync(QueryOperations.GetNativeQueryable(query), cancellationToken);
		}

		/// <summary>
		/// Asynchronously computes the average of a sequence of nullable <see cref="long"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of nullable <see cref="long"/> values to calculate the average of.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the values in the sequence.</returns>
		public static Task<double?> AverageAsync(this IQueryable<long?> query)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			return GetTerminalMethodsAdapter(query).AverageAsync(QueryOperations.GetNativeQueryable(query));
		}

		/// <summary>
		/// Asynchronously computes the average of a sequence of nullable <see cref="long"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of nullable <see cref="long"/> values to calculate the average of.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the values in the sequence.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<double?> AverageAsync(this IQueryable<long?> query, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			return GetTerminalMethodsAdapter(query).AverageAsync(QueryOperations.GetNativeQueryable(query), cancellationToken);
		}

		/// <summary>
		/// Asynchronously computes the average of a sequence of <see cref="float"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of <see cref="float"/> values to calculate the average of.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the values in the sequence.</returns>
		public static Task<float> AverageAsync(this IQueryable<float> query)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			return GetTerminalMethodsAdapter(query).AverageAsync(QueryOperations.GetNativeQueryable(query));
		}

		/// <summary>
		/// Asynchronously computes the average of a sequence of <see cref="float"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of <see cref="float"/> values to calculate the average of.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the values in the sequence.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<float> AverageAsync(this IQueryable<float> query, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			return GetTerminalMethodsAdapter(query).AverageAsync(QueryOperations.GetNativeQueryable(query), cancellationToken);
		}

		/// <summary>
		/// Asynchronously computes the average of a sequence of nullable <see cref="float"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of nullable <see cref="float"/> values to calculate the average of.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the values in the sequence.</returns>
		public static Task<float?> AverageAsync(this IQueryable<float?> query)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			return GetTerminalMethodsAdapter(query).AverageAsync(QueryOperations.GetNativeQueryable(query));
		}

		/// <summary>
		/// Asynchronously computes the average of a sequence of nullable <see cref="float"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of nullable <see cref="float"/> values to calculate the average of.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the values in the sequence.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<float?> AverageAsync(this IQueryable<float?> query, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			return GetTerminalMethodsAdapter(query).AverageAsync(QueryOperations.GetNativeQueryable(query), cancellationToken);
		}

		/// <summary>
		/// Asynchronously computes the average of a sequence of <see cref="double"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of <see cref="double"/> values to calculate the average of.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the values in the sequence.</returns>
		public static Task<double> AverageAsync(this IQueryable<double> query)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			return GetTerminalMethodsAdapter(query).AverageAsync(QueryOperations.GetNativeQueryable(query));
		}

		/// <summary>
		/// Asynchronously computes the average of a sequence of <see cref="double"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of <see cref="double"/> values to calculate the average of.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the values in the sequence.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<double> AverageAsync(this IQueryable<double> query, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			return GetTerminalMethodsAdapter(query).AverageAsync(QueryOperations.GetNativeQueryable(query), cancellationToken);
		}

		/// <summary>
		/// Asynchronously computes the average of a sequence of nullable <see cref="double"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of nullable <see cref="double"/> values to calculate the average of.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the values in the sequence.</returns>
		public static Task<double?> AverageAsync(this IQueryable<double?> query)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			return GetTerminalMethodsAdapter(query).AverageAsync(QueryOperations.GetNativeQueryable(query));
		}

		/// <summary>
		/// Asynchronously computes the average of a sequence of nullable <see cref="double"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of nullable <see cref="double"/> values to calculate the average of.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the values in the sequence.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<double?> AverageAsync(this IQueryable<double?> query, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			return GetTerminalMethodsAdapter(query).AverageAsync(QueryOperations.GetNativeQueryable(query), cancellationToken);
		}

		/// <summary>
		/// Asynchronously computes the average of a sequence of <see cref="decimal"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of <see cref="decimal"/> values to calculate the average of.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the values in the sequence.</returns>
		public static Task<decimal> AverageAsync(this IQueryable<decimal> query)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			return GetTerminalMethodsAdapter(query).AverageAsync(QueryOperations.GetNativeQueryable(query));
		}

		/// <summary>
		/// Asynchronously computes the average of a sequence of <see cref="decimal"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of <see cref="decimal"/> values to calculate the average of.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the values in the sequence.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<decimal> AverageAsync(this IQueryable<decimal> query, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			return GetTerminalMethodsAdapter(query).AverageAsync(QueryOperations.GetNativeQueryable(query), cancellationToken);
		}

		/// <summary>
		/// Asynchronously computes the average of a sequence of nullable <see cref="decimal"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of nullable <see cref="decimal"/> values to calculate the average of.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the values in the sequence.</returns>
		public static Task<decimal?> AverageAsync(this IQueryable<decimal?> query)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			return GetTerminalMethodsAdapter(query).AverageAsync(QueryOperations.GetNativeQueryable(query));
		}

		/// <summary>
		/// Asynchronously computes the average of a sequence of nullable <see cref="decimal"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of nullable <see cref="decimal"/> values to calculate the average of.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the values in the sequence.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<decimal?> AverageAsync(this IQueryable<decimal?> query, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			return GetTerminalMethodsAdapter(query).AverageAsync(QueryOperations.GetNativeQueryable(query), cancellationToken);
		}

		/// <summary>
		/// Asynchronously computes the average of a projected sequence of <see cref="int"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the average.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the projected values.</returns>
		public static Task<double> AverageAsync<T>(this IQueryable<T> query, Expression<Func<T, int>> selector)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (selector == null) throw new ArgumentNullException(nameof(selector));
			return GetTerminalMethodsAdapter(query).AverageAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, selector));
		}

		/// <summary>
		/// Asynchronously computes the average of a projected sequence of <see cref="int"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the average.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the projected values.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<double> AverageAsync<T>(this IQueryable<T> query, Expression<Func<T, int>> selector, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (selector == null) throw new ArgumentNullException(nameof(selector));
			return GetTerminalMethodsAdapter(query).AverageAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, selector), cancellationToken);
		}

		/// <summary>
		/// Asynchronously computes the average of a projected sequence of nullable <see cref="int"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the average.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the projected values.</returns>
		public static Task<double?> AverageAsync<T>(this IQueryable<T> query, Expression<Func<T, int?>> selector)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (selector == null) throw new ArgumentNullException(nameof(selector));
			return GetTerminalMethodsAdapter(query).AverageAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, selector));
		}

		/// <summary>
		/// Asynchronously computes the average of a projected sequence of nullable <see cref="int"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the average.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the projected values.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<double?> AverageAsync<T>(this IQueryable<T> query, Expression<Func<T, int?>> selector, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (selector == null) throw new ArgumentNullException(nameof(selector));
			return GetTerminalMethodsAdapter(query).AverageAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, selector), cancellationToken);
		}

		/// <summary>
		/// Asynchronously computes the average of a projected sequence of <see cref="long"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the average.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the projected values.</returns>
		public static Task<double> AverageAsync<T>(this IQueryable<T> query, Expression<Func<T, long>> selector)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (selector == null) throw new ArgumentNullException(nameof(selector));
			return GetTerminalMethodsAdapter(query).AverageAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, selector));
		}

		/// <summary>
		/// Asynchronously computes the average of a projected sequence of <see cref="long"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the average.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the projected values.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<double> AverageAsync<T>(this IQueryable<T> query, Expression<Func<T, long>> selector, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (selector == null) throw new ArgumentNullException(nameof(selector));
			return GetTerminalMethodsAdapter(query).AverageAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, selector), cancellationToken);
		}

		/// <summary>
		/// Asynchronously computes the average of a projected sequence of nullable <see cref="long"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the average.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the projected values.</returns>
		public static Task<double?> AverageAsync<T>(this IQueryable<T> query, Expression<Func<T, long?>> selector)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (selector == null) throw new ArgumentNullException(nameof(selector));
			return GetTerminalMethodsAdapter(query).AverageAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, selector));
		}

		/// <summary>
		/// Asynchronously computes the average of a projected sequence of nullable <see cref="long"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the average.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the projected values.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<double?> AverageAsync<T>(this IQueryable<T> query, Expression<Func<T, long?>> selector, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (selector == null) throw new ArgumentNullException(nameof(selector));
			return GetTerminalMethodsAdapter(query).AverageAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, selector), cancellationToken);
		}

		/// <summary>
		/// Asynchronously computes the average of a projected sequence of <see cref="float"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the average.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the projected values.</returns>
		public static Task<float> AverageAsync<T>(this IQueryable<T> query, Expression<Func<T, float>> selector)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (selector == null) throw new ArgumentNullException(nameof(selector));
			return GetTerminalMethodsAdapter(query).AverageAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, selector));
		}

		/// <summary>
		/// Asynchronously computes the average of a projected sequence of <see cref="float"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the average.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the projected values.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<float> AverageAsync<T>(this IQueryable<T> query, Expression<Func<T, float>> selector, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (selector == null) throw new ArgumentNullException(nameof(selector));
			return GetTerminalMethodsAdapter(query).AverageAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, selector), cancellationToken);
		}

		/// <summary>
		/// Asynchronously computes the average of a projected sequence of nullable <see cref="float"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the average.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the projected values.</returns>
		public static Task<float?> AverageAsync<T>(this IQueryable<T> query, Expression<Func<T, float?>> selector)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (selector == null) throw new ArgumentNullException(nameof(selector));
			return GetTerminalMethodsAdapter(query).AverageAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, selector));
		}

		/// <summary>
		/// Asynchronously computes the average of a projected sequence of nullable <see cref="float"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the average.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the projected values.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<float?> AverageAsync<T>(this IQueryable<T> query, Expression<Func<T, float?>> selector, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (selector == null) throw new ArgumentNullException(nameof(selector));
			return GetTerminalMethodsAdapter(query).AverageAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, selector), cancellationToken);
		}

		/// <summary>
		/// Asynchronously computes the average of a projected sequence of <see cref="double"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the average.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the projected values.</returns>
		public static Task<double> AverageAsync<T>(this IQueryable<T> query, Expression<Func<T, double>> selector)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (selector == null) throw new ArgumentNullException(nameof(selector));
			return GetTerminalMethodsAdapter(query).AverageAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, selector));
		}

		/// <summary>
		/// Asynchronously computes the average of a projected sequence of <see cref="double"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the average.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the projected values.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<double> AverageAsync<T>(this IQueryable<T> query, Expression<Func<T, double>> selector, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (selector == null) throw new ArgumentNullException(nameof(selector));
			return GetTerminalMethodsAdapter(query).AverageAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, selector), cancellationToken);
		}

		/// <summary>
		/// Asynchronously computes the average of a projected sequence of nullable <see cref="double"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the average.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the projected values.</returns>
		public static Task<double?> AverageAsync<T>(this IQueryable<T> query, Expression<Func<T, double?>> selector)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (selector == null) throw new ArgumentNullException(nameof(selector));
			return GetTerminalMethodsAdapter(query).AverageAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, selector));
		}

		/// <summary>
		/// Asynchronously computes the average of a projected sequence of nullable <see cref="double"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the average.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the projected values.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<double?> AverageAsync<T>(this IQueryable<T> query, Expression<Func<T, double?>> selector, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (selector == null) throw new ArgumentNullException(nameof(selector));
			return GetTerminalMethodsAdapter(query).AverageAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, selector), cancellationToken);
		}

		/// <summary>
		/// Asynchronously computes the average of a projected sequence of <see cref="decimal"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the average.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the projected values.</returns>
		public static Task<decimal> AverageAsync<T>(this IQueryable<T> query, Expression<Func<T, decimal>> selector)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (selector == null) throw new ArgumentNullException(nameof(selector));
			return GetTerminalMethodsAdapter(query).AverageAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, selector));
		}

		/// <summary>
		/// Asynchronously computes the average of a projected sequence of <see cref="decimal"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the average.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the projected values.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<decimal> AverageAsync<T>(this IQueryable<T> query, Expression<Func<T, decimal>> selector, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (selector == null) throw new ArgumentNullException(nameof(selector));
			return GetTerminalMethodsAdapter(query).AverageAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, selector), cancellationToken);
		}

		/// <summary>
		/// Asynchronously computes the average of a projected sequence of nullable <see cref="decimal"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the average.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the projected values.</returns>
		public static Task<decimal?> AverageAsync<T>(this IQueryable<T> query, Expression<Func<T, decimal?>> selector)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (selector == null) throw new ArgumentNullException(nameof(selector));
			return GetTerminalMethodsAdapter(query).AverageAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, selector));
		}

		/// <summary>
		/// Asynchronously computes the average of a projected sequence of nullable <see cref="decimal"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the average.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the projected values.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public static Task<decimal?> AverageAsync<T>(this IQueryable<T> query, Expression<Func<T, decimal?>> selector, CancellationToken cancellationToken)
		{
			if (query == null) throw new ArgumentNullException(nameof(query));
			if (selector == null) throw new ArgumentNullException(nameof(selector));
			return GetTerminalMethodsAdapter(query).AverageAsync(QueryOperations.GetNativeQueryable(query), QueryOperations.TranslateExpression(query, selector), cancellationToken);
		}

		#endregion

		#region Private methods

		private static TerminalMethodsAdapter GetTerminalMethodsAdapter<T>(IQueryable<T> query)
		{
			if (query is IEntityQuery<T> entityQuery)
			{
				var queryTranslator = entityQuery.DomainContainer.TryGetQueryTranslator();

				if (queryTranslator != null)
				{
					return queryTranslator.TerminalMethodsAdapter;
				}
			}

			return DefaultTerminalMethodsAdapter;
		}

		#endregion
	}
}



