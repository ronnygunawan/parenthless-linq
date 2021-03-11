using Parenthless.Clauses;
using System;
using System.Collections.Generic;
#if NETCOREAPP3_1 || NET5_0
using System.Collections.Immutable;
#endif
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Parenthless {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	public static class LinqExtensions {
		// where OfType<TResult>()
		public static IEnumerable<TResult> Where<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, OfTypeClause<TResult>> _) {
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
		public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, DefaultIfEmptyClause> _) {
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
		public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, DistinctClause> _) {
			return source.Distinct();
		}

		// orderby Distinct
		#region Distinct
		public static IEnumerable<TSource> OrderBy<TSource>(this IEnumerable<TSource> source, Func<TSource, DistinctClause> _) {
			return source.Distinct();
		}
		[Obsolete("'descending' has no effect.")]
		public static IEnumerable<TSource> OrderByDescending<TSource>(this IEnumerable<TSource> source, Func<TSource, DistinctClause> _) {
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
		public static IEnumerable<TSource> OrderBy<TSource>(this IEnumerable<TSource> source, Func<TSource, ReverseClause> _) {
			return source.Reverse();
		}
		[Obsolete("'descending' has no effect.")]
		public static IEnumerable<TSource> OrderByDescending<TSource>(this IEnumerable<TSource> source, Func<TSource, ReverseClause> _) {
			return source.Reverse();
		}
		#endregion

		// group x by ToList into g
		#region ToList
		public static ToListEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, ToListClause> _) {
			return new ToListEnumerable<TSource>(source);
		}
		public static ToListEnumerable<TElement> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, ToListClause> _, Func<TSource, TElement> elementSelector) {
			return new ToListEnumerable<TElement>(source.Select(elementSelector));
		}
		public static ToListEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, Func<IEnumerable<TSource>, List<TSource>>> _) {
			return new ToListEnumerable<TSource>(source);
		}
		public static ToListEnumerable<TElement> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, Func<IEnumerable<TSource>, List<TSource>>> _, Func<TSource, TElement> elementSelector) {
			return new ToListEnumerable<TElement>(source.Select(elementSelector));
		}
		public static List<TResult> Select<TSource, TResult>(this ToListEnumerable<TSource> source, Func<TSource, TResult> resultSelector) {
			if (source.Enumerable is List<TResult> list) {
				return list;
			} else {
				return source.Enumerable.Select(resultSelector).ToList();
			}
		}
		#endregion

		#region ToImmutableList
#if NETCOREAPP3_1 || NET5_0
		public static ToImmutableListEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, ToImmutableListClause> _) {
			return new ToImmutableListEnumerable<TSource>(source);
		}
		public static ToImmutableListEnumerable<TElement> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, ToImmutableListClause> _, Func<TSource, TElement> elementSelector) {
			return new ToImmutableListEnumerable<TElement>(source.Select(elementSelector));
		}
		public static ToImmutableListEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, Func<IEnumerable<TSource>, ImmutableList<TSource>>> _) {
			return new ToImmutableListEnumerable<TSource>(source);
		}
		public static ToImmutableListEnumerable<TElement> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, Func<IEnumerable<TSource>, ImmutableList<TSource>>> _, Func<TSource, TElement> elementSelector) {
			return new ToImmutableListEnumerable<TElement>(source.Select(elementSelector));
		}
		public static ImmutableList<TResult> Select<TSource, TResult>(this ToImmutableListEnumerable<TSource> source, Func<TSource, TResult> resultSelector) {
			if (source.Enumerable is ImmutableList<TResult> list) {
				return list;
			} else {
				return source.Enumerable.Select(resultSelector).ToImmutableList();
			}
		}
#endif
		#endregion

		// group x by ToArray into g
		#region ToArray
		public static ToArrayEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, ToArrayClause> _) {
			return new ToArrayEnumerable<TSource>(source);
		}
		public static ToArrayEnumerable<TElement> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, ToArrayClause> _, Func<TSource, TElement> elementSelector) {
			return new ToArrayEnumerable<TElement>(source.Select(elementSelector));
		}
		public static ToArrayEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, Func<IEnumerable<TSource>, TSource[]>> _) {
			return new ToArrayEnumerable<TSource>(source);
		}
		public static ToArrayEnumerable<TElement> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, Func<IEnumerable<TSource>, TSource[]>> _, Func<TSource, TElement> elementSelector) {
			return new ToArrayEnumerable<TElement>(source.Select(elementSelector));
		}
		public static TResult[] Select<TSource, TResult>(this ToArrayEnumerable<TSource> source, Func<TSource, TResult> resultSelector) {
			return source.Enumerable.Select(resultSelector).ToArray();
		}
		#endregion

		#region ToImmutableArray
#if NETCOREAPP3_1 || NET5_0
		public static ToImmutableArrayEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, ToImmutableArrayClause> _) {
			return new ToImmutableArrayEnumerable<TSource>(source);
		}
		public static ToImmutableArrayEnumerable<TElement> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, ToImmutableArrayClause> _, Func<TSource, TElement> elementSelector) {
			return new ToImmutableArrayEnumerable<TElement>(source.Select(elementSelector));
		}
		public static ToImmutableArrayEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, Func<IEnumerable<TSource>, ImmutableArray<TSource>>> _) {
			return new ToImmutableArrayEnumerable<TSource>(source);
		}
		public static ToImmutableArrayEnumerable<TElement> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, Func<IEnumerable<TSource>, ImmutableArray<TSource>>> _, Func<TSource, TElement> elementSelector) {
			return new ToImmutableArrayEnumerable<TElement>(source.Select(elementSelector));
		}
		public static ImmutableArray<TResult> Select<TSource, TResult>(this ToImmutableArrayEnumerable<TSource> source, Func<TSource, TResult> resultSelector) {
			return source.Enumerable.Select(resultSelector).ToImmutableArray();
		}
#endif
		#endregion

		// group x by ToHashSet into g
		#region ToHashSet
		public static ToHashSetEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, ToHashSetClause> _) {
			return new ToHashSetEnumerable<TSource>(source);
		}
		public static ToHashSetEnumerable<TElement> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, ToHashSetClause> _, Func<TSource, TElement> elementSelector) {
			return new ToHashSetEnumerable<TElement>(source.Select(elementSelector));
		}
		public static ToHashSetEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, Func<IEnumerable<TSource>, HashSet<TSource>>> _) {
			return new ToHashSetEnumerable<TSource>(source);
		}
		public static ToHashSetEnumerable<TElement> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, Func<IEnumerable<TSource>, HashSet<TSource>>> _, Func<TSource, TElement> elementSelector) {
			return new ToHashSetEnumerable<TElement>(source.Select(elementSelector));
		}
		public static HashSet<TResult> Select<TSource, TResult>(this ToHashSetEnumerable<TSource> source, Func<TSource, TResult> resultSelector) {
#if NETSTANDARD2_0
			return new HashSet<TResult>(source.Enumerable.Select(resultSelector));
#else
			return source.Enumerable.Select(resultSelector).ToHashSet();
#endif
		}
		#endregion

		#region ToImmutableHashSet
#if NETCOREAPP3_1 || NET5_0
		public static ToImmutableHashSetEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, ToImmutableHashSetClause> _) {
			return new ToImmutableHashSetEnumerable<TSource>(source);
		}
		public static ToImmutableHashSetEnumerable<TElement> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, ToImmutableHashSetClause> _, Func<TSource, TElement> elementSelector) {
			return new ToImmutableHashSetEnumerable<TElement>(source.Select(elementSelector));
		}
		public static ToImmutableHashSetEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, Func<IEnumerable<TSource>, ImmutableHashSet<TSource>>> _) {
			return new ToImmutableHashSetEnumerable<TSource>(source);
		}
		public static ToImmutableHashSetEnumerable<TElement> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, Func<IEnumerable<TSource>, ImmutableHashSet<TSource>>> _, Func<TSource, TElement> elementSelector) {
			return new ToImmutableHashSetEnumerable<TElement>(source.Select(elementSelector));
		}
		public static ImmutableHashSet<TResult> Select<TSource, TResult>(this ToImmutableHashSetEnumerable<TSource> source, Func<TSource, TResult> resultSelector) {
			return source.Enumerable.Select(resultSelector).ToImmutableHashSet();
		}
#endif
		#endregion

		// group x by ToDictionary(key) into g
		#region ToDictionary(key)
		public static ToDictionaryEnumerable<TSource, TKey> GroupBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, ToDictionaryClause<TKey>> toDictionaryClauseSelector) {
			return new ToDictionaryEnumerable<TSource, TKey>(source, toDictionaryClauseSelector);
		}
		public static ToDictionaryEnumerable<TSource, TKey, TElement> GroupBy<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, ToDictionaryClause<TKey>> toDictionaryClauseSelector, Func<TSource, TElement> elementSelector) {
			return new ToDictionaryEnumerable<TSource, TKey, TElement>(source, item => new ToDictionaryClause<TKey, TElement>(toDictionaryClauseSelector.Invoke(item).Key, elementSelector.Invoke(item)));
		}
		public static Dictionary<TKey, TSource> Select<TSource, TKey, TResult>(this ToDictionaryEnumerable<TSource, TKey> source, Func<TSource, TResult> _) {
			return source.Enumerable.ToDictionary(item => source.ToDictionaryClauseSelector.Invoke(item).Key);
		}
		#endregion

		#region ToImmutableDictionary(key)
#if NETCOREAPP3_1 || NET5_0
		public static ToImmutableDictionaryEnumerable<TSource, TKey> GroupBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, ToImmutableDictionaryClause<TKey>> toImmutableDictionaryClauseSelector) {
			return new ToImmutableDictionaryEnumerable<TSource, TKey>(source, toImmutableDictionaryClauseSelector);
		}
		public static ToImmutableDictionaryEnumerable<TSource, TKey, TElement> GroupBy<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, ToImmutableDictionaryClause<TKey>> toImmutableDictionaryClauseSelector, Func<TSource, TElement> elementSelector) {
			return new ToImmutableDictionaryEnumerable<TSource, TKey, TElement>(source, item => new ToImmutableDictionaryClause<TKey, TElement>(toImmutableDictionaryClauseSelector.Invoke(item).Key, elementSelector.Invoke(item)));
		}
		public static ImmutableDictionary<TKey, TSource> Select<TSource, TKey, TResult>(this ToImmutableDictionaryEnumerable<TSource, TKey> source, Func<TSource, TResult> _) {
			return source.Enumerable.ToImmutableDictionary(item => source.ToImmutableDictionaryClauseSelector.Invoke(item).Key);
		}
#endif
		#endregion

		// group x by ToDictionary(key, element) into g
		#region ToDictionary(key, element)
		public static ToDictionaryEnumerable<TSource, TKey, TElement> GroupBy<TSource, TElement, TKey>(this IEnumerable<TSource> source, Func<TSource, ToDictionaryClause<TKey, TElement>> toDictionaryClauseSelector) {
			return new ToDictionaryEnumerable<TSource, TKey, TElement>(source, toDictionaryClauseSelector);
		}
		public static ToDictionaryEnumerable<TSource, TKey, TElement> GroupBy<TSource, TElement, TKey>(this IEnumerable<TSource> source, Func<TSource, ToDictionaryClause<TKey, TElement>> toDictionaryClauseSelector, Func<TSource, TElement> _) {
			return new ToDictionaryEnumerable<TSource, TKey, TElement>(source, toDictionaryClauseSelector);
		}
		public static Dictionary<TKey, TElement> Select<TSource, TKey, TElement, TResult>(this ToDictionaryEnumerable<TSource, TKey, TElement> source, Func<TSource, TResult> _) {
			return source.Enumerable.ToDictionary(item => source.ToDictionaryClauseSelector.Invoke(item).Key, item => source.ToDictionaryClauseSelector.Invoke(item).Element);
		}
		#endregion

		#region ToImmutableDictionary(key, element)
#if NETCOREAPP3_1 || NET5_0
		public static ToImmutableDictionaryEnumerable<TSource, TKey, TElement> GroupBy<TSource, TElement, TKey>(this IEnumerable<TSource> source, Func<TSource, ToImmutableDictionaryClause<TKey, TElement>> toImmutableDictionaryClauseSelector) {
			return new ToImmutableDictionaryEnumerable<TSource, TKey, TElement>(source, toImmutableDictionaryClauseSelector);
		}
		public static ToImmutableDictionaryEnumerable<TSource, TKey, TElement> GroupBy<TSource, TElement, TKey>(this IEnumerable<TSource> source, Func<TSource, ToImmutableDictionaryClause<TKey, TElement>> toImmutableDictionaryClauseSelector, Func<TSource, TElement> _) {
			return new ToImmutableDictionaryEnumerable<TSource, TKey, TElement>(source, toImmutableDictionaryClauseSelector);
		}
		public static ImmutableDictionary<TKey, TElement> Select<TSource, TKey, TElement, TResult>(this ToImmutableDictionaryEnumerable<TSource, TKey, TElement> source, Func<TSource, TResult> _) {
			return source.Enumerable.ToImmutableDictionary(item => source.ToImmutableDictionaryClauseSelector.Invoke(item).Key, item => source.ToImmutableDictionaryClauseSelector.Invoke(item).Element);
		}
#endif
		#endregion

		// group x by First into g select g
		#region First
		public static FirstEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, FirstClause> _) {
			return new FirstEnumerable<TSource>(source);
		}
		public static FirstEnumerable<TElement> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, FirstClause> _, Func<TSource, TElement> elementSelector) {
			return new FirstEnumerable<TElement>(source.Select(elementSelector));
		}
		public static TResult Select<TSource, TResult>(this FirstEnumerable<TSource> source, Func<TSource, TResult> resultSelector) {
			return source.Enumerable.Select(resultSelector).First();
		}
		#endregion

		// group x by FirstOrDefault into g select g
		#region FirstOrDefault
		public static FirstOrDefaultEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, FirstOrDefaultClause> _) {
			return new FirstOrDefaultEnumerable<TSource>(source);
		}
		public static FirstOrDefaultEnumerable<TElement> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, FirstOrDefaultClause> _, Func<TSource, TElement> elementSelector) {
			return new FirstOrDefaultEnumerable<TElement>(source.Select(elementSelector));
		}
		public static TResult Select<TSource, TResult>(this FirstOrDefaultEnumerable<TSource> source, Func<TSource, TResult> resultSelector) {
			return source.Enumerable.Select(resultSelector).FirstOrDefault();
		}
		#endregion

		// group x by Last into g select g
		#region Last
		public static LastEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, LastClause> _) {
			return new LastEnumerable<TSource>(source);
		}
		public static LastEnumerable<TElement> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, LastClause> _, Func<TSource, TElement> elementSelector) {
			return new LastEnumerable<TElement>(source.Select(elementSelector));
		}
		public static TResult Select<TSource, TResult>(this LastEnumerable<TSource> source, Func<TSource, TResult> resultSelector) {
			return source.Enumerable.Select(resultSelector).Last();
		}
		#endregion

		// group x by LastOrDefault into g select g
		#region LastOrDefault
		public static LastOrDefaultEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, LastOrDefaultClause> _) {
			return new LastOrDefaultEnumerable<TSource>(source);
		}
		public static LastOrDefaultEnumerable<TElement> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, LastOrDefaultClause> _, Func<TSource, TElement> elementSelector) {
			return new LastOrDefaultEnumerable<TElement>(source.Select(elementSelector));
		}
		public static TResult Select<TSource, TResult>(this LastOrDefaultEnumerable<TSource> source, Func<TSource, TResult> resultSelector) {
			return source.Enumerable.Select(resultSelector).LastOrDefault();
		}
		#endregion

		// group x by Single into g select g
		#region Single
		public static SingleEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, SingleClause> _) {
			return new SingleEnumerable<TSource>(source);
		}
		public static SingleEnumerable<TElement> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, SingleClause> _, Func<TSource, TElement> elementSelector) {
			return new SingleEnumerable<TElement>(source.Select(elementSelector));
		}
		public static TResult Select<TSource, TResult>(this SingleEnumerable<TSource> source, Func<TSource, TResult> resultSelector) {
			return source.Enumerable.Select(resultSelector).Single();
		}
		#endregion

		// group x by SingleOrDefault into g select g
		#region SingleOrDefault
		public static SingleOrDefaultEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, SingleOrDefaultClause> _) {
			return new SingleOrDefaultEnumerable<TSource>(source);
		}
		public static SingleOrDefaultEnumerable<TElement> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, SingleOrDefaultClause> _, Func<TSource, TElement> elementSelector) {
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
		public static AnyEnumerable<TSource> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, AnyClause> anyClauseSelector, Func<TSource, TElement> _) {
			return new AnyEnumerable<TSource>(source, anyClauseSelector);
		}
		public static bool Select<TSource, TResult>(this AnyEnumerable<TSource> source, Func<TSource, TResult> _) {
			return source.Enumerable.Any(item => source.AnyClauseSelector.Invoke(item).Condition);
		}
		#endregion

		// group x by All(condition) into g select g
		#region All
		public static AllEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, AllClause> allClauseSelector) {
			return new AllEnumerable<TSource>(source, allClauseSelector);
		}
		public static AllEnumerable<TSource> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, AllClause> allClauseSelector, Func<TSource, TElement> _) {
			return new AllEnumerable<TSource>(source, allClauseSelector);
		}
		public static bool Select<TSource, TResult>(this AllEnumerable<TSource> source, Func<TSource, TResult> _) {
			return source.Enumerable.All(item => source.AllClauseSelector.Invoke(item).Condition);
		}
		#endregion

		// group x by Count() into g select g
		#region Count
		public static CountEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, CountClause> countClauseSelector) {
			return new CountEnumerable<TSource>(source, countClauseSelector);
		}
		public static CountEnumerable<TSource> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, CountClause> countClauseSelector, Func<TSource, TElement> _) {
			return new CountEnumerable<TSource>(source, countClauseSelector);
		}
		public static int Select<TSource, TResult>(this CountEnumerable<TSource> source, Func<TSource, TResult> _) {
			return source.Enumerable.Count(item => source.CountClauseSelector.Invoke(item).Condition);
		}
		#endregion

		// group x by Max into g select g
		#region Max
		public static MaxEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, MaxClause> _) where TSource : IComparable<TSource> {
			return new MaxEnumerable<TSource>(source);
		}
		public static MaxEnumerable<TElement> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, MaxClause> _, Func<TSource, TElement> elementSelector) where TElement : IComparable<TElement> {
			return new MaxEnumerable<TElement>(source.Select(elementSelector));
		}
		public static TSource Select<TSource, TResult>(this MaxEnumerable<TSource> source, Func<TSource, TResult> _) where TSource : IComparable<TSource> {
			return source.Enumerable.Max();
		}
		#endregion

		// group x by Min into g select g
		#region Min
		public static MinEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, MinClause> _) where TSource : IComparable<TSource> {
			return new MinEnumerable<TSource>(source);
		}
		public static MinEnumerable<TElement> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, MinClause> _, Func<TSource, TElement> elementSelector) where TElement : IComparable<TElement> {
			return new MinEnumerable<TElement>(source.Select(elementSelector));
		}
		public static TSource Select<TSource, TResult>(this MinEnumerable<TSource> source, Func<TSource, TResult> _) where TSource : IComparable<TSource> {
			return source.Enumerable.Min();
		}
		#endregion

		// group x by Sum into g select g
		#region Sum
		[Obsolete("Sum does not support element of this type.", error: true)]
		public static SumEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, SumClause> _) {
			return new SumEnumerable<TSource>(source);
		}
		public static SumEnumerable<int> GroupBy(this IEnumerable<int> source, Func<int, SumClause> _) {
			return new SumEnumerable<int>(source);
		}
		public static SumEnumerable<int?> GroupBy(this IEnumerable<int?> source, Func<int?, SumClause> _) {
			return new SumEnumerable<int?>(source);
		}
		public static SumEnumerable<long> GroupBy(this IEnumerable<long> source, Func<long, SumClause> _) {
			return new SumEnumerable<long>(source);
		}
		public static SumEnumerable<long?> GroupBy(this IEnumerable<long?> source, Func<long?, SumClause> _) {
			return new SumEnumerable<long?>(source);
		}
		public static SumEnumerable<float> GroupBy(this IEnumerable<float> source, Func<float, SumClause> _) {
			return new SumEnumerable<float>(source);
		}
		public static SumEnumerable<float?> GroupBy(this IEnumerable<float?> source, Func<float?, SumClause> _) {
			return new SumEnumerable<float?>(source);
		}
		public static SumEnumerable<double> GroupBy(this IEnumerable<double> source, Func<double, SumClause> _) {
			return new SumEnumerable<double>(source);
		}
		public static SumEnumerable<double?> GroupBy(this IEnumerable<double?> source, Func<double?, SumClause> _) {
			return new SumEnumerable<double?>(source);
		}
		public static SumEnumerable<decimal> GroupBy(this IEnumerable<decimal> source, Func<decimal, SumClause> _) {
			return new SumEnumerable<decimal>(source);
		}
		public static SumEnumerable<decimal?> GroupBy(this IEnumerable<decimal?> source, Func<decimal?, SumClause> _) {
			return new SumEnumerable<decimal?>(source);
		}
		public static SumEnumerable<TElement> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, SumClause> _, Func<TSource, TElement> elementSelector) {
			return new SumEnumerable<TElement>(source.Select(elementSelector));
		}
		public static SumEnumerable<int> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, SumClause> _, Func<TSource, int> elementSelector) {
			return new SumEnumerable<int>(source.Select(elementSelector));
		}
		public static SumEnumerable<int?> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, SumClause> _, Func<TSource, int?> elementSelector) {
			return new SumEnumerable<int?>(source.Select(elementSelector));
		}
		public static SumEnumerable<long> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, SumClause> _, Func<TSource, long> elementSelector) {
			return new SumEnumerable<long>(source.Select(elementSelector));
		}
		public static SumEnumerable<long?> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, SumClause> _, Func<TSource, long?> elementSelector) {
			return new SumEnumerable<long?>(source.Select(elementSelector));
		}
		public static SumEnumerable<float> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, SumClause> _, Func<TSource, float> elementSelector) {
			return new SumEnumerable<float>(source.Select(elementSelector));
		}
		public static SumEnumerable<float?> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, SumClause> _, Func<TSource, float?> elementSelector) {
			return new SumEnumerable<float?>(source.Select(elementSelector));
		}
		public static SumEnumerable<double> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, SumClause> _, Func<TSource, double> elementSelector) {
			return new SumEnumerable<double>(source.Select(elementSelector));
		}
		public static SumEnumerable<double?> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, SumClause> _, Func<TSource, double?> elementSelector) {
			return new SumEnumerable<double?>(source.Select(elementSelector));
		}
		public static SumEnumerable<decimal> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, SumClause> _, Func<TSource, decimal> elementSelector) {
			return new SumEnumerable<decimal>(source.Select(elementSelector));
		}
		public static SumEnumerable<decimal?> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, SumClause> _, Func<TSource, decimal?> elementSelector) {
			return new SumEnumerable<decimal?>(source.Select(elementSelector));
		}
		public static int Select<TResult>(this SumEnumerable<int> source, Func<int, TResult> _) {
			return source.Enumerable.Sum();
		}
		public static int? Select<TResult>(this SumEnumerable<int?> source, Func<int?, TResult> _) {
			return source.Enumerable.Sum();
		}
		public static long Select<TResult>(this SumEnumerable<long> source, Func<long, TResult> _) {
			return source.Enumerable.Sum();
		}
		public static long? Select<TResult>(this SumEnumerable<long?> source, Func<long?, TResult> _) {
			return source.Enumerable.Sum();
		}
		public static float Select<TResult>(this SumEnumerable<float> source, Func<float, TResult> _) {
			return source.Enumerable.Sum();
		}
		public static float? Select<TResult>(this SumEnumerable<float?> source, Func<float?, TResult> _) {
			return source.Enumerable.Sum();
		}
		public static double Select<TResult>(this SumEnumerable<double> source, Func<double, TResult> _) {
			return source.Enumerable.Sum();
		}
		public static double? Select<TResult>(this SumEnumerable<double?> source, Func<double?, TResult> _) {
			return source.Enumerable.Sum();
		}
		public static decimal Select<TResult>(this SumEnumerable<decimal> source, Func<decimal, TResult> _) {
			return source.Enumerable.Sum();
		}
		public static decimal? Select<TResult>(this SumEnumerable<decimal?> source, Func<decimal?, TResult> _) {
			return source.Enumerable.Sum();
		}
		#endregion

		// group x by Average into g select g
		#region Average
		[Obsolete("Average does not support element of this type.", error: true)]
		public static AverageEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, AverageClause> averageClauseSelector) {
			return new AverageEnumerable<TSource>(source);
		}
		public static AverageEnumerable<int> GroupBy(this IEnumerable<int> source, Func<int, AverageClause> _) {
			return new AverageEnumerable<int>(source);
		}
		public static AverageEnumerable<int?> GroupBy(this IEnumerable<int?> source, Func<int?, AverageClause> _) {
			return new AverageEnumerable<int?>(source);
		}
		public static AverageEnumerable<long> GroupBy(this IEnumerable<long> source, Func<long, AverageClause> _) {
			return new AverageEnumerable<long>(source);
		}
		public static AverageEnumerable<long?> GroupBy(this IEnumerable<long?> source, Func<long?, AverageClause> _) {
			return new AverageEnumerable<long?>(source);
		}
		public static AverageEnumerable<float> GroupBy(this IEnumerable<float> source, Func<float, AverageClause> _) {
			return new AverageEnumerable<float>(source);
		}
		public static AverageEnumerable<float?> GroupBy(this IEnumerable<float?> source, Func<float?, AverageClause> _) {
			return new AverageEnumerable<float?>(source);
		}
		public static AverageEnumerable<double> GroupBy(this IEnumerable<double> source, Func<double, AverageClause> _) {
			return new AverageEnumerable<double>(source);
		}
		public static AverageEnumerable<double?> GroupBy(this IEnumerable<double?> source, Func<double?, AverageClause> _) {
			return new AverageEnumerable<double?>(source);
		}
		public static AverageEnumerable<decimal> GroupBy(this IEnumerable<decimal> source, Func<decimal, AverageClause> _) {
			return new AverageEnumerable<decimal>(source);
		}
		public static AverageEnumerable<decimal?> GroupBy(this IEnumerable<decimal?> source, Func<decimal?, AverageClause> _) {
			return new AverageEnumerable<decimal?>(source);
		}
		[Obsolete("Average does not support element of this type.", error: true)]
		public static AverageEnumerable<TElement> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, AverageClause> averageClauseSelector, Func<TSource, TElement> elementSelector) {
			return new AverageEnumerable<TElement>(source.Select(elementSelector));
		}
		public static AverageEnumerable<int> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, AverageClause> _, Func<TSource, int> elementSelector) {
			return new AverageEnumerable<int>(source.Select(elementSelector));
		}
		public static AverageEnumerable<int?> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, AverageClause> _, Func<TSource, int?> elementSelector) {
			return new AverageEnumerable<int?>(source.Select(elementSelector));
		}
		public static AverageEnumerable<long> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, AverageClause> _, Func<TSource, long> elementSelector) {
			return new AverageEnumerable<long>(source.Select(elementSelector));
		}
		public static AverageEnumerable<long?> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, AverageClause> _, Func<TSource, long?> elementSelector) {
			return new AverageEnumerable<long?>(source.Select(elementSelector));
		}
		public static AverageEnumerable<float> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, AverageClause> _, Func<TSource, float> elementSelector) {
			return new AverageEnumerable<float>(source.Select(elementSelector));
		}
		public static AverageEnumerable<float?> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, AverageClause> _, Func<TSource, float?> elementSelector) {
			return new AverageEnumerable<float?>(source.Select(elementSelector));
		}
		public static AverageEnumerable<double> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, AverageClause> _, Func<TSource, double> elementSelector) {
			return new AverageEnumerable<double>(source.Select(elementSelector));
		}
		public static AverageEnumerable<double?> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, AverageClause> _, Func<TSource, double?> elementSelector) {
			return new AverageEnumerable<double?>(source.Select(elementSelector));
		}
		public static AverageEnumerable<decimal> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, AverageClause> _, Func<TSource, decimal> elementSelector) {
			return new AverageEnumerable<decimal>(source.Select(elementSelector));
		}
		public static AverageEnumerable<decimal?> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, AverageClause> _, Func<TSource, decimal?> elementSelector) {
			return new AverageEnumerable<decimal?>(source.Select(elementSelector));
		}
		public static double Select<TResult>(this AverageEnumerable<int> source, Func<int, TResult> _) {
			return source.Enumerable.Average();
		}
		public static double? Select<TResult>(this AverageEnumerable<int?> source, Func<int?, TResult> _) {
			return source.Enumerable.Average();
		}
		public static double Select<TResult>(this AverageEnumerable<long> source, Func<long, TResult> _) {
			return source.Enumerable.Average();
		}
		public static double? Select<TResult>(this AverageEnumerable<long?> source, Func<long?, TResult> _) {
			return source.Enumerable.Average();
		}
		public static float Select<TResult>(this AverageEnumerable<float> source, Func<float, TResult> _) {
			return source.Enumerable.Average();
		}
		public static float? Select<TResult>(this AverageEnumerable<float?> source, Func<float?, TResult> _) {
			return source.Enumerable.Average();
		}
		public static double Select<TResult>(this AverageEnumerable<double> source, Func<double, TResult> _) {
			return source.Enumerable.Average();
		}
		public static double? Select<TResult>(this AverageEnumerable<double?> source, Func<double?, TResult> _) {
			return source.Enumerable.Average();
		}
		public static decimal Select<TResult>(this AverageEnumerable<decimal> source, Func<decimal, TResult> _) {
			return source.Enumerable.Average();
		}
		public static decimal? Select<TResult>(this AverageEnumerable<decimal?> source, Func<decimal?, TResult> _) {
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
		public static string Select<TSource, TResult>(this StringJoinEnumerable<TSource> source, Func<TSource, TResult> _) {
			return string.Join(source.Separator, source.Enumerable);
		}
		#endregion

		// group x by Contains(value) into g select g
		#region Contains
		public static ContainsEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, ContainsClause<TSource>> containsClauseSelector) {
			ContainsClause<TSource> containsClause = containsClauseSelector.Invoke(source.FirstOrDefault());
			return new ContainsEnumerable<TSource>(source, containsClause.Value, containsClause.Comparer);
		}
		public static ContainsEnumerable<TElement> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, ContainsClause<TElement>> containsClauseSelector, Func<TSource, TElement> elementSelector) {
			ContainsClause<TElement> containsClause = containsClauseSelector.Invoke(source.FirstOrDefault());
			return new ContainsEnumerable<TElement>(source.Select(elementSelector), containsClause.Value, containsClause.Comparer);
		}
		public static bool Select<TSource, TResult>(this ContainsEnumerable<TSource> source, Func<TSource, TResult> _) {
			return source.Enumerable.Contains(source.Value, source.Comparer);
		}
		#endregion

		// group x by ContainsAny(values) into g select g
		#region ContainsAny
		public static ContainsAnyEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, ContainsAnyClause<TSource>> containsAnyClauseSelector) {
			ContainsAnyClause<TSource> containsAnyClause = containsAnyClauseSelector.Invoke(source.FirstOrDefault());
			return new ContainsAnyEnumerable<TSource>(source, containsAnyClause.Values, containsAnyClause.Comparer);
		}
		public static ContainsAnyEnumerable<TElement> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, ContainsAnyClause<TElement>> containsAnyClauseSelector, Func<TSource, TElement> elementSelector) {
			ContainsAnyClause<TElement> containsAnyClause = containsAnyClauseSelector.Invoke(source.FirstOrDefault());
			return new ContainsAnyEnumerable<TElement>(source.Select(elementSelector), containsAnyClause.Values, containsAnyClause.Comparer);
		}
		public static bool Select<TSource, TResult>(this ContainsAnyEnumerable<TSource> source, Func<TSource, TResult> resultSelector) {
#if NETSTANDARD2_0
			HashSet<TSource> values = new(source.Values.Distinct(), source.Comparer);
#else
			HashSet<TSource> values = source.Values.Distinct().ToHashSet(source.Comparer);
#endif
			return source.Enumerable.Any(item => values.Contains(item));
		}
		#endregion

		// group x by ContainsAll(values) into g select g
		#region ContainsAll
		public static ContainsAllEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, ContainsAllClause<TSource>> containsAllClauseSelector) {
			ContainsAllClause<TSource> containsAllClause = containsAllClauseSelector.Invoke(source.FirstOrDefault());
			return new ContainsAllEnumerable<TSource>(source, containsAllClause.Values, containsAllClause.Comparer);
		}
		public static ContainsAllEnumerable<TElement> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, ContainsAllClause<TElement>> containsAllClauseSelector, Func<TSource, TElement> elementSelector) {
			ContainsAllClause<TElement> containsAllClause = containsAllClauseSelector.Invoke(source.FirstOrDefault());
			return new ContainsAllEnumerable<TElement>(source.Select(elementSelector), containsAllClause.Values, containsAllClause.Comparer);
		}
		public static bool Select<TSource, TResult>(this ContainsAllEnumerable<TSource> source, Func<TSource, TResult> resultSelector) {
#if NETSTANDARD2_0
			HashSet<TSource> items = new(source.Enumerable.Distinct(), source.Comparer);
#else
			HashSet<TSource> items = source.Enumerable.Distinct().ToHashSet(source.Comparer);
#endif
			return source.Values.All(value => items.Contains(value));
		}
		#endregion

		// group x by SequenceEqual(second) into g select g
		#region SequenceEqual
		public static SequenceEqualEnumerable<TSource> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, SequenceEqualClause<TSource>> sequenceEqualClauseSelector) {
			SequenceEqualClause<TSource> sequenceEqualClause = sequenceEqualClauseSelector.Invoke(source.FirstOrDefault());
			return new SequenceEqualEnumerable<TSource>(source, sequenceEqualClause.Second, sequenceEqualClause.Comparer);
		}
		public static SequenceEqualEnumerable<TElement> GroupBy<TSource, TElement>(this IEnumerable<TSource> source, Func<TSource, SequenceEqualClause<TElement>> sequenceEqualClauseSelector, Func<TSource, TElement> elementSelector) {
			SequenceEqualClause<TElement> sequenceEqualClause = sequenceEqualClauseSelector.Invoke(source.FirstOrDefault());
			return new SequenceEqualEnumerable<TElement>(source.Select(elementSelector), sequenceEqualClause.Second, sequenceEqualClause.Comparer);
		}
		public static bool Select<TSource, TResult>(this SequenceEqualEnumerable<TSource> source, Func<TSource, TResult> _) {
			return source.Enumerable.SequenceEqual(source.Second, source.Comparer);
		}
		#endregion
	}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
