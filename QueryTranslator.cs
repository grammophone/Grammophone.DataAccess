using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Grammophone.DataAccess
{
	/// <summary>
	/// Specification of translation of terminal and non-terminal query methods.
	/// </summary>
	public class QueryTranslator
	{
		#region Construction

		/// <summary>
		/// Create.
		/// </summary>
		/// <param name="terminalMethodsAdapter">Adaptation of terminal methods which execute or materialize a query.</param>
		/// <param name="shapingMethodsAdapter">Adaptation of non-terminal query shaping methods.</param>
		/// <param name="methodMappingsByMethodInfo">
		/// Dictionary of mappings of portable method expressions into native provider method expressions, keyed by <see cref="MethodInfo"/>.
		/// </param>
		public QueryTranslator(
			TerminalMethodsAdapter terminalMethodsAdapter,
			ShapingMethodsAdapter shapingMethodsAdapter,
			IReadOnlyDictionary<MethodInfo, MethodMapping> methodMappingsByMethodInfo)
		{
			if (terminalMethodsAdapter == null) throw new ArgumentNullException(nameof(terminalMethodsAdapter));
			if (shapingMethodsAdapter == null) throw new ArgumentNullException(nameof(shapingMethodsAdapter));
			if (methodMappingsByMethodInfo == null) throw new ArgumentNullException(nameof(methodMappingsByMethodInfo));

			this.TerminalMethodsAdapter = terminalMethodsAdapter;
			this.ShapingMethodsAdapter = shapingMethodsAdapter;
			this.MethodMappingsByMethodInfo = methodMappingsByMethodInfo;
		}

		#endregion

		#region Public properties

		/// <summary>
		/// Adaptation of terminal methods which execute or materialize a query.
		/// </summary>
		public TerminalMethodsAdapter TerminalMethodsAdapter { get; }

		/// <summary>
		/// Adaptation of non-terminal query shaping methods.
		/// </summary>
		public ShapingMethodsAdapter ShapingMethodsAdapter { get; }

		/// <summary>
		/// Dictionary of mappings of portable method expressions into native provider method expressions, keyed by <see cref="MethodInfo"/>.
		/// </summary>
		public IReadOnlyDictionary<MethodInfo, MethodMapping> MethodMappingsByMethodInfo { get; }

		#endregion
	}
}
