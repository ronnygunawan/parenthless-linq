using Parenthless.Clauses;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "distinctClauseSelector is required in method signature.")]
		public static IEnumerable<TSource> OrderBy<TSource>(this IEnumerable<TSource> source, Func<TSource, DistinctClause> distinctClauseSelector) {
			return source.Distinct();
		}

		// orderby distinct descending
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "distinctClauseSelector is required in method signature.")]
		public static IEnumerable<TSource> OrderByDescending<TSource>(this IEnumerable<TSource> source, Func<TSource, DistinctClause> distinctClauseSelector) {
			return source.Distinct();
		}

		// orderby reverse
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "reverseClauseSelector is required in method signature.")]
		public static IEnumerable<TSource> OrderBy<TSource>(this IEnumerable<TSource> source, Func<TSource, ReverseClause> reverseClauseSelector) {
			return source.Reverse();
		}

		// orderby reverse descending
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "reverseClauseSelector is required in method signature.")]
		public static IEnumerable<TSource> OrderByDescending<TSource>(this IEnumerable<TSource> source, Func<TSource, ReverseClause> reverseClauseSelector) {
			return source.Reverse();
		}

		// group x by ToList into g
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "toListClauseSelector is required in method signature.")]
		public static ToListEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, ToListClause> toListClauseSelector) {
			return new ToListEnumerable<TSource>(source);
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "toListClauseSelector is required in method signature.")]
		public static ToListEnumerable<TElement> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, ToListClause> toListClauseSelector, Func<TSource, TElement> elementSelector) {
			return new ToListEnumerable<TElement>(source.Select(elementSelector));
		}

		// group x by ToList into g
		// select g
		public static List<TResult> Select<TSource, TResult>(this ToListEnumerable<TSource> source, Func<TSource, TResult> resultSelector) {
			return source.Enumerable.Select(resultSelector).ToList();
		}

		// group x by ToHashSet into g
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "toHashSetClauseSelector is required in method signature.")]
		public static ToHashSetEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, ToHashSetClause> toHashSetClauseSelector) {
			return new ToHashSetEnumerable<TSource>(source);
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "toHashSetClauseSelector is required in method signature.")]
		public static ToHashSetEnumerable<TElement> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, ToHashSetClause> toHashSetClauseSelector, Func<TSource, TElement> elementSelector) {
			return new ToHashSetEnumerable<TElement>(source.Select(elementSelector));
		}

		// group x by ToHashSet into g
		// select g
		public static HashSet<TResult> Select<TSource, TResult>(this ToHashSetEnumerable<TSource> source, Func<TSource, TResult> resultSelector) {
			return new HashSet<TResult>(source.Enumerable.Select(resultSelector));
		}

		// group x by First into g
		// select g
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "firstClauseSelector is required in method signature.")]
		public static FirstEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, FirstClause> firstClauseSelector) {
			return new FirstEnumerable<TSource>(source);
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "firstClauseSelector is required in method signature.")]
		public static FirstEnumerable<TElement> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, FirstClause> firstClauseSelector, Func<TSource, TElement> elementSelector) {
			return new FirstEnumerable<TElement>(source.Select(elementSelector));
		}

		// group x by First into g
		// select g
		public static TResult Select<TSource, TResult>(this FirstEnumerable<TSource> source, Func<TSource, TResult> resultSelector) {
			return source.Enumerable.Select(resultSelector).First();
		}

		// group x by FirstOrDefault into g
		// select g
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "firstOrDefaultClauseSelector is required in method signature.")]
		public static FirstOrDefaultEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, FirstOrDefaultClause> firstOrDefaultClauseSelector) {
			return new FirstOrDefaultEnumerable<TSource>(source);
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "firstOrDefaultClauseSelector is required in method signature.")]
		public static FirstOrDefaultEnumerable<TElement> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, FirstOrDefaultClause> firstOrDefaultClauseSelector, Func<TSource, TElement> elementSelector) {
			return new FirstOrDefaultEnumerable<TElement>(source.Select(elementSelector));
		}

		// group x by FirstOrDefault into g
		// select g
		public static TResult Select<TSource, TResult>(this FirstOrDefaultEnumerable<TSource> source, Func<TSource, TResult> resultSelector) {
			return source.Enumerable.Select(resultSelector).FirstOrDefault();
		}

		// group x by Last into g
		// select g
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "lastClauseSelector is required in method signature.")]
		public static LastEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, LastClause> lastClauseSelector) {
			return new LastEnumerable<TSource>(source);
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "lastClauseSelector is required in method signature.")]
		public static LastEnumerable<TElement> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, LastClause> lastClauseSelector, Func<TSource, TElement> elementSelector) {
			return new LastEnumerable<TElement>(source.Select(elementSelector));
		}

		// group x by Last into g
		// select g
		public static TResult Select<TSource, TResult>(this LastEnumerable<TSource> source, Func<TSource, TResult> resultSelector) {
			return source.Enumerable.Select(resultSelector).Last();
		}

		// group x by LastOrDefault into g
		// select g
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "lastOrDefaultClauseSelector is required in method signature.")]
		public static LastOrDefaultEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, LastOrDefaultClause> lastOrDefaultClauseSelector) {
			return new LastOrDefaultEnumerable<TSource>(source);
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "lastOrDefaultClauseSelector is required in method signature.")]
		public static LastOrDefaultEnumerable<TElement> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, LastOrDefaultClause> lastOrDefaultClauseSelector, Func<TSource, TElement> elementSelector) {
			return new LastOrDefaultEnumerable<TElement>(source.Select(elementSelector));
		}

		// group x by LastOrDefault into g
		// select g
		public static TResult Select<TSource, TResult>(this LastOrDefaultEnumerable<TSource> source, Func<TSource, TResult> resultSelector) {
			return source.Enumerable.Select(resultSelector).LastOrDefault();
		}

		// group x by Single into g
		// select g
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "singleClauseSelector is required in method signature.")]
		public static SingleEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, SingleClause> singleClauseSelector) {
			return new SingleEnumerable<TSource>(source);
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "singleClauseSelector is required in method signature.")]
		public static SingleEnumerable<TElement> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, SingleClause> singleClauseSelector, Func<TSource, TElement> elementSelector) {
			return new SingleEnumerable<TElement>(source.Select(elementSelector));
		}

		// group x by Single into g
		// select g
		public static TResult Select<TSource, TResult>(this SingleEnumerable<TSource> source, Func<TSource, TResult> resultSelector) {
			return source.Enumerable.Select(resultSelector).Single();
		}

		// group x by SingleOrDefault into g
		// select g
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "singleOrDefaultClauseSelector is required in method signature.")]
		public static SingleOrDefaultEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, SingleOrDefaultClause> singleOrDefaultClauseSelector) {
			return new SingleOrDefaultEnumerable<TSource>(source);
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "singleOrDefaultClauseSelector is required in method signature.")]
		public static SingleOrDefaultEnumerable<TElement> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, SingleOrDefaultClause> singleOrDefaultClauseSelector, Func<TSource, TElement> elementSelector) {
			return new SingleOrDefaultEnumerable<TElement>(source.Select(elementSelector));
		}

		// group x by SingleOrDefault into g
		// select g
		public static TResult Select<TSource, TResult>(this SingleOrDefaultEnumerable<TSource> source, Func<TSource, TResult> resultSelector) {
			return source.Enumerable.Select(resultSelector).SingleOrDefault();
		}

		// group x by Any into g
		// select g
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "anyClauseSelector is required in method signature.")]
		public static AnyEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, AnyClause> anyClauseSelector) {
			return new AnyEnumerable<TSource>(source);
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "anyClauseSelector is required in method signature.")]
		public static AnyEnumerable<TElement> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, AnyClause> anyClauseSelector, Func<TSource, TElement> elementSelector) {
			return new AnyEnumerable<TElement>(source.Select(elementSelector));
		}

		// group x by Any into g
		// select g
		public static bool Select<TSource, TResult>(this AnyEnumerable<TSource> source, Func<TSource, TResult> resultSelector) {
			return source.Enumerable.Select(resultSelector).Any();
		}
	}
}
