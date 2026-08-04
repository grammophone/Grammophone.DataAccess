using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Grammophone.DataAccess.QueryExtensions
{
	/// <summary>
	/// Extracts navigation paths from simple member-access expressions.
	/// </summary>
	internal static class NavigationPath
	{
		#region Public methods

		/// <summary>
		/// Extract a dot-separated navigation path.
		/// </summary>
		public static string Extract(LambdaExpression expression)
		{
			if (expression == null) throw new ArgumentNullException(nameof(expression));

			return ExtractFromBody(expression.Body);
		}

		#endregion

		#region Private methods

		private static string ExtractFromBody(Expression expression)
		{
			var members = new Stack<string>();

			while (expression != null)
			{
				switch (expression.NodeType)
				{
					case ExpressionType.MemberAccess:
						var memberExpression = (MemberExpression)expression;
						members.Push(memberExpression.Member.Name);
						expression = memberExpression.Expression;
						break;

					case ExpressionType.Convert:
					case ExpressionType.ConvertChecked:
						expression = ((UnaryExpression)expression).Operand;
						break;

					case ExpressionType.Parameter:
						return string.Join(".", members);

					default:
						throw new NotSupportedException(
							"Portable Include and ThenInclude support only simple navigation member paths.");
				}
			}

			throw new NotSupportedException("The include expression does not describe a navigation member path.");
		}

		#endregion
	}
}
