using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Grammophone.DataAccess
{
	public abstract class TranslatingQueryProvider : IQueryProvider
	{
		#region Construction

		protected TranslatingQueryProvider(IQueryProvider nativeQueryProvider, IDomainContainer domainContainer)
		{
			if (nativeQueryProvider == null) throw new ArgumentNullException(nameof(nativeQueryProvider));
			if (domainContainer == null) throw new ArgumentNullException(nameof(domainContainer));

			this.NativeQueryProvider = nativeQueryProvider;
			this.DomainContainer = domainContainer;
		}

		#endregion

		#region Public properties

		public IQueryProvider NativeQueryProvider { get; }

		public IDomainContainer DomainContainer { get; }

		#endregion

		#region IQueryProvider implementation

		public IQueryable CreateQuery(Expression expression)
		{
			if (expression == null) throw new ArgumentNullException(nameof(expression));

			return WrapNativeQuery(this.NativeQueryProvider.CreateQuery(expression));
		}

		public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
		{
			if (expression == null) throw new ArgumentNullException(nameof(expression));

			return WrapNativeQuery(this.NativeQueryProvider.CreateQuery<TElement>(expression));
		}

		public object Execute(Expression expression)
		{
			var translatedExpression = TranslateExpression(expression);

			return this.NativeQueryProvider.Execute(expression);
		}

		public TResult Execute<TResult>(Expression expression)
		{
			var translatedExpression = TranslateExpression(expression);

			return this.NativeQueryProvider.Execute<TResult>(expression);
		}

		#endregion

		#region Public methods

		public virtual Expression TranslateExpression(Expression expression)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region Protected methods

		protected abstract IEntityQuery WrapNativeQuery(IQueryable nativeQueryable);

		protected abstract IEntityQuery<T> WrapNativeQuery<T>(IQueryable<T> nativeQueryable);

		#endregion
	}
}
