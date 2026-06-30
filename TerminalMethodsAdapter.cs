using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Grammophone.DataAccess
{
	/// <summary>
	/// Contract for adapting terminal methods which execute or materialize a query.
	/// </summary>
	/// <remarks>
	/// <para>
	/// Adapter implementations execute terminal LINQ operations against a query whose expression has already been
	/// prepared by the surrounding query runtime. Provider-specific implementations should override methods where
	/// the underlying provider offers a more efficient or truly asynchronous implementation.
	/// </para>
	/// <para>
	/// Methods accepting a <see cref="CancellationToken"/> should observe it while waiting for the task to complete.
	/// The default implementation can only observe cancellation before starting its synchronous fallback operation.
	/// </para>
	/// </remarks>
	public abstract class TerminalMethodsAdapter
	{
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
		public abstract Task<bool> AllAsync<T>(IQueryable<T> query, Expression<Func<T, bool>> predicate);

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
		public abstract Task<bool> AllAsync<T>(IQueryable<T> query, Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);

		/// <summary>
		/// Asynchronously determines whether a sequence contains any elements.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> to check for being empty.</param>
		/// <returns>
		/// A task that represents the asynchronous operation.
		/// The task result contains <see langword="true"/> if the sequence contains any elements; otherwise, <see langword="false"/>.
		/// </returns>
		public abstract Task<bool> AnyAsync<T>(IQueryable<T> query);

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
		public abstract Task<bool> AnyAsync<T>(IQueryable<T> query, CancellationToken cancellationToken);

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
		public abstract Task<bool> AnyAsync<T>(IQueryable<T> query, Expression<Func<T, bool>> predicate);

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
		public abstract Task<bool> AnyAsync<T>(IQueryable<T> query, Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);

		/// <summary>
		/// Asynchronously returns the number of elements in a sequence.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> that contains the elements to be counted.</param>
		/// <returns>
		/// A task that represents the asynchronous operation.
		/// The task result contains the number of elements in the input sequence.
		/// </returns>
		public abstract Task<int> CountAsync<T>(IQueryable<T> query);

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
		public abstract Task<int> CountAsync<T>(IQueryable<T> query, CancellationToken cancellationToken);

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
		public abstract Task<int> CountAsync<T>(IQueryable<T> query, Expression<Func<T, bool>> predicate);

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
		public abstract Task<int> CountAsync<T>(IQueryable<T> query, Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);

		/// <summary>
		/// Asynchronously returns a <see cref="long"/> that represents the total number of elements in a sequence.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> that contains the elements to be counted.</param>
		/// <returns>
		/// A task that represents the asynchronous operation.
		/// The task result contains the number of elements in the input sequence.
		/// </returns>
		public abstract Task<long> LongCountAsync<T>(IQueryable<T> query);

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
		public abstract Task<long> LongCountAsync<T>(IQueryable<T> query, CancellationToken cancellationToken);

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
		public abstract Task<long> LongCountAsync<T>(IQueryable<T> query, Expression<Func<T, bool>> predicate);

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
		public abstract Task<long> LongCountAsync<T>(IQueryable<T> query, Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);

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
		public abstract Task<T> FirstAsync<T>(IQueryable<T> query);

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
		public abstract Task<T> FirstAsync<T>(IQueryable<T> query, CancellationToken cancellationToken);

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
		public abstract Task<T> FirstAsync<T>(IQueryable<T> query, Expression<Func<T, bool>> predicate);

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
		public abstract Task<T> FirstAsync<T>(IQueryable<T> query, Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);

		/// <summary>
		/// Asynchronously returns the first element of a sequence, or a default value if the sequence contains no elements.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> to return the first element of.</param>
		/// <returns>
		/// A task that represents the asynchronous operation.
		/// The task result contains <see langword="default"/> if <paramref name="query"/> is empty; otherwise, the first element in <paramref name="query"/>.
		/// </returns>
		public abstract Task<T> FirstOrDefaultAsync<T>(IQueryable<T> query);

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
		public abstract Task<T> FirstOrDefaultAsync<T>(IQueryable<T> query, CancellationToken cancellationToken);

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
		public abstract Task<T> FirstOrDefaultAsync<T>(IQueryable<T> query, Expression<Func<T, bool>> predicate);

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
		public abstract Task<T> FirstOrDefaultAsync<T>(IQueryable<T> query, Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);

		/// <summary>
		/// Asynchronously returns the only element of a sequence, and throws an exception if there is not exactly one element.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> to return the single element of.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the single element of the input sequence.</returns>
		/// <exception cref="InvalidOperationException"><paramref name="query"/> does not contain exactly one element.</exception>
		public abstract Task<T> SingleAsync<T>(IQueryable<T> query);

		/// <summary>
		/// Asynchronously returns the only element of a sequence, and throws an exception if there is not exactly one element.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> to return the single element of.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the single element of the input sequence.</returns>
		/// <exception cref="InvalidOperationException"><paramref name="query"/> does not contain exactly one element.</exception>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public abstract Task<T> SingleAsync<T>(IQueryable<T> query, CancellationToken cancellationToken);

		/// <summary>
		/// Asynchronously returns the only element of a sequence that satisfies a specified condition, and throws an exception if more than one such element exists.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> to return the single element of.</param>
		/// <param name="predicate">A function to test an element for a condition.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the single matching element.</returns>
		/// <exception cref="InvalidOperationException">The sequence does not contain exactly one matching element.</exception>
		public abstract Task<T> SingleAsync<T>(IQueryable<T> query, Expression<Func<T, bool>> predicate);

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
		public abstract Task<T> SingleAsync<T>(IQueryable<T> query, Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);

		/// <summary>
		/// Asynchronously returns the only element of a sequence, or a default value if the sequence is empty; this method throws if more than one element exists.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> to return the single element of.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the single element, or <see langword="default"/> if the sequence is empty.</returns>
		/// <exception cref="InvalidOperationException"><paramref name="query"/> contains more than one element.</exception>
		public abstract Task<T> SingleOrDefaultAsync<T>(IQueryable<T> query);

		/// <summary>
		/// Asynchronously returns the only element of a sequence, or a default value if the sequence is empty; this method throws if more than one element exists.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> to return the single element of.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the single element, or <see langword="default"/> if the sequence is empty.</returns>
		/// <exception cref="InvalidOperationException"><paramref name="query"/> contains more than one element.</exception>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public abstract Task<T> SingleOrDefaultAsync<T>(IQueryable<T> query, CancellationToken cancellationToken);

		/// <summary>
		/// Asynchronously returns the only element of a sequence that satisfies a specified condition, or a default value if no such element exists.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> to return the single element of.</param>
		/// <param name="predicate">A function to test an element for a condition.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the single matching element, or <see langword="default"/> if no such element exists.</returns>
		/// <exception cref="InvalidOperationException">More than one element satisfies the condition in <paramref name="predicate"/>.</exception>
		public abstract Task<T> SingleOrDefaultAsync<T>(IQueryable<T> query, Expression<Func<T, bool>> predicate);

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
		public abstract Task<T> SingleOrDefaultAsync<T>(IQueryable<T> query, Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);

		/// <summary>
		/// Asynchronously creates an array from a query.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> to create an array from.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains an array that contains elements from the input sequence.</returns>
		public abstract Task<T[]> ToArrayAsync<T>(IQueryable<T> query);

		/// <summary>
		/// Asynchronously creates an array from a query.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> to create an array from.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains an array that contains elements from the input sequence.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public abstract Task<T[]> ToArrayAsync<T>(IQueryable<T> query, CancellationToken cancellationToken);

		/// <summary>
		/// Asynchronously creates a <see cref="List{T}"/> from a query.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> to create a list from.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="List{T}"/> that contains elements from the input sequence.</returns>
		public abstract Task<List<T>> ToListAsync<T>(IQueryable<T> query);

		/// <summary>
		/// Asynchronously creates a <see cref="List{T}"/> from a query.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> to create a list from.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="List{T}"/> that contains elements from the input sequence.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public abstract Task<List<T>> ToListAsync<T>(IQueryable<T> query, CancellationToken cancellationToken);

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
		public abstract Task<Dictionary<TKey, T>> ToDictionaryAsync<T, TKey>(IQueryable<T> query, Func<T, TKey> keySelector);

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
		public abstract Task<Dictionary<TKey, T>> ToDictionaryAsync<T, TKey>(IQueryable<T> query, Func<T, TKey> keySelector, CancellationToken cancellationToken);

		/// <summary>
		/// Asynchronously creates a <see cref="Dictionary{TKey, TValue}"/> from a query according to specified key and value selector functions.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
		/// <typeparam name="TValue">The type of the value returned by <paramref name="valueSelector"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> to create a dictionary from.</param>
		/// <param name="keySelector">A function to extract a key from each element.</param>
		/// <param name="valueSelector">A function to extract a value from each element.</param>
		/// <returns>
		/// A task that represents the asynchronous operation.
		/// The task result contains a <see cref="Dictionary{TKey, TValue}"/> that contains values of type <typeparamref name="TValue"/> selected from the input sequence.
		/// </returns>
		/// <exception cref="ArgumentNullException"><paramref name="query"/> or <paramref name="keySelector"/> is null.</exception>
		/// <exception cref="ArgumentException"><paramref name="keySelector"/> produces duplicate keys for two elements.</exception>
		public abstract Task<Dictionary<TKey, TValue>> ToDictionaryAsync<T, TKey, TValue>(IQueryable<T> query, Func<T, TKey> keySelector, Func<T, TValue> valueSelector);

		/// <summary>
		/// Asynchronously creates a <see cref="Dictionary{TKey, TValue}"/> from a query according to specified key and value selector functions.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
		/// <typeparam name="TValue">The type of the value returned by <paramref name="valueSelector"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> to create a dictionary from.</param>
		/// <param name="keySelector">A function to extract a key from each element.</param>
		/// <param name="valueSelector">A function to extract a value from each element.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>
		/// A task that represents the asynchronous operation.
		/// The task result contains a <see cref="Dictionary{TKey, TValue}"/> that contains values of type <typeparamref name="TValue"/> selected from the input sequence.
		/// </returns>
		/// <exception cref="ArgumentNullException"><paramref name="query"/> or <paramref name="keySelector"/> is null.</exception>
		/// <exception cref="ArgumentException"><paramref name="keySelector"/> produces duplicate keys for two elements.</exception>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public abstract Task<Dictionary<TKey, TValue>> ToDictionaryAsync<T, TKey, TValue>(IQueryable<T> query, Func<T, TKey> keySelector, Func<T, TValue> valueSelector, CancellationToken cancellationToken);

		/// <summary>
		/// Asynchronously returns the minimum value of a sequence.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> that contains the elements to determine the minimum of.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the minimum value in the sequence.</returns>
		public abstract Task<T> MinAsync<T>(IQueryable<T> query);

		/// <summary>
		/// Asynchronously returns the minimum value of a sequence.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> that contains the elements to determine the minimum of.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the minimum value in the sequence.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public abstract Task<T> MinAsync<T>(IQueryable<T> query, CancellationToken cancellationToken);

		/// <summary>
		/// Asynchronously invokes a projection function on each element of a sequence and returns the minimum resulting value.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <typeparam name="TResult">The type of the projected value.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before computing the minimum.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the minimum projected value.</returns>
		public abstract Task<TResult> MinAsync<T, TResult>(IQueryable<T> query, Expression<Func<T, TResult>> selector);

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
		public abstract Task<TResult> MinAsync<T, TResult>(IQueryable<T> query, Expression<Func<T, TResult>> selector, CancellationToken cancellationToken);

		/// <summary>
		/// Asynchronously returns the maximum value of a sequence.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> that contains the elements to determine the maximum of.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the maximum value in the sequence.</returns>
		public abstract Task<T> MaxAsync<T>(IQueryable<T> query);

		/// <summary>
		/// Asynchronously returns the maximum value of a sequence.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> that contains the elements to determine the maximum of.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the maximum value in the sequence.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public abstract Task<T> MaxAsync<T>(IQueryable<T> query, CancellationToken cancellationToken);

		/// <summary>
		/// Asynchronously invokes a projection function on each element of a sequence and returns the maximum resulting value.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <typeparam name="TResult">The type of the projected value.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before computing the maximum.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the maximum projected value.</returns>
		public abstract Task<TResult> MaxAsync<T, TResult>(IQueryable<T> query, Expression<Func<T, TResult>> selector);

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
		public abstract Task<TResult> MaxAsync<T, TResult>(IQueryable<T> query, Expression<Func<T, TResult>> selector, CancellationToken cancellationToken);

		/// <summary>
		/// Asynchronously computes the sum of a sequence of <see cref="int"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of <see cref="int"/> values to calculate the sum of.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the values in the sequence.</returns>
		public abstract Task<int> SumAsync(IQueryable<int> query);

		/// <summary>
		/// Asynchronously computes the sum of a sequence of <see cref="int"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of <see cref="int"/> values to calculate the sum of.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the values in the sequence.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public abstract Task<int> SumAsync(IQueryable<int> query, CancellationToken cancellationToken);

		/// <summary>
		/// Asynchronously computes the sum of a sequence of nullable <see cref="int"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of nullable <see cref="int"/> values to calculate the sum of.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the values in the sequence.</returns>
		public abstract Task<int?> SumAsync(IQueryable<int?> query);

		/// <summary>
		/// Asynchronously computes the sum of a sequence of nullable <see cref="int"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of nullable <see cref="int"/> values to calculate the sum of.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the values in the sequence.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public abstract Task<int?> SumAsync(IQueryable<int?> query, CancellationToken cancellationToken);

		/// <summary>
		/// Asynchronously computes the sum of a sequence of <see cref="long"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of <see cref="long"/> values to calculate the sum of.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the values in the sequence.</returns>
		public abstract Task<long> SumAsync(IQueryable<long> query);

		/// <summary>
		/// Asynchronously computes the sum of a sequence of <see cref="long"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of <see cref="long"/> values to calculate the sum of.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the values in the sequence.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public abstract Task<long> SumAsync(IQueryable<long> query, CancellationToken cancellationToken);

		/// <summary>
		/// Asynchronously computes the sum of a sequence of nullable <see cref="long"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of nullable <see cref="long"/> values to calculate the sum of.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the values in the sequence.</returns>
		public abstract Task<long?> SumAsync(IQueryable<long?> query);

		/// <summary>
		/// Asynchronously computes the sum of a sequence of nullable <see cref="long"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of nullable <see cref="long"/> values to calculate the sum of.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the values in the sequence.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public abstract Task<long?> SumAsync(IQueryable<long?> query, CancellationToken cancellationToken);

		/// <summary>
		/// Asynchronously computes the sum of a sequence of <see cref="float"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of <see cref="float"/> values to calculate the sum of.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the values in the sequence.</returns>
		public abstract Task<float> SumAsync(IQueryable<float> query);

		/// <summary>
		/// Asynchronously computes the sum of a sequence of <see cref="float"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of <see cref="float"/> values to calculate the sum of.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the values in the sequence.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public abstract Task<float> SumAsync(IQueryable<float> query, CancellationToken cancellationToken);

		/// <summary>
		/// Asynchronously computes the sum of a sequence of nullable <see cref="float"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of nullable <see cref="float"/> values to calculate the sum of.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the values in the sequence.</returns>
		public abstract Task<float?> SumAsync(IQueryable<float?> query);

		/// <summary>
		/// Asynchronously computes the sum of a sequence of nullable <see cref="float"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of nullable <see cref="float"/> values to calculate the sum of.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the values in the sequence.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public abstract Task<float?> SumAsync(IQueryable<float?> query, CancellationToken cancellationToken);

		/// <summary>
		/// Asynchronously computes the sum of a sequence of <see cref="double"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of <see cref="double"/> values to calculate the sum of.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the values in the sequence.</returns>
		public abstract Task<double> SumAsync(IQueryable<double> query);

		/// <summary>
		/// Asynchronously computes the sum of a sequence of <see cref="double"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of <see cref="double"/> values to calculate the sum of.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the values in the sequence.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public abstract Task<double> SumAsync(IQueryable<double> query, CancellationToken cancellationToken);

		/// <summary>
		/// Asynchronously computes the sum of a sequence of nullable <see cref="double"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of nullable <see cref="double"/> values to calculate the sum of.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the values in the sequence.</returns>
		public abstract Task<double?> SumAsync(IQueryable<double?> query);

		/// <summary>
		/// Asynchronously computes the sum of a sequence of nullable <see cref="double"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of nullable <see cref="double"/> values to calculate the sum of.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the values in the sequence.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public abstract Task<double?> SumAsync(IQueryable<double?> query, CancellationToken cancellationToken);

		/// <summary>
		/// Asynchronously computes the sum of a sequence of <see cref="decimal"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of <see cref="decimal"/> values to calculate the sum of.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the values in the sequence.</returns>
		public abstract Task<decimal> SumAsync(IQueryable<decimal> query);

		/// <summary>
		/// Asynchronously computes the sum of a sequence of <see cref="decimal"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of <see cref="decimal"/> values to calculate the sum of.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the values in the sequence.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public abstract Task<decimal> SumAsync(IQueryable<decimal> query, CancellationToken cancellationToken);

		/// <summary>
		/// Asynchronously computes the sum of a sequence of nullable <see cref="decimal"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of nullable <see cref="decimal"/> values to calculate the sum of.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the values in the sequence.</returns>
		public abstract Task<decimal?> SumAsync(IQueryable<decimal?> query);

		/// <summary>
		/// Asynchronously computes the sum of a sequence of nullable <see cref="decimal"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of nullable <see cref="decimal"/> values to calculate the sum of.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the values in the sequence.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public abstract Task<decimal?> SumAsync(IQueryable<decimal?> query, CancellationToken cancellationToken);

		/// <summary>
		/// Asynchronously computes the sum of a projected sequence of <see cref="int"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the sum.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the projected values.</returns>
		public abstract Task<int> SumAsync<T>(IQueryable<T> query, Expression<Func<T, int>> selector);

		/// <summary>
		/// Asynchronously computes the sum of a projected sequence of <see cref="int"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the sum.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the projected values.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public abstract Task<int> SumAsync<T>(IQueryable<T> query, Expression<Func<T, int>> selector, CancellationToken cancellationToken);

		/// <summary>
		/// Asynchronously computes the sum of a projected sequence of nullable <see cref="int"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the sum.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the projected values.</returns>
		public abstract Task<int?> SumAsync<T>(IQueryable<T> query, Expression<Func<T, int?>> selector);

		/// <summary>
		/// Asynchronously computes the sum of a projected sequence of nullable <see cref="int"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the sum.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the projected values.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public abstract Task<int?> SumAsync<T>(IQueryable<T> query, Expression<Func<T, int?>> selector, CancellationToken cancellationToken);

		/// <summary>
		/// Asynchronously computes the sum of a projected sequence of <see cref="long"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the sum.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the projected values.</returns>
		public abstract Task<long> SumAsync<T>(IQueryable<T> query, Expression<Func<T, long>> selector);

		/// <summary>
		/// Asynchronously computes the sum of a projected sequence of <see cref="long"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the sum.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the projected values.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public abstract Task<long> SumAsync<T>(IQueryable<T> query, Expression<Func<T, long>> selector, CancellationToken cancellationToken);

		/// <summary>
		/// Asynchronously computes the sum of a projected sequence of nullable <see cref="long"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the sum.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the projected values.</returns>
		public abstract Task<long?> SumAsync<T>(IQueryable<T> query, Expression<Func<T, long?>> selector);

		/// <summary>
		/// Asynchronously computes the sum of a projected sequence of nullable <see cref="long"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the sum.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the projected values.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public abstract Task<long?> SumAsync<T>(IQueryable<T> query, Expression<Func<T, long?>> selector, CancellationToken cancellationToken);

		/// <summary>
		/// Asynchronously computes the sum of a projected sequence of <see cref="float"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the sum.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the projected values.</returns>
		public abstract Task<float> SumAsync<T>(IQueryable<T> query, Expression<Func<T, float>> selector);

		/// <summary>
		/// Asynchronously computes the sum of a projected sequence of <see cref="float"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the sum.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the projected values.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public abstract Task<float> SumAsync<T>(IQueryable<T> query, Expression<Func<T, float>> selector, CancellationToken cancellationToken);

		/// <summary>
		/// Asynchronously computes the sum of a projected sequence of nullable <see cref="float"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the sum.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the projected values.</returns>
		public abstract Task<float?> SumAsync<T>(IQueryable<T> query, Expression<Func<T, float?>> selector);

		/// <summary>
		/// Asynchronously computes the sum of a projected sequence of nullable <see cref="float"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the sum.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the projected values.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public abstract Task<float?> SumAsync<T>(IQueryable<T> query, Expression<Func<T, float?>> selector, CancellationToken cancellationToken);

		/// <summary>
		/// Asynchronously computes the sum of a projected sequence of <see cref="double"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the sum.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the projected values.</returns>
		public abstract Task<double> SumAsync<T>(IQueryable<T> query, Expression<Func<T, double>> selector);

		/// <summary>
		/// Asynchronously computes the sum of a projected sequence of <see cref="double"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the sum.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the projected values.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public abstract Task<double> SumAsync<T>(IQueryable<T> query, Expression<Func<T, double>> selector, CancellationToken cancellationToken);

		/// <summary>
		/// Asynchronously computes the sum of a projected sequence of nullable <see cref="double"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the sum.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the projected values.</returns>
		public abstract Task<double?> SumAsync<T>(IQueryable<T> query, Expression<Func<T, double?>> selector);

		/// <summary>
		/// Asynchronously computes the sum of a projected sequence of nullable <see cref="double"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the sum.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the projected values.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public abstract Task<double?> SumAsync<T>(IQueryable<T> query, Expression<Func<T, double?>> selector, CancellationToken cancellationToken);

		/// <summary>
		/// Asynchronously computes the sum of a projected sequence of <see cref="decimal"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the sum.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the projected values.</returns>
		public abstract Task<decimal> SumAsync<T>(IQueryable<T> query, Expression<Func<T, decimal>> selector);

		/// <summary>
		/// Asynchronously computes the sum of a projected sequence of <see cref="decimal"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the sum.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the projected values.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public abstract Task<decimal> SumAsync<T>(IQueryable<T> query, Expression<Func<T, decimal>> selector, CancellationToken cancellationToken);

		/// <summary>
		/// Asynchronously computes the sum of a projected sequence of nullable <see cref="decimal"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the sum.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the projected values.</returns>
		public abstract Task<decimal?> SumAsync<T>(IQueryable<T> query, Expression<Func<T, decimal?>> selector);

		/// <summary>
		/// Asynchronously computes the sum of a projected sequence of nullable <see cref="decimal"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the sum.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the sum of the projected values.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public abstract Task<decimal?> SumAsync<T>(IQueryable<T> query, Expression<Func<T, decimal?>> selector, CancellationToken cancellationToken);

		/// <summary>
		/// Asynchronously computes the average of a sequence of <see cref="int"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of <see cref="int"/> values to calculate the average of.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the values in the sequence.</returns>
		public abstract Task<double> AverageAsync(IQueryable<int> query);

		/// <summary>
		/// Asynchronously computes the average of a sequence of <see cref="int"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of <see cref="int"/> values to calculate the average of.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the values in the sequence.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public abstract Task<double> AverageAsync(IQueryable<int> query, CancellationToken cancellationToken);

		/// <summary>
		/// Asynchronously computes the average of a sequence of nullable <see cref="int"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of nullable <see cref="int"/> values to calculate the average of.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the values in the sequence.</returns>
		public abstract Task<double?> AverageAsync(IQueryable<int?> query);

		/// <summary>
		/// Asynchronously computes the average of a sequence of nullable <see cref="int"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of nullable <see cref="int"/> values to calculate the average of.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the values in the sequence.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public abstract Task<double?> AverageAsync(IQueryable<int?> query, CancellationToken cancellationToken);

		/// <summary>
		/// Asynchronously computes the average of a sequence of <see cref="long"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of <see cref="long"/> values to calculate the average of.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the values in the sequence.</returns>
		public abstract Task<double> AverageAsync(IQueryable<long> query);

		/// <summary>
		/// Asynchronously computes the average of a sequence of <see cref="long"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of <see cref="long"/> values to calculate the average of.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the values in the sequence.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public abstract Task<double> AverageAsync(IQueryable<long> query, CancellationToken cancellationToken);

		/// <summary>
		/// Asynchronously computes the average of a sequence of nullable <see cref="long"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of nullable <see cref="long"/> values to calculate the average of.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the values in the sequence.</returns>
		public abstract Task<double?> AverageAsync(IQueryable<long?> query);

		/// <summary>
		/// Asynchronously computes the average of a sequence of nullable <see cref="long"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of nullable <see cref="long"/> values to calculate the average of.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the values in the sequence.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public abstract Task<double?> AverageAsync(IQueryable<long?> query, CancellationToken cancellationToken);

		/// <summary>
		/// Asynchronously computes the average of a sequence of <see cref="float"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of <see cref="float"/> values to calculate the average of.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the values in the sequence.</returns>
		public abstract Task<float> AverageAsync(IQueryable<float> query);

		/// <summary>
		/// Asynchronously computes the average of a sequence of <see cref="float"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of <see cref="float"/> values to calculate the average of.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the values in the sequence.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public abstract Task<float> AverageAsync(IQueryable<float> query, CancellationToken cancellationToken);

		/// <summary>
		/// Asynchronously computes the average of a sequence of nullable <see cref="float"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of nullable <see cref="float"/> values to calculate the average of.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the values in the sequence.</returns>
		public abstract Task<float?> AverageAsync(IQueryable<float?> query);

		/// <summary>
		/// Asynchronously computes the average of a sequence of nullable <see cref="float"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of nullable <see cref="float"/> values to calculate the average of.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the values in the sequence.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public abstract Task<float?> AverageAsync(IQueryable<float?> query, CancellationToken cancellationToken);

		/// <summary>
		/// Asynchronously computes the average of a sequence of <see cref="double"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of <see cref="double"/> values to calculate the average of.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the values in the sequence.</returns>
		public abstract Task<double> AverageAsync(IQueryable<double> query);

		/// <summary>
		/// Asynchronously computes the average of a sequence of <see cref="double"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of <see cref="double"/> values to calculate the average of.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the values in the sequence.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public abstract Task<double> AverageAsync(IQueryable<double> query, CancellationToken cancellationToken);

		/// <summary>
		/// Asynchronously computes the average of a sequence of nullable <see cref="double"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of nullable <see cref="double"/> values to calculate the average of.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the values in the sequence.</returns>
		public abstract Task<double?> AverageAsync(IQueryable<double?> query);

		/// <summary>
		/// Asynchronously computes the average of a sequence of nullable <see cref="double"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of nullable <see cref="double"/> values to calculate the average of.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the values in the sequence.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public abstract Task<double?> AverageAsync(IQueryable<double?> query, CancellationToken cancellationToken);

		/// <summary>
		/// Asynchronously computes the average of a sequence of <see cref="decimal"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of <see cref="decimal"/> values to calculate the average of.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the values in the sequence.</returns>
		public abstract Task<decimal> AverageAsync(IQueryable<decimal> query);

		/// <summary>
		/// Asynchronously computes the average of a sequence of <see cref="decimal"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of <see cref="decimal"/> values to calculate the average of.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the values in the sequence.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public abstract Task<decimal> AverageAsync(IQueryable<decimal> query, CancellationToken cancellationToken);

		/// <summary>
		/// Asynchronously computes the average of a sequence of nullable <see cref="decimal"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of nullable <see cref="decimal"/> values to calculate the average of.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the values in the sequence.</returns>
		public abstract Task<decimal?> AverageAsync(IQueryable<decimal?> query);

		/// <summary>
		/// Asynchronously computes the average of a sequence of nullable <see cref="decimal"/> values.
		/// </summary>
		/// <param name="query">An <see cref="IQueryable{T}"/> of nullable <see cref="decimal"/> values to calculate the average of.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the values in the sequence.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public abstract Task<decimal?> AverageAsync(IQueryable<decimal?> query, CancellationToken cancellationToken);

		/// <summary>
		/// Asynchronously computes the average of a projected sequence of <see cref="int"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the average.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the projected values.</returns>
		public abstract Task<double> AverageAsync<T>(IQueryable<T> query, Expression<Func<T, int>> selector);

		/// <summary>
		/// Asynchronously computes the average of a projected sequence of <see cref="int"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the average.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the projected values.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public abstract Task<double> AverageAsync<T>(IQueryable<T> query, Expression<Func<T, int>> selector, CancellationToken cancellationToken);

		/// <summary>
		/// Asynchronously computes the average of a projected sequence of nullable <see cref="int"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the average.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the projected values.</returns>
		public abstract Task<double?> AverageAsync<T>(IQueryable<T> query, Expression<Func<T, int?>> selector);

		/// <summary>
		/// Asynchronously computes the average of a projected sequence of nullable <see cref="int"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the average.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the projected values.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public abstract Task<double?> AverageAsync<T>(IQueryable<T> query, Expression<Func<T, int?>> selector, CancellationToken cancellationToken);

		/// <summary>
		/// Asynchronously computes the average of a projected sequence of <see cref="long"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the average.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the projected values.</returns>
		public abstract Task<double> AverageAsync<T>(IQueryable<T> query, Expression<Func<T, long>> selector);

		/// <summary>
		/// Asynchronously computes the average of a projected sequence of <see cref="long"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the average.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the projected values.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public abstract Task<double> AverageAsync<T>(IQueryable<T> query, Expression<Func<T, long>> selector, CancellationToken cancellationToken);

		/// <summary>
		/// Asynchronously computes the average of a projected sequence of nullable <see cref="long"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the average.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the projected values.</returns>
		public abstract Task<double?> AverageAsync<T>(IQueryable<T> query, Expression<Func<T, long?>> selector);

		/// <summary>
		/// Asynchronously computes the average of a projected sequence of nullable <see cref="long"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the average.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the projected values.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public abstract Task<double?> AverageAsync<T>(IQueryable<T> query, Expression<Func<T, long?>> selector, CancellationToken cancellationToken);

		/// <summary>
		/// Asynchronously computes the average of a projected sequence of <see cref="float"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the average.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the projected values.</returns>
		public abstract Task<float> AverageAsync<T>(IQueryable<T> query, Expression<Func<T, float>> selector);

		/// <summary>
		/// Asynchronously computes the average of a projected sequence of <see cref="float"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the average.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the projected values.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public abstract Task<float> AverageAsync<T>(IQueryable<T> query, Expression<Func<T, float>> selector, CancellationToken cancellationToken);

		/// <summary>
		/// Asynchronously computes the average of a projected sequence of nullable <see cref="float"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the average.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the projected values.</returns>
		public abstract Task<float?> AverageAsync<T>(IQueryable<T> query, Expression<Func<T, float?>> selector);

		/// <summary>
		/// Asynchronously computes the average of a projected sequence of nullable <see cref="float"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the average.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the projected values.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public abstract Task<float?> AverageAsync<T>(IQueryable<T> query, Expression<Func<T, float?>> selector, CancellationToken cancellationToken);

		/// <summary>
		/// Asynchronously computes the average of a projected sequence of <see cref="double"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the average.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the projected values.</returns>
		public abstract Task<double> AverageAsync<T>(IQueryable<T> query, Expression<Func<T, double>> selector);

		/// <summary>
		/// Asynchronously computes the average of a projected sequence of <see cref="double"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the average.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the projected values.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public abstract Task<double> AverageAsync<T>(IQueryable<T> query, Expression<Func<T, double>> selector, CancellationToken cancellationToken);

		/// <summary>
		/// Asynchronously computes the average of a projected sequence of nullable <see cref="double"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the average.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the projected values.</returns>
		public abstract Task<double?> AverageAsync<T>(IQueryable<T> query, Expression<Func<T, double?>> selector);

		/// <summary>
		/// Asynchronously computes the average of a projected sequence of nullable <see cref="double"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the average.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the projected values.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public abstract Task<double?> AverageAsync<T>(IQueryable<T> query, Expression<Func<T, double?>> selector, CancellationToken cancellationToken);

		/// <summary>
		/// Asynchronously computes the average of a projected sequence of <see cref="decimal"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the average.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the projected values.</returns>
		public abstract Task<decimal> AverageAsync<T>(IQueryable<T> query, Expression<Func<T, decimal>> selector);

		/// <summary>
		/// Asynchronously computes the average of a projected sequence of <see cref="decimal"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the average.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the projected values.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public abstract Task<decimal> AverageAsync<T>(IQueryable<T> query, Expression<Func<T, decimal>> selector, CancellationToken cancellationToken);

		/// <summary>
		/// Asynchronously computes the average of a projected sequence of nullable <see cref="decimal"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the average.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the projected values.</returns>
		public abstract Task<decimal?> AverageAsync<T>(IQueryable<T> query, Expression<Func<T, decimal?>> selector);

		/// <summary>
		/// Asynchronously computes the average of a projected sequence of nullable <see cref="decimal"/> values.
		/// </summary>
		/// <typeparam name="T">The type of the elements of <paramref name="query"/>.</typeparam>
		/// <param name="query">An <see cref="IQueryable{T}"/> whose elements are projected before calculating the average.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the average of the projected values.</returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken"/> is canceled.</exception>
		public abstract Task<decimal?> AverageAsync<T>(IQueryable<T> query, Expression<Func<T, decimal?>> selector, CancellationToken cancellationToken);
	}
}
