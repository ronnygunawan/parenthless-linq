using FluentAssertions;
using Parenthless;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Xunit;
using static Parenthless.Linq;

namespace Tests {
	public class QueryTests {
		private static readonly int[] INT_SOURCE = { 1, 9, 4, 5, 0, 8, 1, 7 };

		[Fact]
		public void CanFilterUsingOfType() {
			IEnumerable<int>[] enumerables = {
				new List<int>(),
				Array.Empty<int>(),
				ImmutableList<int>.Empty,
				ImmutableArray<int>.Empty
			};

			var listCount = from i in enumerables
							where OfType<List<int>>()
							group i by Count() into count
							select count;
			var arrayCount = from i in enumerables
							 where OfType<int[]>()
							 group i by Count() into count
							 select count;
			var ilistCount = from i in enumerables
							 where OfType<IList<int>>()
							 group i by Count() into count
							 select count;
			var icollectionCount = from i in enumerables
								   where OfType<ICollection<int>>()
								   group i by Count() into count
								   select count;

			listCount.Should().Be(1);
			arrayCount.Should().Be(1);
			ilistCount.Should().Be(4);
			icollectionCount.Should().Be(4);
		}

		[Fact]
		public void CanSkipAndTake() {
			var middleTwoItems = from i in INT_SOURCE
								 where Skip(3)
								 where Take(2)
								 group i by ToList into list
								 select list;

			middleTwoItems.GetType().Should().Be<List<int>>();
			middleTwoItems.Should().ContainInOrder(5, 0);
		}

		[Fact]
		public void CanSkipLast() {
			var withoutLastFour = from i in INT_SOURCE
								  where SkipLast(4)
								  group i by ToList into list
								  select list;

			withoutLastFour.GetType().Should().Be<List<int>>();
			withoutLastFour.Should().ContainInOrder(1, 9, 4, 5);
		}

		[Fact]
		public void CanTakeLast() {
			var lastThree = from i in INT_SOURCE
							where TakeLast(3)
							group i by ToList into list
							select list;

			lastThree.GetType().Should().Be<List<int>>();
			lastThree.Should().ContainInOrder(8, 1, 7);
		}

		[Fact]
		public void CanSkipLastThenTakeLast() {
			var lastTwoOfSubsetWithoutLastFour = from i in INT_SOURCE
												 where SkipLast(4).TakeLast(2)
												 group i by ToList into list
												 select list;

			lastTwoOfSubsetWithoutLastFour.GetType().Should().Be<List<int>>();
			lastTwoOfSubsetWithoutLastFour.Should().ContainInOrder(4, 5);
		}

		[Fact]
		public void CanSkipUsingPredicate() {
			var withoutStartingOddNumbers = from i in INT_SOURCE
											where SkipWhile(i % 2 > 0)
											group i by ToList into list
											select list;

			withoutStartingOddNumbers.GetType().Should().Be<List<int>>();
			withoutStartingOddNumbers.Should().ContainInOrder(4, 5, 0, 8, 1, 7);
		}

		[Fact]
		public void CanTakeUsingPredicate() {
			var beforeFirstZero = from i in INT_SOURCE
								  where TakeWhile(i > 0)
								  group i by ToList into list
								  select list;

			beforeFirstZero.GetType().Should().Be<List<int>>();
			beforeFirstZero.Should().ContainInOrder(1, 9, 4, 5);
		}

		[Fact]
		public void CanDefaultIfEmpty() {
			var defaultedEmpty = from i in Array.Empty<int>()
								 where DefaultIfEmpty
								 group i by ToList into list
								 select list;

			defaultedEmpty.GetType().Should().Be<List<int>>();
			defaultedEmpty.Should().ContainInOrder(0);
		}

		[Fact]
		public void CanConcat() {
			int[] second = { 1, 2, 3, 4, 5 };

			var union = from i in INT_SOURCE
						where Concat(second)
						group i by ToList into list
						select list;

			union.GetType().Should().Be<List<int>>();
			union.Should().ContainInOrder(1, 9, 4, 5, 0, 8, 1, 7, 1, 2, 3, 4, 5);
		}

		[Fact]
		public void CanUnion() {
			int[] second = { 1, 2, 3, 4, 5 };

			var union = from i in INT_SOURCE
						where Union(second)
						group i by ToList into list
						select list;

			union.GetType().Should().Be<List<int>>();
			union.Should().ContainInOrder(1, 9, 4, 5, 0, 8, 7, 2, 3);
		}

		[Fact]
		public void CanExcept() {
			int[] second = { 1, 2, 3, 4, 5 };

			var union = from i in INT_SOURCE
						where Except(second)
						group i by ToList into list
						select list;

			union.GetType().Should().Be<List<int>>();
			union.Should().ContainInOrder(9, 0, 8, 7);
		}

		[Fact]
		public void CanIntersect() {
			int[] second = { 1, 2, 3, 4, 5 };

			var union = from i in INT_SOURCE
						where Intersect(second)
						group i by ToList into list
						select list;

			union.GetType().Should().Be<List<int>>();
			union.Should().ContainInOrder(1, 4, 5);
		}

		[Fact]
		public void CanSelectDistinct() {
			var distinctItems = from i in INT_SOURCE
								orderby Distinct
								group i by ToHashSet into set
								select set;

			distinctItems.GetType().Should().Be<HashSet<int>>();
			distinctItems.Should().ContainInOrder(1, 9, 4, 5, 0, 8, 7);
		}

		[Fact]
		public void CanAggregateIntoList() {
			var list = from i in INT_SOURCE
					   group i by ToList into l
					   select l;

			list.GetType().Should().Be<List<int>>();
		}

		[Fact]
		public void CanAggregateIntoArray() {
			var array = from i in INT_SOURCE
						group i by ToArray into a
						select a;

			array.GetType().Should().Be<int[]>();
		}

		[Fact]
		public void CanAggregateIntoHashSet() {
			var set = from i in INT_SOURCE
					  orderby Distinct
					  group i by ToHashSet into s
					  select s;

			set.GetType().Should().Be<HashSet<int>>();
		}

		[Fact]
		public void CanAggregateIntoFirst() {
			var firstItem = from i in INT_SOURCE
							group i by First into g
							select g;

			firstItem.GetType().Should().Be<int>();
			firstItem.Should().Be(1);
		}

		[Fact]
		public void CanAggregateIntoLast() {
			var lastItem = from i in INT_SOURCE
						   group i by Last into g
						   select g;

			lastItem.GetType().Should().Be<int>();
			lastItem.Should().Be(7);
		}

		[Fact]
		public void CanAggregateIntoSingle() {
			var singleItem = from i in INT_SOURCE
							 where Skip(2)
							 where Take(1)
							 group i by Linq.Single into g
							 select g;

			singleItem.GetType().Should().Be<int>();
			singleItem.Should().Be(4);
		}

		[Fact]
		public void CanAggregateIntoElementAt() {
			var fifthElement = from i in INT_SOURCE
							   group i by ElementAt(4) into g
							   select g;

			fifthElement.GetType().Should().Be<int>();
			fifthElement.Should().Be(0);
		}

		[Fact]
		public void CanAggregateIntoAny() {
			var anyNumberGreaterThan9 = from i in INT_SOURCE
										group i by Any(i > 9) into a
										select a;

			anyNumberGreaterThan9.GetType().Should().Be<bool>();
			anyNumberGreaterThan9.Should().Be(false);
		}

		[Fact]
		public void CanAggregateIntoAll() {
			var allNumbersLessThan10 = from i in INT_SOURCE
									   group i by All(i < 10) into a
									   select a;

			allNumbersLessThan10.GetType().Should().Be<bool>();
			allNumbersLessThan10.Should().Be(true);
		}

		[Fact]
		public void CanAggregateIntoCount() {
			var count = from i in INT_SOURCE
						group i by Count() into c
						select c;

			count.GetType().Should().Be<int>();
			count.Should().Be(8);
		}

		[Fact]
		public void CanAggregateIntoMax() {
			var max = from i in INT_SOURCE
					  group i by Max into m
					  select m;

			max.GetType().Should().Be<int>();
			max.Should().Be(9);
		}

		[Fact]
		public void CanAggregateIntoMin() {
			var min = from i in INT_SOURCE
					  group i by Min into m
					  select m;

			min.GetType().Should().Be<int>();
			min.Should().Be(0);
		}

		[Fact]
		public void CanAggregateIntoSum() {
			var sum = from i in INT_SOURCE
					  group i by Sum into s
					  select s;

			sum.GetType().Should().Be<int>();
			sum.Should().Be(1 + 9 + 4 + 5 + 0 + 8 + 1 + 7);
		}

		[Fact]
		public void CanAggregateIntoAverage() {
			var avg = from i in INT_SOURCE
					  group i by Average into a
					  select a;

			avg.GetType().Should().Be<double>();
			avg.Should().Be(35.0 / 8.0);
		}

		[Fact]
		public void CanAggregateIntoStringJoin() {
			var joined = from i in INT_SOURCE
						 group i by StringJoin() into sj
						 select sj;

			joined.GetType().Should().Be<string>();
			joined.Should().Be("1, 9, 4, 5, 0, 8, 1, 7");
		}
	}
}
