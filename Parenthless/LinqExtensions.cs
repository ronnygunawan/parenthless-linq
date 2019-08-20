using System;
using System.Collections.Generic;
using System.Linq;

namespace Parenthless {
	public static class LinqExtensions {

		// where skip(x)
		public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, SkipClause> skipClauseSelector) {
			if (source.Any()) {
				SkipClause skipClause = skipClauseSelector.Invoke(source.First());
				return source.Skip(skipClause.Count);
			} else {
				return source;
			}
		}

		// where take(x)
		public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, TakeClause> takeClauseSelector) {
			if (source.Any()) {
				TakeClause takeClause = takeClauseSelector.Invoke(source.First());
				return source.Take(takeClause.Count);
			} else {
				return source;
			}
		}

		// where skip(x).take(y)
		public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, SkipThenTakeClause> skipThenTakeClauseSelector) {
			if (source.Any()) {
				SkipThenTakeClause skipThenTakeClause = skipThenTakeClauseSelector.Invoke(source.First());
				return source.Skip(skipThenTakeClause.SkipCount).Take(skipThenTakeClause.TakeCount);
			} else {
				return source;
			}
		}

		// orderby distinct
		public static IEnumerable<TSource> OrderBy<TSource>(this IEnumerable<TSource> source, Func<TSource, DistinctClause> distinctClauseSelector) {
			return source.Distinct();
		}

		// orderby distinct descending
		public static IEnumerable<TSource> OrderByDescending<TSource>(this IEnumerable<TSource> source, Func<TSource, DistinctClause> distinctClauseSelector) {
			return source.Distinct();
		}

		// group x by list into g
		public static ToListEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, ToListClause> toListClauseSelector) {
			return new ToListEnumerable<TSource>(source);
		}

		// group x by list into g
		// select g
		public static List<TResult> Select<TSource, TResult>(this ToListEnumerable<TSource> source, Func<TSource, TResult> resultSelector) {
			return source.Enumerable.Select(resultSelector).ToList();
		}

		// group x by hashset into g
		public static ToHashSetEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, ToHashSetClause> toHashSetClauseSelector) {
			return new ToHashSetEnumerable<TSource>(source);
		}

		// group x by hashset into g
		// select g
		public static HashSet<TResult> Select<TSource, TResult>(this ToHashSetEnumerable<TSource> source, Func<TSource, TResult> resultSelector) {
			return new HashSet<TResult>(source.Enumerable.Select(resultSelector));
		}

	}
}
