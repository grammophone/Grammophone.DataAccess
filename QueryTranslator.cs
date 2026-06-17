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
		/// <summary>
		/// Adaptation of terminal methods which execute or materialize a query.
		/// </summary>
		public TerminalMethodsAdapter TerminalMethodsAdapter { get; }

		/// <summary>
		/// Dictionary of mappings of portable method expressions into native provider method expressions, keyed by <see cref="MethodInfo"/>.
		/// </summary>
		public IReadOnlyDictionary<MethodInfo, MethodMapping> MethodMappingsByMethodInfo { get; }
	}
}
