using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Text;

namespace Grammophone.DataAccess
{
	/// <summary>
	/// Base query provider which translates portable query expressions to native provider expressions.
	/// </summary>
	public abstract class TranslatingQueryProvider : IQueryProvider
	{
		#region Construction

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="nativeQueryProvider">The underlying native query provider.</param>
		/// <param name="domainContainer">The domain container which owns the query.</param>
		protected TranslatingQueryProvider(IQueryProvider nativeQueryProvider, IDomainContainer domainContainer)
		{
			if (nativeQueryProvider == null) throw new ArgumentNullException(nameof(nativeQueryProvider));
			if (domainContainer == null) throw new ArgumentNullException(nameof(domainContainer));

			this.NativeQueryProvider = nativeQueryProvider;
			this.DomainContainer = domainContainer;
		}

		#endregion

		#region Public properties

		/// <summary>
		/// The underlying native query provider.
		/// </summary>
		public IQueryProvider NativeQueryProvider { get; }

		/// <summary>
		/// The domain container which owns the query.
		/// </summary>
		public IDomainContainer DomainContainer { get; }

		#endregion

		#region IQueryProvider implementation

		/// <summary>
		/// Cached generic method definition of <see cref="CreateQuery{TElement}(Expression)"/>, used by the
		/// non-generic <see cref="CreateQuery(Expression)"/> to preserve the element type of the query.
		/// </summary>
		private static readonly MethodInfo genericCreateQueryMethodDefinition =
			typeof(TranslatingQueryProvider).GetMethods()
				.Single(method => method.Name == nameof(CreateQuery) && method.IsGenericMethodDefinition);

		/// <summary>
		/// Constructs an <see cref="IQueryable"/> object that can evaluate the translated query represented by a specified expression tree.
		/// </summary>
		/// <param name="expression">An expression tree that represents a query.</param>
		/// <returns>An <see cref="IQueryable"/> that can evaluate the query.</returns>
		/// <remarks>
		/// When <paramref name="expression"/> is of type <see cref="IQueryable{T}"/>, the returned query is guaranteed to be
		/// assignable to <see cref="IQueryable{T}"/>: the element type is recovered from the expression and the call is
		/// delegated to <see cref="CreateQuery{TElement}(Expression)"/>. This matches the behavior of the BCL's own
		/// <c>EnumerableQuery</c> and of Entity Framework, whose non-generic providers likewise preserve the element type.
		/// </remarks>
		public IQueryable CreateQuery(Expression expression)
		{
			if (expression == null) throw new ArgumentNullException(nameof(expression));

			Type elementType = TryGetQueryableElementType(expression.Type);

			if (elementType != null)
			{
				try
				{
					return (IQueryable)genericCreateQueryMethodDefinition
						.MakeGenericMethod(elementType)
						.Invoke(this, new object[] { expression });
				}
				catch (TargetInvocationException ex) when (ex.InnerException != null)
				{
					ExceptionDispatchInfo.Capture(ex.InnerException).Throw();

					throw; // Unreachable; the line above always rethrows.
				}
			}

			// The expression has no IQueryable<T>/IEnumerable<T> element type: keep the original non-generic behavior.
			var translatedExpression = TranslateExpression(expression);

			return WrapNativeQuery(this.NativeQueryProvider.CreateQuery(translatedExpression));
		}

		/// <summary>
		/// Constructs an <see cref="IQueryable{T}"/> object that can evaluate the translated query represented by a specified expression tree.
		/// </summary>
		/// <typeparam name="TElement">The type of the elements of the query.</typeparam>
		/// <param name="expression">An expression tree that represents a query.</param>
		/// <returns>An <see cref="IQueryable{T}"/> that can evaluate the query.</returns>
		public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
		{
			if (expression == null) throw new ArgumentNullException(nameof(expression));

			var translatedExpression = TranslateExpression(expression);

			return WrapNativeQuery(this.NativeQueryProvider.CreateQuery<TElement>(translatedExpression));
		}

		/// <summary>
		/// Executes the translated query represented by a specified expression tree.
		/// </summary>
		/// <param name="expression">An expression tree that represents a query.</param>
		/// <returns>The value that results from executing the query.</returns>
		public object Execute(Expression expression)
		{
			if (expression == null) throw new ArgumentNullException(nameof(expression));

			var translatedExpression = TranslateExpression(expression);

			return this.NativeQueryProvider.Execute(translatedExpression);
		}

		/// <summary>
		/// Executes the strongly typed translated query represented by a specified expression tree.
		/// </summary>
		/// <typeparam name="TResult">The type of the result of executing the query.</typeparam>
		/// <param name="expression">An expression tree that represents a query.</param>
		/// <returns>The value that results from executing the query.</returns>
		public TResult Execute<TResult>(Expression expression)
		{
			if (expression == null) throw new ArgumentNullException(nameof(expression));

			var translatedExpression = TranslateExpression(expression);

			return this.NativeQueryProvider.Execute<TResult>(translatedExpression);
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Translates a portable expression tree into an expression tree understood by the native provider.
		/// </summary>
		/// <param name="expression">The portable expression tree.</param>
		/// <returns>The translated expression tree.</returns>
		public virtual Expression TranslateExpression(Expression expression)
		{
			if (expression == null) throw new ArgumentNullException(nameof(expression));

			var queryTranslator = this.DomainContainer.TryGetQueryTranslator();

			if (queryTranslator == null)
			{
				return expression;
			}

			var normalizedExpression = new IncludeChainNormalizerVisitor().Visit(expression);

			return new MethodMappingExpressionVisitor(queryTranslator.MethodMappingsByMethodInfo).Visit(normalizedExpression);
		}

		#endregion

		#region Protected methods

		/// <summary>
		/// Wraps a native queryable into an abstraction queryable.
		/// </summary>
		/// <param name="nativeQueryable">The native queryable.</param>
		/// <returns>The wrapped queryable.</returns>
		protected abstract IEntityQuery WrapNativeQuery(IQueryable nativeQueryable);

		/// <summary>
		/// Wraps a native queryable into a strongly typed abstraction queryable.
		/// </summary>
		/// <typeparam name="T">The element type of the queryable.</typeparam>
		/// <param name="nativeQueryable">The native queryable.</param>
		/// <returns>The wrapped queryable.</returns>
		protected abstract IEntityQuery<T> WrapNativeQuery<T>(IQueryable<T> nativeQueryable);

		#endregion

		#region Private methods

		/// <summary>
		/// If <paramref name="type"/> represents a queryable/enumerable sequence, returns its element type; otherwise <see langword="null"/>.
		/// </summary>
		/// <param name="type">The type to inspect, typically the <see cref="Expression.Type"/> of a query expression.</param>
		/// <returns>The element type, or <see langword="null"/> if <paramref name="type"/> is not a generic sequence.</returns>
		private static Type TryGetQueryableElementType(Type type)
		{
			if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IQueryable<>))
				return type.GetGenericArguments()[0];

			foreach (var interfaceType in type.GetInterfaces())
			{
				if (interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
					return interfaceType.GetGenericArguments()[0];
			}

			return null;
		}

		#endregion
	}
}
