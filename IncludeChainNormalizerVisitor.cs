using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Grammophone.DataAccess.QueryExtensions;

namespace Grammophone.DataAccess
{
	/// <summary>
	/// Normalizes portable include chains into portable string includes.
	/// </summary>
	public class IncludeChainNormalizerVisitor : ExpressionVisitor
	{
		#region Protected methods

		/// <inheritdoc/>
		protected override Expression VisitMethodCall(MethodCallExpression node)
		{
			if (node == null) throw new ArgumentNullException(nameof(node));

			if (IsThenIncludeMethod(node.Method))
			{
				var includeChain = ParseIncludeChain(node);

				return Expression.Call(
					null,
					QueryExtensionMethodInfos.IncludeString.MakeGenericMethod(includeChain.EntityType),
					Visit(includeChain.SourceExpression),
					Expression.Constant(includeChain.Path));
			}

			return base.VisitMethodCall(node);
		}

		#endregion

		#region Private methods

		private static IncludeChain ParseIncludeChain(MethodCallExpression node)
		{
			if (IsThenIncludeMethod(node.Method))
			{
				var previousChain = ParseIncludeChain((MethodCallExpression)node.Arguments[0]);
				var path = ExtractPath(node.Arguments[1]);

				return new IncludeChain(
					previousChain.EntityType,
					previousChain.SourceExpression,
					$"{previousChain.Path}.{path}");
			}

			var methodInfo = GetMethodInfoKey(node.Method);

			if (methodInfo == QueryExtensionMethodInfos.IncludeExpression)
			{
				return new IncludeChain(
					node.Method.GetGenericArguments()[0],
					node.Arguments[0],
					ExtractPath(node.Arguments[1]));
			}

			if (methodInfo == QueryExtensionMethodInfos.IncludeString)
			{
				return new IncludeChain(
					node.Method.GetGenericArguments()[0],
					node.Arguments[0],
					ExtractStringPath(node.Arguments[1]));
			}

			throw new NotSupportedException("The ThenInclude call is not rooted in a portable Include call.");
		}

		private static string ExtractPath(Expression expression)
		{
			if (expression is UnaryExpression unaryExpression && expression.NodeType == ExpressionType.Quote)
			{
				expression = unaryExpression.Operand;
			}

			if (!(expression is LambdaExpression lambdaExpression))
			{
				throw new NotSupportedException("Include path expressions must be lambda expressions.");
			}

			return ExtractPathFromBody(lambdaExpression.Body);
		}

		private static string ExtractPathFromBody(Expression expression)
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

		private static string ExtractStringPath(Expression expression)
		{
			if (expression is ConstantExpression constantExpression && constantExpression.Value is string path)
			{
				return path;
			}

			throw new NotSupportedException("Portable string Include paths must be constants before ThenInclude normalization.");
		}

		private static bool IsThenIncludeMethod(MethodInfo methodInfo)
		{
			var methodInfoKey = GetMethodInfoKey(methodInfo);

			return methodInfoKey == QueryExtensionMethodInfos.ThenIncludeReference
				|| methodInfoKey == QueryExtensionMethodInfos.ThenIncludeCollection
				|| methodInfoKey == QueryExtensionMethodInfos.ThenIncludeICollection;
		}

		private static MethodInfo GetMethodInfoKey(MethodInfo methodInfo)
		{
			if (methodInfo.IsGenericMethod && !methodInfo.IsGenericMethodDefinition)
			{
				return methodInfo.GetGenericMethodDefinition();
			}

			return methodInfo;
		}

		#endregion

		#region Private types

		private sealed class IncludeChain
		{
			public IncludeChain(Type entityType, Expression sourceExpression, string path)
			{
				if (entityType == null) throw new ArgumentNullException(nameof(entityType));
				if (sourceExpression == null) throw new ArgumentNullException(nameof(sourceExpression));
				if (path == null) throw new ArgumentNullException(nameof(path));

				this.EntityType = entityType;
				this.SourceExpression = sourceExpression;
				this.Path = path;
			}

			public Type EntityType { get; }

			public Expression SourceExpression { get; }

			public string Path { get; }
		}

		#endregion
	}
}
