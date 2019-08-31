using Parenthless.Clauses;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Parenthless {
	public static class LinqExtensions {

		// where OfType<TResult>()
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "ofTypeClauseSelector is required in method signature.")]
		public static IEnumerable<TResult> Where<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, OfTypeClause<TResult>> ofTypeClauseSelector) {
			return source.OfType<TResult>();
		}

		// where Skip(x)
		public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, SkipClause> skipClauseSelector) {
			SkipClause skipClause = skipClauseSelector.Invoke(source.FirstOrDefault());
			return source.Skip(skipClause.Count);
		}

		// where Take(x)
		public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, TakeClause> takeClauseSelector) {
			TakeClause takeClause = takeClauseSelector.Invoke(source.FirstOrDefault());
			return source.Take(takeClause.Count);
		}

		// where Skip(x).Take(y)
		public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, SkipThenTakeClause> skipThenTakeClauseSelector) {
			SkipThenTakeClause skipThenTakeClause = skipThenTakeClauseSelector.Invoke(source.FirstOrDefault());
			return source.Skip(skipThenTakeClause.SkipCount).Take(skipThenTakeClause.TakeCount);
		}

		// where SkipLast(x)
		public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, SkipLastClause> skipLastClauseSelector) {
			SkipLastClause skipLastClause = skipLastClauseSelector.Invoke(source.FirstOrDefault());
#if NETSTANDARD2_0 || NETFRAMEWORK
			return Implementations.SkipLast(source, skipLastClause.Count);
#else
			return source.SkipLast(skipLastClause.Count);
#endif
		}

		// where TakeLast(x)
		public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, TakeLastClause> takeLastClauseSelector) {
			TakeLastClause takeLastClause = takeLastClauseSelector.Invoke(source.FirstOrDefault());
#if NETSTANDARD2_0 || NETFRAMEWORK
			return Implementations.TakeLast(source, takeLastClause.Count);
#else
			return source.TakeLast(takeLastClause.Count);
#endif
		}

		// where SkipLast(x).TakeLast(y)
		public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, SkipLastThenTakeLastClause> skipLastThenTakeLastClauseSelector) {
			SkipLastThenTakeLastClause skipLastThenTakeLastClause = skipLastThenTakeLastClauseSelector.Invoke(source.FirstOrDefault());
			return Implementations.SkipLastThenTakeLast(source, skipLastThenTakeLastClause.SkipLastCount, skipLastThenTakeLastClause.TakeLastCount);
		}

		// where SkipWhile(condition)
		public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, SkipWhileClause> skipWhileClauseSelector) {
			return source.SkipWhile(item => skipWhileClauseSelector.Invoke(item).Condition);
		}

		// where TakeWhile(condition)
		public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, TakeWhileClause> takeWhileClauseSelector) {
			return source.TakeWhile(item => takeWhileClauseSelector.Invoke(item).Condition);
		}

		// where DefaultIfEmpty
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "defaultIfEmptyClauseSelector is required in method signature.")]
		public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, DefaultIfEmptyClause> defaultIfEmptyClauseSelector) {
			return source.DefaultIfEmpty();
		}

		// where Concat(second)
		public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, ConcatClause<TSource>> concatClauseSelector) {
			ConcatClause<TSource> concatClause = concatClauseSelector.Invoke(source.FirstOrDefault());
			return source.Concat(concatClause.Second);
		}

		// where Union(second)
		public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, UnionClause<TSource>> unionClauseSelector) {
			UnionClause<TSource> unionClause = unionClauseSelector.Invoke(source.FirstOrDefault());
			return source.Union(unionClause.Second, unionClause.Comparer);
		}

		// where Except(second)
		public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, ExceptClause<TSource>> exceptClauseSelector) {
			ExceptClause<TSource> exceptClause = exceptClauseSelector.Invoke(source.FirstOrDefault());
			return source.Except(exceptClause.Second, exceptClause.Comparer);
		}

		// where Intersect(second)
		public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, IntersectClause<TSource>> intersectClauseSelector) {
			IntersectClause<TSource> intersectClause = intersectClauseSelector.Invoke(source.FirstOrDefault());
			return source.Intersect(intersectClause.Second, intersectClause.Comparer);
		}

		// where Distinct
		public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, DistinctClause> distinctClauseSelector) {
			return source.Distinct();
		}

		// orderby Distinct
		#region Distinct
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "distinctClauseSelector is required in method signature.")]
		public static IEnumerable<TSource> OrderBy<TSource>(this IEnumerable<TSource> source, Func<TSource, DistinctClause> distinctClauseSelector) {
			return source.Distinct();
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "distinctClauseSelector is required in method signature.")]
		[Obsolete("'descending' has no effect.")]
		public static IEnumerable<TSource> OrderByDescending<TSource>(this IEnumerable<TSource> source, Func<TSource, DistinctClause> distinctClauseSelector) {
			return source.Distinct();
		}
		#endregion

		// orderby Distinct.Using(comparer)
		#region Distinct.Using(comparer)
		public static IEnumerable<TSource> OrderBy<TSource>(this IEnumerable<TSource> source, Func<TSource, DistinctClause<TSource>> distinctClauseSelector) {
			DistinctClause<TSource> distinctClause = distinctClauseSelector.Invoke(source.FirstOrDefault());
			return source.Distinct(distinctClause.Comparer);
		}
		[Obsolete("'descending' has no effect.")]
		public static IEnumerable<TSource> OrderByDescending<TSource>(this IEnumerable<TSource> source, Func<TSource, DistinctClause<TSource>> distinctClauseSelector) {
			DistinctClause<TSource> distinctClause = distinctClauseSelector.Invoke(source.FirstOrDefault());
			return source.Distinct(distinctClause.Comparer);
		}
		#endregion

		// orderby Reverse
		#region Reverse
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "reverseClauseSelector is required in method signature.")]
		public static IEnumerable<TSource> OrderBy<TSource>(this IEnumerable<TSource> source, Func<TSource, ReverseClause> reverseClauseSelector) {
			return source.Reverse();
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "reverseClauseSelector is required in method signature.")]
		[Obsolete("'descending' has no effect.")]
		public static IEnumerable<TSource> OrderByDescending<TSource>(this IEnumerable<TSource> source, Func<TSource, ReverseClause> reverseClauseSelector) {
			return source.Reverse();
		}
		#endregion

		// group x by ToList into g
		#region ToList
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "toListClauseSelector is required in method signature.")]
		public static ToListEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, ToListClause> toListClauseSelector) {
			return new ToListEnumerable<TSource>(source);
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "toListClauseSelector is required in method signature.")]
		public static ToListEnumerable<TElement> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, ToListClause> toListClauseSelector, Func<TSource, TElement> elementSelector) {
			return new ToListEnumerable<TElement>(source.Select(elementSelector));
		}
		public static List<TResult> Select<TSource, TResult>(this ToListEnumerable<TSource> source, Func<TSource, TResult> resultSelector) {
			return source.Enumerable.Select(resultSelector).ToList();
		}
		#endregion

		// group x by ToArray into g
		#region ToArray
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "toArrayClauseSelector is required in method signature.")]
		public static ToArrayEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, ToArrayClause> toArrayClauseSelector) {
			return new ToArrayEnumerable<TSource>(source);
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "toArrayClauseSelector is required in method signature.")]
		public static ToArrayEnumerable<TElement> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, ToArrayClause> toArrayClauseSelector, Func<TSource, TElement> elementSelector) {
			return new ToArrayEnumerable<TElement>(source.Select(elementSelector));
		}
		public static TResult[] Select<TSource, TResult>(this ToArrayEnumerable<TSource> source, Func<TSource, TResult> resultSelector) {
			return source.Enumerable.Select(resultSelector).ToArray();
		}
		#endregion

		// group x by ToHashSet into g
		#region ToHashSet
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "toHashSetClauseSelector is required in method signature.")]
		public static ToHashSetEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, ToHashSetClause> toHashSetClauseSelector) {
			return new ToHashSetEnumerable<TSource>(source);
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "toHashSetClauseSelector is required in method signature.")]
		public static ToHashSetEnumerable<TElement> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, ToHashSetClause> toHashSetClauseSelector, Func<TSource, TElement> elementSelector) {
			return new ToHashSetEnumerable<TElement>(source.Select(elementSelector));
		}
		public static HashSet<TResult> Select<TSource, TResult>(this ToHashSetEnumerable<TSource> source, Func<TSource, TResult> resultSelector) {
			return new HashSet<TResult>(source.Enumerable.Select(resultSelector));
		}
		#endregion

		// group x by First into g select g
		#region First
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "firstClauseSelector is required in method signature.")]
		public static FirstEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, FirstClause> firstClauseSelector) {
			return new FirstEnumerable<TSource>(source);
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "firstClauseSelector is required in method signature.")]
		public static FirstEnumerable<TElement> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, FirstClause> firstClauseSelector, Func<TSource, TElement> elementSelector) {
			return new FirstEnumerable<TElement>(source.Select(elementSelector));
		}
		public static TResult Select<TSource, TResult>(this FirstEnumerable<TSource> source, Func<TSource, TResult> resultSelector) {
			return source.Enumerable.Select(resultSelector).First();
		}
		#endregion

		// group x by FirstOrDefault into g select g
		#region FirstOrDefault
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "firstOrDefaultClauseSelector is required in method signature.")]
		public static FirstOrDefaultEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, FirstOrDefaultClause> firstOrDefaultClauseSelector) {
			return new FirstOrDefaultEnumerable<TSource>(source);
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "firstOrDefaultClauseSelector is required in method signature.")]
		public static FirstOrDefaultEnumerable<TElement> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, FirstOrDefaultClause> firstOrDefaultClauseSelector, Func<TSource, TElement> elementSelector) {
			return new FirstOrDefaultEnumerable<TElement>(source.Select(elementSelector));
		}
		public static TResult Select<TSource, TResult>(this FirstOrDefaultEnumerable<TSource> source, Func<TSource, TResult> resultSelector) {
			return source.Enumerable.Select(resultSelector).FirstOrDefault();
		}
		#endregion

		// group x by Last into g select g
		#region Last
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "lastClauseSelector is required in method signature.")]
		public static LastEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, LastClause> lastClauseSelector) {
			return new LastEnumerable<TSource>(source);
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "lastClauseSelector is required in method signature.")]
		public static LastEnumerable<TElement> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, LastClause> lastClauseSelector, Func<TSource, TElement> elementSelector) {
			return new LastEnumerable<TElement>(source.Select(elementSelector));
		}
		public static TResult Select<TSource, TResult>(this LastEnumerable<TSource> source, Func<TSource, TResult> resultSelector) {
			return source.Enumerable.Select(resultSelector).Last();
		}
		#endregion

		// group x by LastOrDefault into g select g
		#region LastOrDefault
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "lastOrDefaultClauseSelector is required in method signature.")]
		public static LastOrDefaultEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, LastOrDefaultClause> lastOrDefaultClauseSelector) {
			return new LastOrDefaultEnumerable<TSource>(source);
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "lastOrDefaultClauseSelector is required in method signature.")]
		public static LastOrDefaultEnumerable<TElement> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, LastOrDefaultClause> lastOrDefaultClauseSelector, Func<TSource, TElement> elementSelector) {
			return new LastOrDefaultEnumerable<TElement>(source.Select(elementSelector));
		}
		public static TResult Select<TSource, TResult>(this LastOrDefaultEnumerable<TSource> source, Func<TSource, TResult> resultSelector) {
			return source.Enumerable.Select(resultSelector).LastOrDefault();
		}
		#endregion

		// group x by Single into g select g
		#region Single
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "singleClauseSelector is required in method signature.")]
		public static SingleEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, SingleClause> singleClauseSelector) {
			return new SingleEnumerable<TSource>(source);
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "singleClauseSelector is required in method signature.")]
		public static SingleEnumerable<TElement> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, SingleClause> singleClauseSelector, Func<TSource, TElement> elementSelector) {
			return new SingleEnumerable<TElement>(source.Select(elementSelector));
		}
		public static TResult Select<TSource, TResult>(this SingleEnumerable<TSource> source, Func<TSource, TResult> resultSelector) {
			return source.Enumerable.Select(resultSelector).Single();
		}
		#endregion

		// group x by SingleOrDefault into g select g
		#region SingleOrDefault
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "singleOrDefaultClauseSelector is required in method signature.")]
		public static SingleOrDefaultEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, SingleOrDefaultClause> singleOrDefaultClauseSelector) {
			return new SingleOrDefaultEnumerable<TSource>(source);
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "singleOrDefaultClauseSelector is required in method signature.")]
		public static SingleOrDefaultEnumerable<TElement> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, SingleOrDefaultClause> singleOrDefaultClauseSelector, Func<TSource, TElement> elementSelector) {
			return new SingleOrDefaultEnumerable<TElement>(source.Select(elementSelector));
		}
		public static TResult Select<TSource, TResult>(this SingleOrDefaultEnumerable<TSource> source, Func<TSource, TResult> resultSelector) {
			return source.Enumerable.Select(resultSelector).SingleOrDefault();
		}
		#endregion

		// group x by ElementAt(index) into g select g
		#region ElementAt
		public static ElementAtEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, ElementAtClause> elementAtClauseSelector) {
			ElementAtClause elementAtClause = elementAtClauseSelector.Invoke(source.FirstOrDefault());
			return new ElementAtEnumerable<TSource>(source, elementAtClause.Index);
		}
		public static ElementAtEnumerable<TElement> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, ElementAtClause> elementAtClauseSelector, Func<TSource, TElement> elementSelector) {
			ElementAtClause elementAtClause = elementAtClauseSelector.Invoke(source.FirstOrDefault());
			return new ElementAtEnumerable<TElement>(source.Select(elementSelector), elementAtClause.Index);
		}
		public static TResult Select<TSource, TResult>(this ElementAtEnumerable<TSource> source, Func<TSource, TResult> resultSelector) {
			return source.Enumerable.Select(resultSelector).ElementAt(source.Index);
		}
		#endregion

		// group x by ElementAtOrDefault(index) into g select g
		#region ElementAtOrDefault
		public static ElementAtOrDefaultEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, ElementAtOrDefaultClause> elementAtOrDefaultClauseSelector) {
			ElementAtOrDefaultClause elementAtOrDefaultClause = elementAtOrDefaultClauseSelector.Invoke(source.FirstOrDefault());
			return new ElementAtOrDefaultEnumerable<TSource>(source, elementAtOrDefaultClause.Index);
		}
		public static ElementAtOrDefaultEnumerable<TElement> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, ElementAtOrDefaultClause> elementAtOrDefaultClauseSelector, Func<TSource, TElement> elementSelector) {
			ElementAtOrDefaultClause elementAtOrDefaultClause = elementAtOrDefaultClauseSelector.Invoke(source.FirstOrDefault());
			return new ElementAtOrDefaultEnumerable<TElement>(source.Select(elementSelector), elementAtOrDefaultClause.Index);
		}
		public static TResult Select<TSource, TResult>(this ElementAtOrDefaultEnumerable<TSource> source, Func<TSource, TResult> resultSelector) {
			return source.Enumerable.Select(resultSelector).ElementAtOrDefault(source.Index);
		}
		#endregion

		// group x by Any(condition) into g select g
		#region Any
		public static AnyEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, AnyClause> anyClauseSelector) {
			return new AnyEnumerable<TSource>(source, anyClauseSelector);
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "elementSelector is required in method signature.")]
		public static AnyEnumerable<TSource> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, AnyClause> anyClauseSelector, Func<TSource, TElement> elementSelector) {
			return new AnyEnumerable<TSource>(source, anyClauseSelector);
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "resultSelector is required in method signature.")]
		public static bool Select<TSource, TResult>(this AnyEnumerable<TSource> source, Func<TSource, TResult> resultSelector) {
			return source.Enumerable.Any(item => source.AnyClauseSelector.Invoke(item).Condition);
		}
		#endregion

		// group x by All(condition) into g select g
		#region All
		public static AllEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, AllClause> allClauseSelector) {
			return new AllEnumerable<TSource>(source, allClauseSelector);
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "elementSelector is required in method signature.")]
		public static AllEnumerable<TSource> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, AllClause> allClauseSelector, Func<TSource, TElement> elementSelector) {
			return new AllEnumerable<TSource>(source, allClauseSelector);
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "resultSelector is required in method signature.")]
		public static bool Select<TSource, TResult>(this AllEnumerable<TSource> source, Func<TSource, TResult> resultSelector) {
			return source.Enumerable.All(item => source.AllClauseSelector.Invoke(item).Condition);
		}
		#endregion

		// group x by Count() into g select g
		#region Count
		public static CountEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, CountClause> countClauseSelector) {
			return new CountEnumerable<TSource>(source, countClauseSelector);
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "elementSelector is required in method signature.")]
		public static CountEnumerable<TSource> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, CountClause> countClauseSelector, Func<TSource, TElement> elementSelector) {
			return new CountEnumerable<TSource>(source, countClauseSelector);
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "resultSelector is required in method signature.")]
		public static int Select<TSource, TResult>(this CountEnumerable<TSource> source, Func<TSource, TResult> resultSelector) {
			return source.Enumerable.Count(item => source.CountClauseSelector.Invoke(item).Condition);
		}
		#endregion

		// group x by Max into g select g
		#region Max
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "maxClauseSelector is required in method signature.")]
		public static MaxEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, MaxClause> maxClauseSelector) where TSource : IComparable<TSource> {
			return new MaxEnumerable<TSource>(source);
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "maxClauseSelector is required in method signature.")]
		public static MaxEnumerable<TElement> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, MaxClause> maxClauseSelector, Func<TSource, TElement> elementSelector) where TElement : IComparable<TElement> {
			return new MaxEnumerable<TElement>(source.Select(elementSelector));
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "resultSelector is required in method signature.")]
		public static TSource Select<TSource, TResult>(this MaxEnumerable<TSource> source, Func<TSource, TResult> resultSelector) where TSource : IComparable<TSource> {
			return source.Enumerable.Max();
		}
		#endregion

		// group x by Min into g select g
		#region Min
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "minClauseSelector is required in method signature.")]
		public static MinEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, MinClause> minClauseSelector) where TSource : IComparable<TSource> {
			return new MinEnumerable<TSource>(source);
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "minClauseSelector is required in method signature.")]
		public static MinEnumerable<TElement> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, MinClause> minClauseSelector, Func<TSource, TElement> elementSelector) where TElement : IComparable<TElement> {
			return new MinEnumerable<TElement>(source.Select(elementSelector));
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "resultSelector is required in method signature.")]
		public static TSource Select<TSource, TResult>(this MinEnumerable<TSource> source, Func<TSource, TResult> resultSelector) where TSource : IComparable<TSource> {
			return source.Enumerable.Min();
		}
		#endregion

		// group x by Sum into g select g
		#region Sum
		[Obsolete("Sum does not support element of this type.", error: true)]
		public static SumEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, SumClause> sumClauseSelector) {
			return new SumEnumerable<TSource>(source);
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "sumClauseSelector is required in method signature.")]
		public static SumEnumerable<int> GroupBy(this IEnumerable<int> source, Func<int, SumClause> sumClauseSelector) {
			return new SumEnumerable<int>(source);
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "sumClauseSelector is required in method signature.")]
		public static SumEnumerable<int?> GroupBy(this IEnumerable<int?> source, Func<int?, SumClause> sumClauseSelector) {
			return new SumEnumerable<int?>(source);
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "sumClauseSelector is required in method signature.")]
		public static SumEnumerable<long> GroupBy(this IEnumerable<long> source, Func<long, SumClause> sumClauseSelector) {
			return new SumEnumerable<long>(source);
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "sumClauseSelector is required in method signature.")]
		public static SumEnumerable<long?> GroupBy(this IEnumerable<long?> source, Func<long?, SumClause> sumClauseSelector) {
			return new SumEnumerable<long?>(source);
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "sumClauseSelector is required in method signature.")]
		public static SumEnumerable<float> GroupBy(this IEnumerable<float> source, Func<float, SumClause> sumClauseSelector) {
			return new SumEnumerable<float>(source);
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "sumClauseSelector is required in method signature.")]
		public static SumEnumerable<float?> GroupBy(this IEnumerable<float?> source, Func<float?, SumClause> sumClauseSelector) {
			return new SumEnumerable<float?>(source);
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "sumClauseSelector is required in method signature.")]
		public static SumEnumerable<double> GroupBy(this IEnumerable<double> source, Func<double, SumClause> sumClauseSelector) {
			return new SumEnumerable<double>(source);
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "sumClauseSelector is required in method signature.")]
		public static SumEnumerable<double?> GroupBy(this IEnumerable<double?> source, Func<double?, SumClause> sumClauseSelector) {
			return new SumEnumerable<double?>(source);
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "sumClauseSelector is required in method signature.")]
		public static SumEnumerable<decimal> GroupBy(this IEnumerable<decimal> source, Func<decimal, SumClause> sumClauseSelector) {
			return new SumEnumerable<decimal>(source);
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "sumClauseSelector is required in method signature.")]
		public static SumEnumerable<decimal?> GroupBy(this IEnumerable<decimal?> source, Func<decimal?, SumClause> sumClauseSelector) {
			return new SumEnumerable<decimal?>(source);
		}
		[Obsolete("Sum does not support element of this type.", error: true)]
		public static SumEnumerable<TElement> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, SumClause> sumClauseSelector, Func<TSource, TElement> elementSelector) {
			return new SumEnumerable<TElement>(source.Select(elementSelector));
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "sumClauseSelector is required in method signature.")]
		public static SumEnumerable<int> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, SumClause> sumClauseSelector, Func<TSource, int> elementSelector) {
			return new SumEnumerable<int>(source.Select(elementSelector));
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "sumClauseSelector is required in method signature.")]
		public static SumEnumerable<int?> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, SumClause> sumClauseSelector, Func<TSource, int?> elementSelector) {
			return new SumEnumerable<int?>(source.Select(elementSelector));
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "sumClauseSelector is required in method signature.")]
		public static SumEnumerable<long> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, SumClause> sumClauseSelector, Func<TSource, long> elementSelector) {
			return new SumEnumerable<long>(source.Select(elementSelector));
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "sumClauseSelector is required in method signature.")]
		public static SumEnumerable<long?> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, SumClause> sumClauseSelector, Func<TSource, long?> elementSelector) {
			return new SumEnumerable<long?>(source.Select(elementSelector));
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "sumClauseSelector is required in method signature.")]
		public static SumEnumerable<float> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, SumClause> sumClauseSelector, Func<TSource, float> elementSelector) {
			return new SumEnumerable<float>(source.Select(elementSelector));
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "sumClauseSelector is required in method signature.")]
		public static SumEnumerable<float?> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, SumClause> sumClauseSelector, Func<TSource, float?> elementSelector) {
			return new SumEnumerable<float?>(source.Select(elementSelector));
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "sumClauseSelector is required in method signature.")]
		public static SumEnumerable<double> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, SumClause> sumClauseSelector, Func<TSource, double> elementSelector) {
			return new SumEnumerable<double>(source.Select(elementSelector));
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "sumClauseSelector is required in method signature.")]
		public static SumEnumerable<double?> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, SumClause> sumClauseSelector, Func<TSource, double?> elementSelector) {
			return new SumEnumerable<double?>(source.Select(elementSelector));
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "sumClauseSelector is required in method signature.")]
		public static SumEnumerable<decimal> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, SumClause> sumClauseSelector, Func<TSource, decimal> elementSelector) {
			return new SumEnumerable<decimal>(source.Select(elementSelector));
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "sumClauseSelector is required in method signature.")]
		public static SumEnumerable<decimal?> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, SumClause> sumClauseSelector, Func<TSource, decimal?> elementSelector) {
			return new SumEnumerable<decimal?>(source.Select(elementSelector));
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "resultSelector is required in method signature.")]
		public static int Select<TResult>(this SumEnumerable<int> source, Func<int, TResult> resultSelector) {
			return source.Enumerable.Sum();
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "resultSelector is required in method signature.")]
		public static int? Select<TResult>(this SumEnumerable<int?> source, Func<int?, TResult> resultSelector) {
			return source.Enumerable.Sum();
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "resultSelector is required in method signature.")]
		public static long Select<TResult>(this SumEnumerable<long> source, Func<long, TResult> resultSelector) {
			return source.Enumerable.Sum();
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "resultSelector is required in method signature.")]
		public static long? Select<TResult>(this SumEnumerable<long?> source, Func<long?, TResult> resultSelector) {
			return source.Enumerable.Sum();
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "resultSelector is required in method signature.")]
		public static float Select<TResult>(this SumEnumerable<float> source, Func<float, TResult> resultSelector) {
			return source.Enumerable.Sum();
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "resultSelector is required in method signature.")]
		public static float? Select<TResult>(this SumEnumerable<float?> source, Func<float?, TResult> resultSelector) {
			return source.Enumerable.Sum();
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "resultSelector is required in method signature.")]
		public static double Select<TResult>(this SumEnumerable<double> source, Func<double, TResult> resultSelector) {
			return source.Enumerable.Sum();
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "resultSelector is required in method signature.")]
		public static double? Select<TResult>(this SumEnumerable<double?> source, Func<double?, TResult> resultSelector) {
			return source.Enumerable.Sum();
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "resultSelector is required in method signature.")]
		public static decimal Select<TResult>(this SumEnumerable<decimal> source, Func<decimal, TResult> resultSelector) {
			return source.Enumerable.Sum();
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "resultSelector is required in method signature.")]
		public static decimal? Select<TResult>(this SumEnumerable<decimal?> source, Func<decimal?, TResult> resultSelector) {
			return source.Enumerable.Sum();
		}
		#endregion

		// group x by Average into g select g
		#region Average
		[Obsolete("Average does not support element of this type.", error: true)]
		public static AverageEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, AverageClause> averageClauseSelector) {
			return new AverageEnumerable<TSource>(source);
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "averageClauseSelector is required in method signature.")]
		public static AverageEnumerable<int> GroupBy(this IEnumerable<int> source, Func<int, AverageClause> averageClauseSelector) {
			return new AverageEnumerable<int>(source);
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "averageClauseSelector is required in method signature.")]
		public static AverageEnumerable<int?> GroupBy(this IEnumerable<int?> source, Func<int?, AverageClause> averageClauseSelector) {
			return new AverageEnumerable<int?>(source);
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "averageClauseSelector is required in method signature.")]
		public static AverageEnumerable<long> GroupBy(this IEnumerable<long> source, Func<long, AverageClause> averageClauseSelector) {
			return new AverageEnumerable<long>(source);
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "averageClauseSelector is required in method signature.")]
		public static AverageEnumerable<long?> GroupBy(this IEnumerable<long?> source, Func<long?, AverageClause> averageClauseSelector) {
			return new AverageEnumerable<long?>(source);
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "averageClauseSelector is required in method signature.")]
		public static AverageEnumerable<float> GroupBy(this IEnumerable<float> source, Func<float, AverageClause> averageClauseSelector) {
			return new AverageEnumerable<float>(source);
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "averageClauseSelector is required in method signature.")]
		public static AverageEnumerable<float?> GroupBy(this IEnumerable<float?> source, Func<float?, AverageClause> averageClauseSelector) {
			return new AverageEnumerable<float?>(source);
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "averageClauseSelector is required in method signature.")]
		public static AverageEnumerable<double> GroupBy(this IEnumerable<double> source, Func<double, AverageClause> averageClauseSelector) {
			return new AverageEnumerable<double>(source);
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "averageClauseSelector is required in method signature.")]
		public static AverageEnumerable<double?> GroupBy(this IEnumerable<double?> source, Func<double?, AverageClause> averageClauseSelector) {
			return new AverageEnumerable<double?>(source);
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "averageClauseSelector is required in method signature.")]
		public static AverageEnumerable<decimal> GroupBy(this IEnumerable<decimal> source, Func<decimal, AverageClause> averageClauseSelector) {
			return new AverageEnumerable<decimal>(source);
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "averageClauseSelector is required in method signature.")]
		public static AverageEnumerable<decimal?> GroupBy(this IEnumerable<decimal?> source, Func<decimal?, AverageClause> averageClauseSelector) {
			return new AverageEnumerable<decimal?>(source);
		}
		[Obsolete("Average does not support element of this type.", error: true)]
		public static AverageEnumerable<TElement> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, AverageClause> averageClauseSelector, Func<TSource, TElement> elementSelector) {
			return new AverageEnumerable<TElement>(source.Select(elementSelector));
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "averageClauseSelector is required in method signature.")]
		public static AverageEnumerable<int> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, AverageClause> averageClauseSelector, Func<TSource, int> elementSelector) {
			return new AverageEnumerable<int>(source.Select(elementSelector));
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "averageClauseSelector is required in method signature.")]
		public static AverageEnumerable<int?> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, AverageClause> averageClauseSelector, Func<TSource, int?> elementSelector) {
			return new AverageEnumerable<int?>(source.Select(elementSelector));
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "averageClauseSelector is required in method signature.")]
		public static AverageEnumerable<long> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, AverageClause> averageClauseSelector, Func<TSource, long> elementSelector) {
			return new AverageEnumerable<long>(source.Select(elementSelector));
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "averageClauseSelector is required in method signature.")]
		public static AverageEnumerable<long?> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, AverageClause> averageClauseSelector, Func<TSource, long?> elementSelector) {
			return new AverageEnumerable<long?>(source.Select(elementSelector));
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "averageClauseSelector is required in method signature.")]
		public static AverageEnumerable<float> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, AverageClause> averageClauseSelector, Func<TSource, float> elementSelector) {
			return new AverageEnumerable<float>(source.Select(elementSelector));
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "averageClauseSelector is required in method signature.")]
		public static AverageEnumerable<float?> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, AverageClause> averageClauseSelector, Func<TSource, float?> elementSelector) {
			return new AverageEnumerable<float?>(source.Select(elementSelector));
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "averageClauseSelector is required in method signature.")]
		public static AverageEnumerable<double> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, AverageClause> averageClauseSelector, Func<TSource, double> elementSelector) {
			return new AverageEnumerable<double>(source.Select(elementSelector));
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "averageClauseSelector is required in method signature.")]
		public static AverageEnumerable<double?> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, AverageClause> averageClauseSelector, Func<TSource, double?> elementSelector) {
			return new AverageEnumerable<double?>(source.Select(elementSelector));
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "averageClauseSelector is required in method signature.")]
		public static AverageEnumerable<decimal> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, AverageClause> averageClauseSelector, Func<TSource, decimal> elementSelector) {
			return new AverageEnumerable<decimal>(source.Select(elementSelector));
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "averageClauseSelector is required in method signature.")]
		public static AverageEnumerable<decimal?> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, AverageClause> averageClauseSelector, Func<TSource, decimal?> elementSelector) {
			return new AverageEnumerable<decimal?>(source.Select(elementSelector));
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "resultSelector is required in method signature.")]
		public static double Select<TResult>(this AverageEnumerable<int> source, Func<int, TResult> resultSelector) {
			return source.Enumerable.Average();
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "resultSelector is required in method signature.")]
		public static double? Select<TResult>(this AverageEnumerable<int?> source, Func<int?, TResult> resultSelector) {
			return source.Enumerable.Average();
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "resultSelector is required in method signature.")]
		public static double Select<TResult>(this AverageEnumerable<long> source, Func<long, TResult> resultSelector) {
			return source.Enumerable.Average();
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "resultSelector is required in method signature.")]
		public static double? Select<TResult>(this AverageEnumerable<long?> source, Func<long?, TResult> resultSelector) {
			return source.Enumerable.Average();
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "resultSelector is required in method signature.")]
		public static float Select<TResult>(this AverageEnumerable<float> source, Func<float, TResult> resultSelector) {
			return source.Enumerable.Average();
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "resultSelector is required in method signature.")]
		public static float? Select<TResult>(this AverageEnumerable<float?> source, Func<float?, TResult> resultSelector) {
			return source.Enumerable.Average();
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "resultSelector is required in method signature.")]
		public static double Select<TResult>(this AverageEnumerable<double> source, Func<double, TResult> resultSelector) {
			return source.Enumerable.Average();
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "resultSelector is required in method signature.")]
		public static double? Select<TResult>(this AverageEnumerable<double?> source, Func<double?, TResult> resultSelector) {
			return source.Enumerable.Average();
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "resultSelector is required in method signature.")]
		public static decimal Select<TResult>(this AverageEnumerable<decimal> source, Func<decimal, TResult> resultSelector) {
			return source.Enumerable.Average();
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "resultSelector is required in method signature.")]
		public static decimal? Select<TResult>(this AverageEnumerable<decimal?> source, Func<decimal?, TResult> resultSelector) {
			return source.Enumerable.Average();
		}
		#endregion

		// group x by StringJoin into g select g
		#region StringJoin
		public static StringJoinEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, StringJoinClause> stringJoinClauseSelector) {
			StringJoinClause stringJoinClause = stringJoinClauseSelector.Invoke(source.FirstOrDefault());
			return new StringJoinEnumerable<TSource>(source, stringJoinClause.Separator);
		}
		public static StringJoinEnumerable<TElement> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, StringJoinClause> stringJoinClauseSelector, Func<TSource, TElement> elementSelector) {
			StringJoinClause stringJoinClause = stringJoinClauseSelector.Invoke(source.FirstOrDefault());
			return new StringJoinEnumerable<TElement>(source.Select(elementSelector), stringJoinClause.Separator);
		}
		[SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "resultSelector is required in method signature.")]
		public static string Select<TSource, TResult>(this StringJoinEnumerable<TSource> source, Func<TSource, TResult> resultSelector) {
			return string.Join(source.Separator, source.Enumerable);
		}
		#endregion
	}
}
