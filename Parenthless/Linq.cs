using Parenthless.Clauses;
using System;
using System.Collections.Generic;
#if NETCOREAPP3_1 || NET5_0
using System.Collections.Immutable;
#endif

namespace Parenthless {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	public static class Linq {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

		/*
		 * WHERE CLAUSES
		 */

		// 1.0.3
		/// <summary>
		/// Filters the elements of an <see cref="IEnumerable{T}"/> based on a specified type.
		/// <para>Usage: where OfType&lt;T&gt;()</para>
		/// </summary>
		/// <typeparam name="TResult">The type to filter the elements of the sequence on.</typeparam>
		public static OfTypeClause<TResult> OfType<TResult>() => new();

		// 1.0.2
		/// <summary>
		/// Bypasses a specified number of elements in a sequence and then returns the remaining elements.
		/// <para>Usage: where Skip(count)</para>
		/// </summary>
		/// <param name="count">The number of elements to skip before returning the remaining elements.</param>
		public static SkipClause Skip(int count) => new(count);

		// 1.0.2
		/// <summary>
		/// Returns a specified number of contiguous elements from the start of a sequence.
		/// <para>Usage: where Take(count)</para>
		/// </summary>
		/// <param name="count">The number of elements to return.</param>
		public static TakeClause Take(int count) => new(count);

		// 1.0.3
		/// <summary>
		/// Bypasses a specified number of elements in the end of a sequence and then returns the remaining elements.
		/// <para>Usage: where SkipLast(count)</para>
		/// </summary>
		/// <param name="count">The number of elements to skip before returning the remaining elements.</param>
		public static SkipLastClause SkipLast(int count) => new(count);

		// 1.0.3
		/// <summary>
		/// Returns a specified number of contiguous elements from the end of a sequence.
		/// <para>Usage: where TakeLast(count)</para>
		/// </summary>
		/// <param name="count">The number of elements to return.</param>
		public static TakeLastClause TakeLast(int count) => new(count);

		// 1.0.3
		/// <summary>
		/// Bypasses elements in a sequence as long as a specified condition is true and
		/// then returns the remaining elements. The element&#39;s index is used in the logic
		/// of the predicate function.
		/// <para>Usage: where SkipWhile(condition)</para>
		/// </summary>
		/// <param name="condition"></param>
		public static SkipWhileClause SkipWhile(bool condition) => new(condition);

		// 1.0.3
		/// <summary>
		/// Returns elements from a sequence as long as a specified condition is true. The
		/// element&#39;s index is used in the logic of the predicate function.
		/// <para>Usage: where TakeWhile(condition)</para>
		/// </summary>
		/// <param name="condition"></param>
		public static TakeWhileClause TakeWhile(bool condition) => new(condition);

		// 1.0.3
		/// <summary>
		/// Returns the elements of the specified sequence or the type parameter&#39;s default
		/// value in a singleton collection if the sequence is empty.
		/// <para>Usage: where DefaultIfEmpty</para>
		/// </summary>
		public static DefaultIfEmptyClause DefaultIfEmpty { get; } = new DefaultIfEmptyClause();

		// 1.0.3
		/// <summary>
		/// Concatenates two sequences.
		/// <para>Usage: where Concat(second)</para>
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <param name="second">The sequence to concatenate to the first sequence.</param>
		public static ConcatClause<TSource> Concat<TSource>(IEnumerable<TSource> second) => new(second);

		// 1.0.3
		/// <summary>
		/// Produces the set union of two sequences by using the default equality comparer.
		/// <para>Usage: where Union(second)</para>
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <param name="second">An System.Collections.Generic.IEnumerable`1 whose distinct elements form the second set for the union.</param>
		public static UnionClause<TSource> Union<TSource>(IEnumerable<TSource> second) => new(second);

		// 1.0.3
		/// <summary>
		/// Produces the set union of two sequences by using a specified <see cref="IEqualityComparer{T}"/>
		/// <para>Usage: where Union(second, comparer)</para>
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <param name="second">An <see cref="IEnumerable{T}"/> whose distinct elements form the second set for the union.</param>
		/// <param name="comparer">The <see cref="IEqualityComparer{T}"/> to compare values.</param>
		public static UnionClause<TSource> Union<TSource>(IEnumerable<TSource> second, IEqualityComparer<TSource> comparer) => new(second, comparer);

		// 1.0.3
		/// <summary>
		/// Produces the set difference of two sequences by using the default equality comparer
		/// to compare values.
		/// <para>Usage: where Except(second)</para>
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <param name="second">An <see cref="IEnumerable{T}"/> whose elements that also occur in
		/// the first sequence will cause those elements to be removed from the returned
		/// sequence.</param>
		public static ExceptClause<TSource> Except<TSource>(IEnumerable<TSource> second) => new(second);

		// 1.0.3
		/// <summary>
		/// Produces the set difference of two sequences by using the specified <see cref="IEqualityComparer{T}"/> to compare values.
		/// <para>Usage: where Except(second, comparer)</para>
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <param name="second">An <see cref="IEnumerable{T}"/> whose elements that also occur in
		/// the first sequence will cause those elements to be removed from the returned
		/// sequence.</param>
		/// <param name="comparer">An <see cref="IEqualityComparer{T}"/> to compare values.</param>
		public static ExceptClause<TSource> Except<TSource>(IEnumerable<TSource> second, IEqualityComparer<TSource> comparer) => new(second, comparer);

		// 1.0.3
		/// <summary>
		/// Produces the set intersection of two sequences by using the default equality
		/// comparer to compare values.
		/// <para>Usage: where Intersect(second)</para>
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <param name="second">An <see cref="IEnumerable{T}"/> whose distinct elements that also
		/// appear in the first sequence will be returned.</param>
		public static IntersectClause<TSource> Intersect<TSource>(IEnumerable<TSource> second) => new(second);

		// 1.0.3
		/// <summary>
		/// Produces the set intersection of two sequences by using the specified <see cref="IEqualityComparer{T}"/>
		/// to compare values.
		/// <para>Usage: where Intersect(second, comparer)</para>
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <param name="second">An <see cref="IEnumerable{T}"/> whose distinct elements that also
		/// appear in the first sequence will be returned.</param>
		/// <param name="comparer">An <see cref="IEqualityComparer{T}"/> to compare values.</param>
		public static IntersectClause<TSource> Intersect<TSource>(IEnumerable<TSource> second, IEqualityComparer<TSource> comparer) => new(second, comparer);


		/*
		 * ORDERBY CLAUSES
		 */

		// 1.0.2
		/// <summary>
		/// Returns distinct elements from a sequence by using the default equality comparer
		/// to compare values.
		/// <para>Usage: orderby Distinct</para>
		/// <para>Usage: where Distinct</para>
		/// </summary>
		public static DistinctClause Distinct { get; } = new DistinctClause();

		// 1.0.2
		/// <summary>
		/// Inverts the order of the elements in a sequence.
		/// <para>Usage: orderby Reverse</para>
		/// </summary>
		public static ReverseClause Reverse { get; } = new ReverseClause();

		/*
		 * GROUPBY CLAUSES
		 */

		// 1.0.2
		/// <summary>
		/// Creates a <see cref="List{T}"/> from <see cref="IEnumerable{T}"/>.
		/// <para>Usage: group x by ToList into list</para>
		/// </summary>
		public static ToListClause ToList { get; } = new ToListClause();

#if NETCOREAPP3_1 || NET5_0
		// 1.0.5
		/// <summary>
		/// Creates an <see cref="ImmutableList{T}"/> from <see cref="IEnumerable{T}"/>.
		/// <para>Usage: group x by ToImmutableList into list</para>
		/// </summary>
		public static ToImmutableListClause ToImmutableList { get; } = new ToImmutableListClause();
#endif

		// 1.0.3
		/// <summary>
		/// Creates an array from a <see cref="IEnumerable{T}"/>.
		/// <para>Usage: group x by ToArray into array</para>
		/// </summary>
		public static ToArrayClause ToArray { get; } = new ToArrayClause();

#if NETCOREAPP3_1 || NET5_0
		// 1.0.5
		/// <summary>
		/// Creates an <see cref="ImmutableArray{T}"/> from a <see cref="IEnumerable{T}"/>.
		/// <para>Usage: group x by ToImmutableArray into array</para>
		/// </summary>
		public static ToImmutableArrayClause ToImmutableArray { get; } = new ToImmutableArrayClause();
#endif

		// 1.0.2
		/// <summary>
		/// Creates a <see cref="HashSet{T}"/> from an <see cref="IEnumerable{T}"/>.
		/// <para>Usage: group x by ToHashSet into set</para>
		/// </summary>
		public static ToHashSetClause ToHashSet { get; } = new ToHashSetClause();

#if NETCOREAPP3_1 || NET5_0
		// 1.0.5
		/// <summary>
		/// Creates an <see cref="ImmutableHashSet{T}"/> from an <see cref="IEnumerable{T}"/>.
		/// <para>Usage: group x by ToImmutableHashSet into set</para>
		/// </summary>
		public static ToImmutableHashSetClause ToImmutableHashSet { get; } = new ToImmutableHashSetClause();
#endif

		// 1.0.3
		/// <summary>
		/// Creates a <see cref="Dictionary{TKey, TValue}"/> from an <see cref="IEnumerable{T}"/>
		/// according to a specified key.
		/// <para>Usage: group element by ToDictionary(element.Key) into dict</para>
		/// </summary>
		/// <typeparam name="TKey">The type of the key.</typeparam>
		/// <param name="key">A key from each element.</param>
		/// <exception cref="ArgumentException">Method produces duplicate keys for two elements.</exception>
		public static ToDictionaryClause<TKey> ToDictionary<TKey>(TKey key) => new(key);

#if NETCOREAPP3_1 || NET5_0
		// 1.0.5
		/// <summary>
		/// Creates an <see cref="ImmutableDictionary{TKey, TValue}"/> from an <see cref="IEnumerable{T}"/>
		/// according to a specified key.
		/// <para>Usage: group element by ToImmutableDictionary(element.Key) into dict</para>
		/// </summary>
		/// <typeparam name="TKey">The type of the key.</typeparam>
		/// <param name="key">A key from each element.</param>
		/// <exception cref="ArgumentException">Method produces duplicate keys for two elements.</exception>
		public static ToImmutableDictionaryClause<TKey> ToImmutableDictionary<TKey>(TKey key) => new(key);
#endif

		// 1.0.3
		/// <summary>
		/// Creates a <see cref="Dictionary{TKey, TValue}"/> from an <see cref="IEnumerable{T}"/>
		/// according to specified key and element.
		/// <para>Usage: group element by ToDictionary(element.Key, element) into dict</para>
		/// </summary>
		/// <typeparam name="TKey">The type of the key.</typeparam>
		/// <typeparam name="TElement">The type of the value of the element.</typeparam>
		/// <param name="key">A key from each element.</param>
		/// <param name="element">Element value from each element.</param>
		/// <exception cref="ArgumentException">Method produces duplicate keys for two elements.</exception>
		public static ToDictionaryClause<TKey, TElement> ToDictionary<TKey, TElement>(TKey key, TElement element) => new(key, element);

#if NETCOREAPP3_1 || NET5_0
		// 1.0.5
		/// <summary>
		/// Creates an <see cref="ImmutableDictionary{TKey, TValue}"/> from an <see cref="IEnumerable{T}"/>
		/// according to specified key and element.
		/// <para>Usage: group element by ToImmutableDictionary(element.Key, element) into dict</para>
		/// </summary>
		/// <typeparam name="TKey">The type of the key.</typeparam>
		/// <typeparam name="TElement">The type of the value of the element.</typeparam>
		/// <param name="key">A key from each element.</param>
		/// <param name="element">Element value from each element.</param>
		/// <exception cref="ArgumentException">Method produces duplicate keys for two elements.</exception>
		public static ToImmutableDictionaryClause<TKey, TElement> ToImmutableDictionary<TKey, TElement>(TKey key, TElement element) => new(key, element);
#endif

		// 1.0.2
		/// <summary>
		/// Returns the first element of a sequence.
		/// <para>Usage: group x by First into first</para>
		/// </summary>
		/// <exception cref="InvalidOperationException">The source sequence is empty.</exception>
		public static FirstClause First { get; } = new FirstClause();

		// 1.0.2
		/// <summary>
		/// Returns the first element of a sequence, or a default value if the sequence contains
		/// no elements.
		/// <para>Usage: group x by FirstOrDefault into first</para>
		/// </summary>
		public static FirstOrDefaultClause FirstOrDefault { get; } = new FirstOrDefaultClause();

		// 1.0.2
		/// <summary>
		/// Returns the last element of a sequence.
		/// <para>Usage: group x by Last into last</para>
		/// </summary>
		/// <exception cref="InvalidOperationException">The source sequence is empty.</exception>
		public static LastClause Last { get; } = new LastClause();

		// 1.0.2
		/// <summary>
		/// Returns the last element of a sequence, or a default value if the sequence contains
		/// no elements.
		/// <para>Usage: group x by LastOrDefault into last</para>
		/// </summary>
		public static LastOrDefaultClause LastOrDefault { get; } = new LastOrDefaultClause();

		// 1.0.2
		/// <summary>
		/// Returns the only element of a sequence, and throws an exception if there is not
		/// exactly one element in the sequence.
		/// <para>Usage: group x by Single into g</para>
		/// </summary>
		/// <exception cref="InvalidOperationException">The input sequence contains more than one element. -or- The input sequence is empty.</exception>
		public static SingleClause Single { get; } = new SingleClause();

		// 1.0.2
		/// <summary>
		/// Returns the only element of a sequence, or a default value if the sequence is
		/// empty; this method throws an exception if there is more than one element in the
		/// sequence.
		/// <para>Usage: group x by SingleOrDefault into g</para>
		/// </summary>
		/// <exception cref="InvalidOperationException">The input sequence contains more than one element.</exception>
		public static SingleOrDefaultClause SingleOrDefault { get; } = new SingleOrDefaultClause();

		// 1.0.3
		/// <summary>
		/// Returns the element at a specified index in a sequence.
		/// <para>Usage: group x by ElementAt into elem</para>
		/// </summary>
		/// <param name="index">The zero-based index of the element to retrieve.</param>
		/// <exception cref="ArgumentOutOfRangeException">index is less than 0 or greater than or equal to the number of elements in source.</exception>
		public static ElementAtClause ElementAt(int index) => new(index);

		// 1.0.3
		/// <summary>
		/// Returns the element at a specified index in a sequence or a default value if
		/// the index is out of range.
		/// <para>Usage: group x by ElementAtOrDefault into elem</para>
		/// </summary>
		/// <param name="index">The zero-based index of the element to retrieve.</param>
		public static ElementAtOrDefaultClause ElementAtOrDefault(int index) => new(index);

		// 1.0.3
		/// <summary>
		/// Determines whether any element of a sequence satisfies a condition.
		/// <para>Usage: group x by Any(condition) into any</para>
		/// </summary>
		/// <param name="condition"></param>
		public static AnyClause Any(bool condition = true) => new(condition);

		// 1.0.3
		/// <summary>
		/// Determines whether all elements of a sequence satisfy a condition.
		/// <para>Usage: group x by All(condition) into all</para>
		/// </summary>
		/// <param name="condition"></param>
		public static AllClause All(bool condition) => new(condition);

		// 1.0.3
		/// <summary>
		/// Returns a number that represents how many elements in the specified sequence
		/// satisfy a condition.
		/// <para>Usage: group x by Count(condition) into count</para>
		/// </summary>
		/// <param name="condition"></param>
		public static CountClause Count(bool condition = true) => new(condition);

		// 1.0.3
		/// <summary>
		/// Returns the maximum value in a sequence.
		/// <para>Usage: group x by Max into max</para>
		/// </summary>
		public static MaxClause Max { get; } = new MaxClause();

		// 1.0.3
		/// <summary>
		/// Returns the minimum value in a sequence.
		/// <para>Usage: group x by Min into min</para>
		/// </summary>
		public static MinClause Min { get; } = new MinClause();

		// 1.0.3
		/// <summary>
		/// Computes the sum of a sequence.
		/// <para>Usage: group x by Sum into sum</para>
		/// </summary>
		public static SumClause Sum { get; } = new SumClause();

		// 1.0.3
		/// <summary>
		/// Computes the average of a sequence.
		/// <para>Usage: group x by Average into avg</para>
		/// </summary>
		public static AverageClause Average { get; } = new AverageClause();

		// 1.0.3
		/// <summary>
		/// Concatenates the members of a collection, using the specified separator between
		/// each member.
		/// <para>Usage: group x by StringJoin(", ") into joined</para>
		/// </summary>
		/// <param name="separator">The string to use as a separator.separator is included in the returned string
		/// only if values has more than one element.</param>
		public static StringJoinClause StringJoin(string separator = ", ") => new(separator);

		// 1.0.3
		/// <summary>
		/// Determines whether a sequence contains a specified element by using the default
		/// equality comparer.
		/// <para>Usage: group x by Contains(value) into c</para>
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of source.</typeparam>
		/// <param name="value">The value to locate in the sequence.</param>
		public static ContainsClause<TSource> Contains<TSource>(TSource value) => new(value);

		// 1.0.3
		/// <summary>
		/// Determines whether a sequence contains a specified element by using a specified
		/// <see cref="IEqualityComparer{T}"/>.
		/// <para>Usage: group x by Contains(value, comparer) into c</para>
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of source.</typeparam>
		/// <param name="value">The value to locate in the sequence.</param>
		/// <param name="comparer">An equality comparer to compare values.</param>
		public static ContainsClause<TSource> Contains<TSource>(TSource value, IEqualityComparer<TSource> comparer) => new(value, comparer);

		// 1.0.3
		/// <summary>
		/// Determines whether a sequence contains any of specified elements by using the
		/// default equality comparer.
		/// <para>Usage: group x by ContainsAny(values) into c</para>
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of source.</typeparam>
		/// <param name="values">Values to locate in the sequence.</param>
		public static ContainsAnyClause<TSource> ContainsAny<TSource>(IEnumerable<TSource> values) => new(values);

		// 1.0.3
		/// <summary>
		/// Determines whether a sequence contains any of specified elements by using the
		/// default equality comparer.
		/// <para>Usage: group x by ContainsAny(value1, value2, value3) into c</para>
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of source.</typeparam>
		/// <param name="values">Values to locate in the sequence.</param>
		public static ContainsAnyClause<TSource> ContainsAny<TSource>(params TSource[] values) => new(values);

		// 1.0.3
		/// <summary>
		/// Determines whether a sequence contains any of specified elements by using a
		/// specified <see cref="IEqualityComparer{T}"/>
		/// <para>Usage: group x by ContainsAny(values, comparer) into c</para>
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of source.</typeparam>
		/// <param name="values">Values to locate in the sequence.</param>
		/// <param name="comparer">An equality comparer to compare values.</param>
		public static ContainsAnyClause<TSource> ContainsAny<TSource>(IEnumerable<TSource> values, IEqualityComparer<TSource> comparer) => new(values, comparer);

		// 1.0.3
		/// <summary>
		/// Determines whether a sequence contains all of specified elements by using the
		/// default equality comparer.
		/// <para>Usage: group x by ContainsAll(value1, value2, value3) into c</para>
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of source.</typeparam>
		/// <param name="values">Values to locate in the sequence.</param>
		public static ContainsAllClause<TSource> ContainsAll<TSource>(IEnumerable<TSource> values) => new(values);

		// 1.0.3
		/// <summary>
		/// Determines whether a sequence contains all of specified elements by using the
		/// default equality comparer.
		/// <para>Usage: group x by ContainsAll(values) into c</para>
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of source.</typeparam>
		/// <param name="values">Values to locate in the sequence.</param>
		public static ContainsAllClause<TSource> ContainsAll<TSource>(params TSource[] values) => new(values);

		// 1.0.3
		/// <summary>
		/// Determines whether a sequence contains all of specified elements by using a
		/// specified <see cref="IEqualityComparer{T}"/>
		/// <para>Usage: group x by ContainsAll(values, comparer) into c</para>
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of source.</typeparam>
		/// <param name="values">Values to locate in the sequence.</param>
		/// <param name="comparer">An equality comparer to compare values.</param>
		public static ContainsAllClause<TSource> ContainsAll<TSource>(IEnumerable<TSource> values, IEqualityComparer<TSource> comparer) => new(values, comparer);

		// 1.0.3
		/// <summary>
		/// Determines whether two sequences are equal by comparing the elements by using
		/// the default equality comparer for their type.
		/// <para>Usage: group x by SequenceEqual(second) into eq</para>
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <param name="second">An <see cref="IEnumerable{T}"/> to compare to the first sequence.</param>
		public static SequenceEqualClause<TSource> SequenceEqual<TSource>(IEnumerable<TSource> second) => new(second);

		// 1.0.3
		/// <summary>
		/// Determines whether two sequences are equal by comparing their elements by using
		/// a specified <see cref="IEqualityComparer{T}"/>.
		/// <para>Usage: group x by SequenceEqual(second, comparer) into eq</para>
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <param name="second">An <see cref="IEnumerable{T}"/> to compare to the first sequence.</param>
		/// <param name="comparer">An <see cref="IEqualityComparer{T}"/> to use to compare elements.</param>
		public static SequenceEqualClause<TSource> SequenceEqual<TSource>(IEnumerable<TSource> second, IEqualityComparer<TSource> comparer) => new(second, comparer);
	}
}
