using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Grammophone.DataAccess
{
	/// <summary>
	/// A default implementation of all adapted methods.
	/// Provider implementations can override adaptations to provide more efficient implementations.
	/// </summary>
	public class DefaultTerminalMethodsAdapter : TerminalMethodsAdapter
	{
		/// <inheritdoc/>
		public override Task<bool> AllAsync<T>(IQueryable<T> query, Expression<Func<T, bool>> predicate) => AllAsync(query, predicate, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<bool> AllAsync<T>(IQueryable<T> query, Expression<Func<T, bool>> predicate, CancellationToken cancellationToken) => Execute(() => query.All(predicate), cancellationToken);

		/// <inheritdoc/>
		public override Task<bool> AnyAsync<T>(IQueryable<T> query) => AnyAsync(query, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<bool> AnyAsync<T>(IQueryable<T> query, CancellationToken cancellationToken) => Execute(query.Any, cancellationToken);

		/// <inheritdoc/>
		public override Task<bool> AnyAsync<T>(IQueryable<T> query, Expression<Func<T, bool>> predicate) => AnyAsync(query, predicate, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<bool> AnyAsync<T>(IQueryable<T> query, Expression<Func<T, bool>> predicate, CancellationToken cancellationToken) => Execute(() => query.Any(predicate), cancellationToken);

		/// <inheritdoc/>
		public override Task<int> CountAsync<T>(IQueryable<T> query) => CountAsync(query, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<int> CountAsync<T>(IQueryable<T> query, CancellationToken cancellationToken) => Execute(query.Count, cancellationToken);

		/// <inheritdoc/>
		public override Task<int> CountAsync<T>(IQueryable<T> query, Expression<Func<T, bool>> predicate) => CountAsync(query, predicate, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<int> CountAsync<T>(IQueryable<T> query, Expression<Func<T, bool>> predicate, CancellationToken cancellationToken) => Execute(() => query.Count(predicate), cancellationToken);

		/// <inheritdoc/>
		public override Task<long> LongCountAsync<T>(IQueryable<T> query) => LongCountAsync(query, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<long> LongCountAsync<T>(IQueryable<T> query, CancellationToken cancellationToken) => Execute(query.LongCount, cancellationToken);

		/// <inheritdoc/>
		public override Task<long> LongCountAsync<T>(IQueryable<T> query, Expression<Func<T, bool>> predicate) => LongCountAsync(query, predicate, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<long> LongCountAsync<T>(IQueryable<T> query, Expression<Func<T, bool>> predicate, CancellationToken cancellationToken) => Execute(() => query.LongCount(predicate), cancellationToken);

		/// <inheritdoc/>
		public override Task<T> FirstAsync<T>(IQueryable<T> query) => FirstAsync(query, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<T> FirstAsync<T>(IQueryable<T> query, CancellationToken cancellationToken) => Execute(query.First, cancellationToken);

		/// <inheritdoc/>
		public override Task<T> FirstAsync<T>(IQueryable<T> query, Expression<Func<T, bool>> predicate) => FirstAsync(query, predicate, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<T> FirstAsync<T>(IQueryable<T> query, Expression<Func<T, bool>> predicate, CancellationToken cancellationToken) => Execute(() => query.First(predicate), cancellationToken);

		/// <inheritdoc/>
		public override Task<T> FirstOrDefaultAsync<T>(IQueryable<T> query) => FirstOrDefaultAsync(query, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<T> FirstOrDefaultAsync<T>(IQueryable<T> query, CancellationToken cancellationToken) => Execute(query.FirstOrDefault, cancellationToken);

		/// <inheritdoc/>
		public override Task<T> FirstOrDefaultAsync<T>(IQueryable<T> query, Expression<Func<T, bool>> predicate) => FirstOrDefaultAsync(query, predicate, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<T> FirstOrDefaultAsync<T>(IQueryable<T> query, Expression<Func<T, bool>> predicate, CancellationToken cancellationToken) => Execute(() => query.FirstOrDefault(predicate), cancellationToken);

		/// <inheritdoc/>
		public override Task<T> SingleAsync<T>(IQueryable<T> query) => SingleAsync(query, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<T> SingleAsync<T>(IQueryable<T> query, CancellationToken cancellationToken) => Execute(query.Single, cancellationToken);

		/// <inheritdoc/>
		public override Task<T> SingleAsync<T>(IQueryable<T> query, Expression<Func<T, bool>> predicate) => SingleAsync(query, predicate, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<T> SingleAsync<T>(IQueryable<T> query, Expression<Func<T, bool>> predicate, CancellationToken cancellationToken) => Execute(() => query.Single(predicate), cancellationToken);

		/// <inheritdoc/>
		public override Task<T> SingleOrDefaultAsync<T>(IQueryable<T> query) => SingleOrDefaultAsync(query, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<T> SingleOrDefaultAsync<T>(IQueryable<T> query, CancellationToken cancellationToken) => Execute(query.SingleOrDefault, cancellationToken);

		/// <inheritdoc/>
		public override Task<T> SingleOrDefaultAsync<T>(IQueryable<T> query, Expression<Func<T, bool>> predicate) => SingleOrDefaultAsync(query, predicate, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<T> SingleOrDefaultAsync<T>(IQueryable<T> query, Expression<Func<T, bool>> predicate, CancellationToken cancellationToken) => Execute(() => query.SingleOrDefault(predicate), cancellationToken);

		/// <inheritdoc/>
		public override Task<T[]> ToArrayAsync<T>(IQueryable<T> query) => ToArrayAsync(query, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<T[]> ToArrayAsync<T>(IQueryable<T> query, CancellationToken cancellationToken) => Execute(query.ToArray, cancellationToken);

		/// <inheritdoc/>
		public override Task<List<T>> ToListAsync<T>(IQueryable<T> query) => ToListAsync(query, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<List<T>> ToListAsync<T>(IQueryable<T> query, CancellationToken cancellationToken) => Execute(query.ToList, cancellationToken);

		/// <inheritdoc/>
		public override Task<Dictionary<TKey, T>> ToDictionaryAsync<T, TKey>(IQueryable<T> query, Func<T, TKey> keySelector) => ToDictionaryAsync(query, keySelector, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<Dictionary<TKey, T>> ToDictionaryAsync<T, TKey>(IQueryable<T> query, Func<T, TKey> keySelector, CancellationToken cancellationToken) => Execute(() => query.ToDictionary(keySelector), cancellationToken);

		/// <inheritdoc/>
		public override Task<T> MinAsync<T>(IQueryable<T> query) => MinAsync(query, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<T> MinAsync<T>(IQueryable<T> query, CancellationToken cancellationToken) => Execute(query.Min, cancellationToken);

		/// <inheritdoc/>
		public override Task<TResult> MinAsync<T, TResult>(IQueryable<T> query, Expression<Func<T, TResult>> selector) => MinAsync(query, selector, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<TResult> MinAsync<T, TResult>(IQueryable<T> query, Expression<Func<T, TResult>> selector, CancellationToken cancellationToken) => Execute(() => query.Min(selector), cancellationToken);

		/// <inheritdoc/>
		public override Task<T> MaxAsync<T>(IQueryable<T> query) => MaxAsync(query, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<T> MaxAsync<T>(IQueryable<T> query, CancellationToken cancellationToken) => Execute(query.Max, cancellationToken);

		/// <inheritdoc/>
		public override Task<TResult> MaxAsync<T, TResult>(IQueryable<T> query, Expression<Func<T, TResult>> selector) => MaxAsync(query, selector, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<TResult> MaxAsync<T, TResult>(IQueryable<T> query, Expression<Func<T, TResult>> selector, CancellationToken cancellationToken) => Execute(() => query.Max(selector), cancellationToken);

		/// <inheritdoc/>
		public override Task<int> SumAsync(IQueryable<int> query) => SumAsync(query, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<int> SumAsync(IQueryable<int> query, CancellationToken cancellationToken) => Execute(query.Sum, cancellationToken);

		/// <inheritdoc/>
		public override Task<int?> SumAsync(IQueryable<int?> query) => SumAsync(query, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<int?> SumAsync(IQueryable<int?> query, CancellationToken cancellationToken) => Execute(query.Sum, cancellationToken);

		/// <inheritdoc/>
		public override Task<long> SumAsync(IQueryable<long> query) => SumAsync(query, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<long> SumAsync(IQueryable<long> query, CancellationToken cancellationToken) => Execute(query.Sum, cancellationToken);

		/// <inheritdoc/>
		public override Task<long?> SumAsync(IQueryable<long?> query) => SumAsync(query, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<long?> SumAsync(IQueryable<long?> query, CancellationToken cancellationToken) => Execute(query.Sum, cancellationToken);

		/// <inheritdoc/>
		public override Task<float> SumAsync(IQueryable<float> query) => SumAsync(query, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<float> SumAsync(IQueryable<float> query, CancellationToken cancellationToken) => Execute(query.Sum, cancellationToken);

		/// <inheritdoc/>
		public override Task<float?> SumAsync(IQueryable<float?> query) => SumAsync(query, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<float?> SumAsync(IQueryable<float?> query, CancellationToken cancellationToken) => Execute(query.Sum, cancellationToken);

		/// <inheritdoc/>
		public override Task<double> SumAsync(IQueryable<double> query) => SumAsync(query, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<double> SumAsync(IQueryable<double> query, CancellationToken cancellationToken) => Execute(query.Sum, cancellationToken);

		/// <inheritdoc/>
		public override Task<double?> SumAsync(IQueryable<double?> query) => SumAsync(query, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<double?> SumAsync(IQueryable<double?> query, CancellationToken cancellationToken) => Execute(query.Sum, cancellationToken);

		/// <inheritdoc/>
		public override Task<decimal> SumAsync(IQueryable<decimal> query) => SumAsync(query, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<decimal> SumAsync(IQueryable<decimal> query, CancellationToken cancellationToken) => Execute(query.Sum, cancellationToken);

		/// <inheritdoc/>
		public override Task<decimal?> SumAsync(IQueryable<decimal?> query) => SumAsync(query, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<decimal?> SumAsync(IQueryable<decimal?> query, CancellationToken cancellationToken) => Execute(query.Sum, cancellationToken);

		/// <inheritdoc/>
		public override Task<int> SumAsync<T>(IQueryable<T> query, Expression<Func<T, int>> selector) => SumAsync(query, selector, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<int> SumAsync<T>(IQueryable<T> query, Expression<Func<T, int>> selector, CancellationToken cancellationToken) => Execute(() => query.Sum(selector), cancellationToken);

		/// <inheritdoc/>
		public override Task<int?> SumAsync<T>(IQueryable<T> query, Expression<Func<T, int?>> selector) => SumAsync(query, selector, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<int?> SumAsync<T>(IQueryable<T> query, Expression<Func<T, int?>> selector, CancellationToken cancellationToken) => Execute(() => query.Sum(selector), cancellationToken);

		/// <inheritdoc/>
		public override Task<long> SumAsync<T>(IQueryable<T> query, Expression<Func<T, long>> selector) => SumAsync(query, selector, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<long> SumAsync<T>(IQueryable<T> query, Expression<Func<T, long>> selector, CancellationToken cancellationToken) => Execute(() => query.Sum(selector), cancellationToken);

		/// <inheritdoc/>
		public override Task<long?> SumAsync<T>(IQueryable<T> query, Expression<Func<T, long?>> selector) => SumAsync(query, selector, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<long?> SumAsync<T>(IQueryable<T> query, Expression<Func<T, long?>> selector, CancellationToken cancellationToken) => Execute(() => query.Sum(selector), cancellationToken);

		/// <inheritdoc/>
		public override Task<float> SumAsync<T>(IQueryable<T> query, Expression<Func<T, float>> selector) => SumAsync(query, selector, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<float> SumAsync<T>(IQueryable<T> query, Expression<Func<T, float>> selector, CancellationToken cancellationToken) => Execute(() => query.Sum(selector), cancellationToken);

		/// <inheritdoc/>
		public override Task<float?> SumAsync<T>(IQueryable<T> query, Expression<Func<T, float?>> selector) => SumAsync(query, selector, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<float?> SumAsync<T>(IQueryable<T> query, Expression<Func<T, float?>> selector, CancellationToken cancellationToken) => Execute(() => query.Sum(selector), cancellationToken);

		/// <inheritdoc/>
		public override Task<double> SumAsync<T>(IQueryable<T> query, Expression<Func<T, double>> selector) => SumAsync(query, selector, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<double> SumAsync<T>(IQueryable<T> query, Expression<Func<T, double>> selector, CancellationToken cancellationToken) => Execute(() => query.Sum(selector), cancellationToken);

		/// <inheritdoc/>
		public override Task<double?> SumAsync<T>(IQueryable<T> query, Expression<Func<T, double?>> selector) => SumAsync(query, selector, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<double?> SumAsync<T>(IQueryable<T> query, Expression<Func<T, double?>> selector, CancellationToken cancellationToken) => Execute(() => query.Sum(selector), cancellationToken);

		/// <inheritdoc/>
		public override Task<decimal> SumAsync<T>(IQueryable<T> query, Expression<Func<T, decimal>> selector) => SumAsync(query, selector, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<decimal> SumAsync<T>(IQueryable<T> query, Expression<Func<T, decimal>> selector, CancellationToken cancellationToken) => Execute(() => query.Sum(selector), cancellationToken);

		/// <inheritdoc/>
		public override Task<decimal?> SumAsync<T>(IQueryable<T> query, Expression<Func<T, decimal?>> selector) => SumAsync(query, selector, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<decimal?> SumAsync<T>(IQueryable<T> query, Expression<Func<T, decimal?>> selector, CancellationToken cancellationToken) => Execute(() => query.Sum(selector), cancellationToken);

		/// <inheritdoc/>
		public override Task<double> AverageAsync(IQueryable<int> query) => AverageAsync(query, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<double> AverageAsync(IQueryable<int> query, CancellationToken cancellationToken) => Execute(query.Average, cancellationToken);

		/// <inheritdoc/>
		public override Task<double?> AverageAsync(IQueryable<int?> query) => AverageAsync(query, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<double?> AverageAsync(IQueryable<int?> query, CancellationToken cancellationToken) => Execute(query.Average, cancellationToken);

		/// <inheritdoc/>
		public override Task<double> AverageAsync(IQueryable<long> query) => AverageAsync(query, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<double> AverageAsync(IQueryable<long> query, CancellationToken cancellationToken) => Execute(query.Average, cancellationToken);

		/// <inheritdoc/>
		public override Task<double?> AverageAsync(IQueryable<long?> query) => AverageAsync(query, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<double?> AverageAsync(IQueryable<long?> query, CancellationToken cancellationToken) => Execute(query.Average, cancellationToken);

		/// <inheritdoc/>
		public override Task<float> AverageAsync(IQueryable<float> query) => AverageAsync(query, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<float> AverageAsync(IQueryable<float> query, CancellationToken cancellationToken) => Execute(query.Average, cancellationToken);

		/// <inheritdoc/>
		public override Task<float?> AverageAsync(IQueryable<float?> query) => AverageAsync(query, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<float?> AverageAsync(IQueryable<float?> query, CancellationToken cancellationToken) => Execute(query.Average, cancellationToken);

		/// <inheritdoc/>
		public override Task<double> AverageAsync(IQueryable<double> query) => AverageAsync(query, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<double> AverageAsync(IQueryable<double> query, CancellationToken cancellationToken) => Execute(query.Average, cancellationToken);

		/// <inheritdoc/>
		public override Task<double?> AverageAsync(IQueryable<double?> query) => AverageAsync(query, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<double?> AverageAsync(IQueryable<double?> query, CancellationToken cancellationToken) => Execute(query.Average, cancellationToken);

		/// <inheritdoc/>
		public override Task<decimal> AverageAsync(IQueryable<decimal> query) => AverageAsync(query, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<decimal> AverageAsync(IQueryable<decimal> query, CancellationToken cancellationToken) => Execute(query.Average, cancellationToken);

		/// <inheritdoc/>
		public override Task<decimal?> AverageAsync(IQueryable<decimal?> query) => AverageAsync(query, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<decimal?> AverageAsync(IQueryable<decimal?> query, CancellationToken cancellationToken) => Execute(query.Average, cancellationToken);

		/// <inheritdoc/>
		public override Task<double> AverageAsync<T>(IQueryable<T> query, Expression<Func<T, int>> selector) => AverageAsync(query, selector, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<double> AverageAsync<T>(IQueryable<T> query, Expression<Func<T, int>> selector, CancellationToken cancellationToken) => Execute(() => query.Average(selector), cancellationToken);

		/// <inheritdoc/>
		public override Task<double?> AverageAsync<T>(IQueryable<T> query, Expression<Func<T, int?>> selector) => AverageAsync(query, selector, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<double?> AverageAsync<T>(IQueryable<T> query, Expression<Func<T, int?>> selector, CancellationToken cancellationToken) => Execute(() => query.Average(selector), cancellationToken);

		/// <inheritdoc/>
		public override Task<double> AverageAsync<T>(IQueryable<T> query, Expression<Func<T, long>> selector) => AverageAsync(query, selector, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<double> AverageAsync<T>(IQueryable<T> query, Expression<Func<T, long>> selector, CancellationToken cancellationToken) => Execute(() => query.Average(selector), cancellationToken);

		/// <inheritdoc/>
		public override Task<double?> AverageAsync<T>(IQueryable<T> query, Expression<Func<T, long?>> selector) => AverageAsync(query, selector, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<double?> AverageAsync<T>(IQueryable<T> query, Expression<Func<T, long?>> selector, CancellationToken cancellationToken) => Execute(() => query.Average(selector), cancellationToken);

		/// <inheritdoc/>
		public override Task<float> AverageAsync<T>(IQueryable<T> query, Expression<Func<T, float>> selector) => AverageAsync(query, selector, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<float> AverageAsync<T>(IQueryable<T> query, Expression<Func<T, float>> selector, CancellationToken cancellationToken) => Execute(() => query.Average(selector), cancellationToken);

		/// <inheritdoc/>
		public override Task<float?> AverageAsync<T>(IQueryable<T> query, Expression<Func<T, float?>> selector) => AverageAsync(query, selector, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<float?> AverageAsync<T>(IQueryable<T> query, Expression<Func<T, float?>> selector, CancellationToken cancellationToken) => Execute(() => query.Average(selector), cancellationToken);

		/// <inheritdoc/>
		public override Task<double> AverageAsync<T>(IQueryable<T> query, Expression<Func<T, double>> selector) => AverageAsync(query, selector, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<double> AverageAsync<T>(IQueryable<T> query, Expression<Func<T, double>> selector, CancellationToken cancellationToken) => Execute(() => query.Average(selector), cancellationToken);

		/// <inheritdoc/>
		public override Task<double?> AverageAsync<T>(IQueryable<T> query, Expression<Func<T, double?>> selector) => AverageAsync(query, selector, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<double?> AverageAsync<T>(IQueryable<T> query, Expression<Func<T, double?>> selector, CancellationToken cancellationToken) => Execute(() => query.Average(selector), cancellationToken);

		/// <inheritdoc/>
		public override Task<decimal> AverageAsync<T>(IQueryable<T> query, Expression<Func<T, decimal>> selector) => AverageAsync(query, selector, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<decimal> AverageAsync<T>(IQueryable<T> query, Expression<Func<T, decimal>> selector, CancellationToken cancellationToken) => Execute(() => query.Average(selector), cancellationToken);

		/// <inheritdoc/>
		public override Task<decimal?> AverageAsync<T>(IQueryable<T> query, Expression<Func<T, decimal?>> selector) => AverageAsync(query, selector, default(CancellationToken));

		/// <inheritdoc/>
		public override Task<decimal?> AverageAsync<T>(IQueryable<T> query, Expression<Func<T, decimal?>> selector, CancellationToken cancellationToken) => Execute(() => query.Average(selector), cancellationToken);

		private static Task<TResult> Execute<TResult>(Func<TResult> function, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			return Task.FromResult(function());
		}
	}
}
