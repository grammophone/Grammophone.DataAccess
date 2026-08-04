using System;
using System.Reflection;

namespace Grammophone.DataAccess.QueryExtensions
{
	/// <summary>
	/// Method information for portable query functions.
	/// </summary>
	public static class QueryFunctionsMethodInfos
	{
		#region Public fields

		/// <summary>Method information for <see cref="QueryFunctions.Like(string, string)"/>.</summary>
		public static readonly MethodInfo Like = MethodInfoCatalog.GetMethodInfo(
			typeof(QueryFunctions),
			nameof(QueryFunctions.Like),
			typeof(string),
			typeof(string));

		/// <summary>Method information for <see cref="QueryFunctions.Like(string, string, string)"/>.</summary>
		public static readonly MethodInfo LikeWithEscape = MethodInfoCatalog.GetMethodInfo(
			typeof(QueryFunctions),
			nameof(QueryFunctions.Like),
			typeof(string),
			typeof(string),
			typeof(string));

		/// <summary>Method information for <see cref="QueryFunctions.TruncateTime(DateTime?)"/>.</summary>
		public static readonly MethodInfo TruncateDateTime = MethodInfoCatalog.GetMethodInfo(
			typeof(QueryFunctions),
			nameof(QueryFunctions.TruncateTime),
			typeof(DateTime?));

		/// <summary>Method information for <see cref="QueryFunctions.TruncateTime(DateTimeOffset?)"/>.</summary>
		public static readonly MethodInfo TruncateDateTimeOffset = MethodInfoCatalog.GetMethodInfo(
			typeof(QueryFunctions),
			nameof(QueryFunctions.TruncateTime),
			typeof(DateTimeOffset?));

		/// <summary>Method information for <see cref="QueryFunctions.DiffYears(DateTime?, DateTime?)"/>.</summary>
		public static readonly MethodInfo DiffYearsDateTime = GetDateTimeDiff(nameof(QueryFunctions.DiffYears));

		/// <summary>Method information for <see cref="QueryFunctions.DiffYears(DateTimeOffset?, DateTimeOffset?)"/>.</summary>
		public static readonly MethodInfo DiffYearsDateTimeOffset = GetDateTimeOffsetDiff(nameof(QueryFunctions.DiffYears));

		/// <summary>Method information for <see cref="QueryFunctions.DiffMonths(DateTime?, DateTime?)"/>.</summary>
		public static readonly MethodInfo DiffMonthsDateTime = GetDateTimeDiff(nameof(QueryFunctions.DiffMonths));

		/// <summary>Method information for <see cref="QueryFunctions.DiffMonths(DateTimeOffset?, DateTimeOffset?)"/>.</summary>
		public static readonly MethodInfo DiffMonthsDateTimeOffset = GetDateTimeOffsetDiff(nameof(QueryFunctions.DiffMonths));

		/// <summary>Method information for <see cref="QueryFunctions.DiffDays(DateTime?, DateTime?)"/>.</summary>
		public static readonly MethodInfo DiffDaysDateTime = GetDateTimeDiff(nameof(QueryFunctions.DiffDays));

		/// <summary>Method information for <see cref="QueryFunctions.DiffDays(DateTimeOffset?, DateTimeOffset?)"/>.</summary>
		public static readonly MethodInfo DiffDaysDateTimeOffset = GetDateTimeOffsetDiff(nameof(QueryFunctions.DiffDays));

		/// <summary>Method information for <see cref="QueryFunctions.DiffHours(DateTime?, DateTime?)"/>.</summary>
		public static readonly MethodInfo DiffHoursDateTime = GetDateTimeDiff(nameof(QueryFunctions.DiffHours));

		/// <summary>Method information for <see cref="QueryFunctions.DiffHours(DateTimeOffset?, DateTimeOffset?)"/>.</summary>
		public static readonly MethodInfo DiffHoursDateTimeOffset = GetDateTimeOffsetDiff(nameof(QueryFunctions.DiffHours));

		/// <summary>Method information for <see cref="QueryFunctions.DiffMinutes(DateTime?, DateTime?)"/>.</summary>
		public static readonly MethodInfo DiffMinutesDateTime = GetDateTimeDiff(nameof(QueryFunctions.DiffMinutes));

		/// <summary>Method information for <see cref="QueryFunctions.DiffMinutes(DateTimeOffset?, DateTimeOffset?)"/>.</summary>
		public static readonly MethodInfo DiffMinutesDateTimeOffset = GetDateTimeOffsetDiff(nameof(QueryFunctions.DiffMinutes));

		/// <summary>Method information for <see cref="QueryFunctions.DiffSeconds(DateTime?, DateTime?)"/>.</summary>
		public static readonly MethodInfo DiffSecondsDateTime = GetDateTimeDiff(nameof(QueryFunctions.DiffSeconds));

		/// <summary>Method information for <see cref="QueryFunctions.DiffSeconds(DateTimeOffset?, DateTimeOffset?)"/>.</summary>
		public static readonly MethodInfo DiffSecondsDateTimeOffset = GetDateTimeOffsetDiff(nameof(QueryFunctions.DiffSeconds));

		/// <summary>Method information for <see cref="QueryFunctions.DiffMilliseconds(DateTime?, DateTime?)"/>.</summary>
		public static readonly MethodInfo DiffMillisecondsDateTime = GetDateTimeDiff(nameof(QueryFunctions.DiffMilliseconds));

		/// <summary>Method information for <see cref="QueryFunctions.DiffMilliseconds(DateTimeOffset?, DateTimeOffset?)"/>.</summary>
		public static readonly MethodInfo DiffMillisecondsDateTimeOffset = GetDateTimeOffsetDiff(nameof(QueryFunctions.DiffMilliseconds));

		/// <summary>Method information for <see cref="QueryFunctions.AddDays(DateTime?, int?)"/>.</summary>
		public static readonly MethodInfo AddDays = GetDateTimeAdd(nameof(QueryFunctions.AddDays));

		/// <summary>Method information for <see cref="QueryFunctions.AddMonths(DateTime?, int?)"/>.</summary>
		public static readonly MethodInfo AddMonths = GetDateTimeAdd(nameof(QueryFunctions.AddMonths));

		/// <summary>Method information for <see cref="QueryFunctions.AddYears(DateTime?, int?)"/>.</summary>
		public static readonly MethodInfo AddYears = GetDateTimeAdd(nameof(QueryFunctions.AddYears));

		/// <summary>Method information for <see cref="QueryFunctions.CreateDateTime(int?, int?, int?, int?, int?, double?)"/>.</summary>
		public static readonly MethodInfo CreateDateTime = MethodInfoCatalog.GetMethodInfo(
			typeof(QueryFunctions),
			nameof(QueryFunctions.CreateDateTime),
			typeof(int?),
			typeof(int?),
			typeof(int?),
			typeof(int?),
			typeof(int?),
			typeof(double?));

		#endregion

		#region Private methods

		private static MethodInfo GetDateTimeDiff(string methodName)
		{
			return MethodInfoCatalog.GetMethodInfo(
				typeof(QueryFunctions),
				methodName,
				typeof(DateTime?),
				typeof(DateTime?));
		}

		private static MethodInfo GetDateTimeOffsetDiff(string methodName)
		{
			return MethodInfoCatalog.GetMethodInfo(
				typeof(QueryFunctions),
				methodName,
				typeof(DateTimeOffset?),
				typeof(DateTimeOffset?));
		}

		private static MethodInfo GetDateTimeAdd(string methodName)
		{
			return MethodInfoCatalog.GetMethodInfo(
				typeof(QueryFunctions),
				methodName,
				typeof(DateTime?),
				typeof(int?));
		}

		#endregion
	}
}
