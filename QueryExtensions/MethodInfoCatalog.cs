using System;
using System.Linq;
using System.Reflection;

namespace Grammophone.DataAccess.QueryExtensions
{
	/// <summary>
	/// Helper methods for locating method information used by query extension catalogs.
	/// </summary>
	internal static class MethodInfoCatalog
	{
		#region Public methods

		/// <summary>
		/// Locate a generic method definition by name and generic parameter type definitions.
		/// </summary>
		/// <param name="declaringType">The type declaring the method.</param>
		/// <param name="methodName">The method name.</param>
		/// <param name="parameterTypeDefinitions">The parameter type definitions to match.</param>
		/// <returns>The matching generic method definition.</returns>
		public static MethodInfo GetGenericMethodDefinition(
			Type declaringType,
			string methodName,
			params Type[] parameterTypeDefinitions)
		{
			if (declaringType == null) throw new ArgumentNullException(nameof(declaringType));
			if (methodName == null) throw new ArgumentNullException(nameof(methodName));
			if (parameterTypeDefinitions == null) throw new ArgumentNullException(nameof(parameterTypeDefinitions));

			return GetMethod(
				declaringType,
				methodName,
				methodInfo => methodInfo.IsGenericMethodDefinition,
				parameterTypeDefinitions);
		}

		/// <summary>
		/// Locate a non-generic method by name and parameter types.
		/// </summary>
		/// <param name="declaringType">The type declaring the method.</param>
		/// <param name="methodName">The method name.</param>
		/// <param name="parameterTypes">The parameter types to match.</param>
		/// <returns>The matching method.</returns>
		public static MethodInfo GetMethod(Type declaringType, string methodName, params Type[] parameterTypes)
		{
			if (declaringType == null) throw new ArgumentNullException(nameof(declaringType));
			if (methodName == null) throw new ArgumentNullException(nameof(methodName));
			if (parameterTypes == null) throw new ArgumentNullException(nameof(parameterTypes));

			return GetMethod(declaringType, methodName, methodInfo => !methodInfo.IsGenericMethod, parameterTypes);
		}

		#endregion

		#region Private methods

		private static MethodInfo GetMethod(
			Type declaringType,
			string methodName,
			Func<MethodInfo, bool> methodPredicate,
			Type[] parameterTypes)
		{
			foreach (var methodInfo in declaringType.GetMethods(BindingFlags.Public | BindingFlags.Static))
			{
				if (methodInfo.Name != methodName || !methodPredicate(methodInfo)) continue;

				var parameters = methodInfo.GetParameters();

				if (parameters.Length != parameterTypes.Length) continue;

				if (parameters.Select(p => NormalizeParameterType(p.ParameterType)).SequenceEqual(parameterTypes))
				{
					return methodInfo;
				}
			}

			throw new InvalidOperationException(
				$"Method '{methodName}' with the requested signature was not found in type '{declaringType.FullName}'.");
		}

		private static Type NormalizeParameterType(Type parameterType)
		{
			if (parameterType.IsGenericType)
			{
				return parameterType.GetGenericTypeDefinition();
			}

			return parameterType;
		}

		#endregion
	}
}
