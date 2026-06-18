using System;

namespace Grammophone.DataAccess.QueryExtensions
{
	/// <summary>
	/// Portable query functions intended to be translated by data access implementations.
	/// </summary>
	/// <remarks>
	/// <para>
	/// These methods are meant to appear inside query expression trees. They have no in-memory implementation.
	/// A query provider implementation must translate them to equivalent provider-native functions.
	/// </para>
	/// </remarks>
	public static class QueryFunctions
	{
		#region Public methods

		/// <summary>
		/// Matches a character expression against a pattern.
		/// </summary>
		/// <param name="matchExpression">The string expression to test.</param>
		/// <param name="pattern">The pattern to match against.</param>
		/// <returns>
		/// <see langword="true"/> if <paramref name="matchExpression"/> matches <paramref name="pattern"/>; otherwise, <see langword="false"/>.
		/// </returns>
		/// <remarks>
		/// Pattern syntax, escaping, case sensitivity and collation follow the underlying database provider.
		/// </remarks>
		public static bool Like(string matchExpression, string pattern)
		{
			throw CreateNotSupportedException(nameof(Like));
		}

		/// <summary>
		/// Matches a character expression against a pattern using an escape character.
		/// </summary>
		/// <param name="matchExpression">The string expression to test.</param>
		/// <param name="pattern">The pattern to match against.</param>
		/// <param name="escapeCharacter">The escape character used in the pattern.</param>
		/// <returns>
		/// <see langword="true"/> if <paramref name="matchExpression"/> matches <paramref name="pattern"/>; otherwise, <see langword="false"/>.
		/// </returns>
		/// <remarks>
		/// Pattern syntax, escaping, case sensitivity and collation follow the underlying database provider.
		/// </remarks>
		public static bool Like(string matchExpression, string pattern, string escapeCharacter)
		{
			throw CreateNotSupportedException(nameof(Like));
		}

		/// <summary>
		/// Returns the given date with the time portion cleared.
		/// </summary>
		/// <param name="dateValue">The date and time expression to truncate.</param>
		/// <returns>The date with the time portion cleared.</returns>
		public static DateTime? TruncateTime(DateTime? dateValue)
		{
			throw CreateNotSupportedException(nameof(TruncateTime));
		}

		/// <summary>
		/// Returns the given date with the time portion cleared.
		/// </summary>
		/// <param name="dateValue">The date and time expression to truncate.</param>
		/// <returns>The date with the time portion cleared.</returns>
		public static DateTimeOffset? TruncateTime(DateTimeOffset? dateValue)
		{
			throw CreateNotSupportedException(nameof(TruncateTime));
		}

		/// <summary>
		/// Returns the number of year boundaries crossed between two dates.
		/// </summary>
		/// <param name="startDate">The starting date.</param>
		/// <param name="endDate">The ending date.</param>
		/// <returns>The number of year boundaries crossed between the two dates.</returns>
		public static int? DiffYears(DateTime? startDate, DateTime? endDate)
		{
			throw CreateNotSupportedException(nameof(DiffYears));
		}

		/// <summary>
		/// Returns the number of year boundaries crossed between two dates.
		/// </summary>
		/// <param name="startDate">The starting date.</param>
		/// <param name="endDate">The ending date.</param>
		/// <returns>The number of year boundaries crossed between the two dates.</returns>
		public static int? DiffYears(DateTimeOffset? startDate, DateTimeOffset? endDate)
		{
			throw CreateNotSupportedException(nameof(DiffYears));
		}

		/// <summary>
		/// Returns the number of month boundaries crossed between two dates.
		/// </summary>
		/// <param name="startDate">The starting date.</param>
		/// <param name="endDate">The ending date.</param>
		/// <returns>The number of month boundaries crossed between the two dates.</returns>
		public static int? DiffMonths(DateTime? startDate, DateTime? endDate)
		{
			throw CreateNotSupportedException(nameof(DiffMonths));
		}

		/// <summary>
		/// Returns the number of month boundaries crossed between two dates.
		/// </summary>
		/// <param name="startDate">The starting date.</param>
		/// <param name="endDate">The ending date.</param>
		/// <returns>The number of month boundaries crossed between the two dates.</returns>
		public static int? DiffMonths(DateTimeOffset? startDate, DateTimeOffset? endDate)
		{
			throw CreateNotSupportedException(nameof(DiffMonths));
		}

		/// <summary>
		/// Returns the number of day boundaries crossed between two dates.
		/// </summary>
		/// <param name="startDate">The starting date.</param>
		/// <param name="endDate">The ending date.</param>
		/// <returns>The number of day boundaries crossed between the two dates.</returns>
		public static int? DiffDays(DateTime? startDate, DateTime? endDate)
		{
			throw CreateNotSupportedException(nameof(DiffDays));
		}

		/// <summary>
		/// Returns the number of day boundaries crossed between two dates.
		/// </summary>
		/// <param name="startDate">The starting date.</param>
		/// <param name="endDate">The ending date.</param>
		/// <returns>The number of day boundaries crossed between the two dates.</returns>
		public static int? DiffDays(DateTimeOffset? startDate, DateTimeOffset? endDate)
		{
			throw CreateNotSupportedException(nameof(DiffDays));
		}

		/// <summary>
		/// Returns the number of hour boundaries crossed between two dates.
		/// </summary>
		/// <param name="startDate">The starting date.</param>
		/// <param name="endDate">The ending date.</param>
		/// <returns>The number of hour boundaries crossed between the two dates.</returns>
		public static int? DiffHours(DateTime? startDate, DateTime? endDate)
		{
			throw CreateNotSupportedException(nameof(DiffHours));
		}

		/// <summary>
		/// Returns the number of hour boundaries crossed between two dates.
		/// </summary>
		/// <param name="startDate">The starting date.</param>
		/// <param name="endDate">The ending date.</param>
		/// <returns>The number of hour boundaries crossed between the two dates.</returns>
		public static int? DiffHours(DateTimeOffset? startDate, DateTimeOffset? endDate)
		{
			throw CreateNotSupportedException(nameof(DiffHours));
		}

		/// <summary>
		/// Returns the number of minute boundaries crossed between two dates.
		/// </summary>
		/// <param name="startDate">The starting date.</param>
		/// <param name="endDate">The ending date.</param>
		/// <returns>The number of minute boundaries crossed between the two dates.</returns>
		public static int? DiffMinutes(DateTime? startDate, DateTime? endDate)
		{
			throw CreateNotSupportedException(nameof(DiffMinutes));
		}

		/// <summary>
		/// Returns the number of minute boundaries crossed between two dates.
		/// </summary>
		/// <param name="startDate">The starting date.</param>
		/// <param name="endDate">The ending date.</param>
		/// <returns>The number of minute boundaries crossed between the two dates.</returns>
		public static int? DiffMinutes(DateTimeOffset? startDate, DateTimeOffset? endDate)
		{
			throw CreateNotSupportedException(nameof(DiffMinutes));
		}

		/// <summary>
		/// Returns the number of second boundaries crossed between two dates.
		/// </summary>
		/// <param name="startDate">The starting date.</param>
		/// <param name="endDate">The ending date.</param>
		/// <returns>The number of second boundaries crossed between the two dates.</returns>
		public static int? DiffSeconds(DateTime? startDate, DateTime? endDate)
		{
			throw CreateNotSupportedException(nameof(DiffSeconds));
		}

		/// <summary>
		/// Returns the number of second boundaries crossed between two dates.
		/// </summary>
		/// <param name="startDate">The starting date.</param>
		/// <param name="endDate">The ending date.</param>
		/// <returns>The number of second boundaries crossed between the two dates.</returns>
		public static int? DiffSeconds(DateTimeOffset? startDate, DateTimeOffset? endDate)
		{
			throw CreateNotSupportedException(nameof(DiffSeconds));
		}

		/// <summary>
		/// Returns the number of millisecond boundaries crossed between two dates.
		/// </summary>
		/// <param name="startDate">The starting date.</param>
		/// <param name="endDate">The ending date.</param>
		/// <returns>The number of millisecond boundaries crossed between the two dates.</returns>
		public static int? DiffMilliseconds(DateTime? startDate, DateTime? endDate)
		{
			throw CreateNotSupportedException(nameof(DiffMilliseconds));
		}

		/// <summary>
		/// Returns the number of millisecond boundaries crossed between two dates.
		/// </summary>
		/// <param name="startDate">The starting date.</param>
		/// <param name="endDate">The ending date.</param>
		/// <returns>The number of millisecond boundaries crossed between the two dates.</returns>
		public static int? DiffMilliseconds(DateTimeOffset? startDate, DateTimeOffset? endDate)
		{
			throw CreateNotSupportedException(nameof(DiffMilliseconds));
		}

		/// <summary>
		/// Adds the specified number of days to a date.
		/// </summary>
		/// <param name="dateValue">The date expression.</param>
		/// <param name="addValue">The number of days to add.</param>
		/// <returns>The date with the specified number of days added.</returns>
		public static DateTime? AddDays(DateTime? dateValue, int? addValue)
		{
			throw CreateNotSupportedException(nameof(AddDays));
		}

		/// <summary>
		/// Adds the specified number of months to a date.
		/// </summary>
		/// <param name="dateValue">The date expression.</param>
		/// <param name="addValue">The number of months to add.</param>
		/// <returns>The date with the specified number of months added.</returns>
		public static DateTime? AddMonths(DateTime? dateValue, int? addValue)
		{
			throw CreateNotSupportedException(nameof(AddMonths));
		}

		/// <summary>
		/// Adds the specified number of years to a date.
		/// </summary>
		/// <param name="dateValue">The date expression.</param>
		/// <param name="addValue">The number of years to add.</param>
		/// <returns>The date with the specified number of years added.</returns>
		public static DateTime? AddYears(DateTime? dateValue, int? addValue)
		{
			throw CreateNotSupportedException(nameof(AddYears));
		}

		#endregion

		#region Private methods

		private static NotSupportedException CreateNotSupportedException(string functionName)
		{
			return new NotSupportedException(
				$"Function '{functionName}' can only be used inside translated queries.");
		}

		#endregion
	}
}
