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

			int listCount = from i in enumerables
							where OfType<List<int>>()
							group i by Count() into count
							select count;
			int arrayCount = from i in enumerables
							 where OfType<int[]>()
							 group i by Count() into count
							 select count;
			int ilistCount = from i in enumerables
							 where OfType<IList<int>>()
							 group i by Count() into count
							 select count;
			int icollectionCount = from i in enumerables
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
			List<int> middleTwoItems = from i in INT_SOURCE
									   where Skip(3)
									   where Take(2)
									   group i by ToList into list
									   select list;

			middleTwoItems.GetType().Should().Be<List<int>>();
			middleTwoItems.Should().ContainInOrder(5, 0);
		}

		[Fact]
		public void CanSkipLast() {
			List<int> withoutLastFour = from i in INT_SOURCE
										where SkipLast(4)
										group i by ToList into list
										select list;

			withoutLastFour.GetType().Should().Be<List<int>>();
			withoutLastFour.Should().ContainInOrder(1, 9, 4, 5);
		}

		[Fact]
		public void CanTakeLast() {
			List<int> lastThree = from i in INT_SOURCE
								  where TakeLast(3)
								  group i by ToList into list
								  select list;

			lastThree.GetType().Should().Be<List<int>>();
			lastThree.Should().ContainInOrder(8, 1, 7);
		}

		[Fact]
		public void CanSkipLastThenTakeLast() {
			List<int> lastTwoOfSubsetWithoutLastFour = from i in INT_SOURCE
													   where SkipLast(4).TakeLast(2)
													   group i by ToList into list
													   select list;

			lastTwoOfSubsetWithoutLastFour.GetType().Should().Be<List<int>>();
			lastTwoOfSubsetWithoutLastFour.Should().ContainInOrder(4, 5);
		}

		[Fact]
		public void CanSkipUsingPredicate() {
			List<int> withoutStartingOddNumbers = from i in INT_SOURCE
												  where SkipWhile(i % 2 > 0)
												  group i by ToList into list
												  select list;

			withoutStartingOddNumbers.GetType().Should().Be<List<int>>();
			withoutStartingOddNumbers.Should().ContainInOrder(4, 5, 0, 8, 1, 7);
		}

		[Fact]
		public void CanTakeUsingPredicate() {
			List<int> beforeFirstZero = from i in INT_SOURCE
										where TakeWhile(i > 0)
										group i by ToList into list
										select list;

			beforeFirstZero.GetType().Should().Be<List<int>>();
			beforeFirstZero.Should().ContainInOrder(1, 9, 4, 5);
		}

		[Fact]
		public void CanDefaultIfEmpty() {
			List<int> defaultedEmpty = from i in Array.Empty<int>()
									   where DefaultIfEmpty
									   group i by ToList into list
									   select list;

			defaultedEmpty.GetType().Should().Be<List<int>>();
			defaultedEmpty.Should().ContainInOrder(0);
		}

		[Fact]
		public void CanConcat() {
			int[] second = { 1, 2, 3, 4, 5 };

			List<int> union = from i in INT_SOURCE
							  where Concat(second)
							  group i by ToList into list
							  select list;

			union.GetType().Should().Be<List<int>>();
			union.Should().ContainInOrder(1, 9, 4, 5, 0, 8, 1, 7, 1, 2, 3, 4, 5);
		}

		[Fact]
		public void CanUnion() {
			int[] second = { 1, 2, 3, 4, 5 };

			List<int> union = from i in INT_SOURCE
							  where Union(second)
							  group i by ToList into list
							  select list;

			union.GetType().Should().Be<List<int>>();
			union.Should().ContainInOrder(1, 9, 4, 5, 0, 8, 7, 2, 3);
		}

		[Fact]
		public void CanExcept() {
			int[] second = { 1, 2, 3, 4, 5 };

			List<int> union = from i in INT_SOURCE
							  where Except(second)
							  group i by ToList into list
							  select list;

			union.GetType().Should().Be<List<int>>();
			union.Should().ContainInOrder(9, 0, 8, 7);
		}

		[Fact]
		public void CanIntersect() {
			int[] second = { 1, 2, 3, 4, 5 };

			List<int> union = from i in INT_SOURCE
							  where Intersect(second)
							  group i by ToList into list
							  select list;

			union.GetType().Should().Be<List<int>>();
			union.Should().ContainInOrder(1, 4, 5);
		}

		[Fact]
		public void CanSelectDistinct() {
			HashSet<int> distinctItems = from i in INT_SOURCE
										 orderby Distinct
										 group i by ToHashSet into set
										 select set;

			distinctItems.GetType().Should().Be<HashSet<int>>();
			distinctItems.Should().ContainInOrder(1, 9, 4, 5, 0, 8, 7);
		}

		[Fact]
		public void CanAggregateIntoList() {
			List<int> list = from i in INT_SOURCE
							 group i by ToList into l
							 select l;

			list.GetType().Should().Be<List<int>>();
		}

		[Fact]
		public void CanAggregateIntoImmutableList() {
			ImmutableList<int> list = from i in INT_SOURCE
									  group i by ToImmutableList into l
									  select l;

			list.GetType().Should().Be<ImmutableList<int>>();
		}

		[Fact]
		public void CanAggregateIntoArray() {
			int[] array = from i in INT_SOURCE
						  group i by ToArray into a
						  select a;

			array.GetType().Should().Be<int[]>();
		}

		[Fact]
		public void CanAggregateIntoImmutableArray() {
			ImmutableArray<int> array = from i in INT_SOURCE
										group i by ToImmutableArray into a
										select a;

			array.GetType().Should().Be<ImmutableArray<int>>();
		}

		[Fact]
		public void CanAggregateIntoHashSet() {
			HashSet<int> set = from i in INT_SOURCE
							   orderby Distinct
							   group i by ToHashSet into s
							   select s;

			set.GetType().Should().Be<HashSet<int>>();
		}

		[Fact]
		public void CanAggregateIntoImmutableHashSet() {
			ImmutableHashSet<int> set = from i in INT_SOURCE
										orderby Distinct
										group i by ToImmutableHashSet into s
										select s;

			set.GetType().Should().Be<ImmutableHashSet<int>>();
		}

		[Fact]
		public void CanAggregateIntoDictionary() {
			Dictionary<int, string> stringRepByValue = from i in INT_SOURCE
													   where Distinct
													   group i.ToString() by ToDictionary(i) into dict
													   select dict;

			stringRepByValue.GetType().Should().Be<Dictionary<int, string>>();
			stringRepByValue.Should().Contain(new Dictionary<int, string> {
				{ 1, "1" },
				{ 9, "9" },
				{ 4, "4" },
				{ 5, "5" },
				{ 0, "0" },
				{ 8, "8" },
				{ 7, "7" }
			});

			Dictionary<int, string> stringRepByValue2 = from i in INT_SOURCE
														where Distinct
														group i by ToDictionary(i, i.ToString()) into dict
														select dict;

			stringRepByValue2.GetType().Should().Be<Dictionary<int, string>>();
			stringRepByValue2.Should().Contain(new Dictionary<int, string> {
				{ 1, "1" },
				{ 9, "9" },
				{ 4, "4" },
				{ 5, "5" },
				{ 0, "0" },
				{ 8, "8" },
				{ 7, "7" }
			});
		}

		[Fact]
		public void CanAggregateIntoImmutableDictionary() {
			ImmutableDictionary<int, string> stringRepByValue = from i in INT_SOURCE
																where Distinct
																group i.ToString() by ToImmutableDictionary(i) into dict
																select dict;

			stringRepByValue.GetType().Should().Be<ImmutableDictionary<int, string>>();
			stringRepByValue.Should().Contain(new Dictionary<int, string> {
				{ 1, "1" },
				{ 9, "9" },
				{ 4, "4" },
				{ 5, "5" },
				{ 0, "0" },
				{ 8, "8" },
				{ 7, "7" }
			});

			ImmutableDictionary<int, string> stringRepByValue2 = from i in INT_SOURCE
																 where Distinct
																 group i by ToImmutableDictionary(i, i.ToString()) into dict
																 select dict;

			stringRepByValue2.GetType().Should().Be<ImmutableDictionary<int, string>>();
			stringRepByValue2.Should().Contain(new Dictionary<int, string> {
				{ 1, "1" },
				{ 9, "9" },
				{ 4, "4" },
				{ 5, "5" },
				{ 0, "0" },
				{ 8, "8" },
				{ 7, "7" }
			});
		}

		[Fact]
		public void CanAggregateIntoFirst() {
			int firstItem = from i in INT_SOURCE
							group i by First into g
							select g;

			firstItem.GetType().Should().Be<int>();
			firstItem.Should().Be(1);
		}

		[Fact]
		public void CanAggregateIntoLast() {
			int lastItem = from i in INT_SOURCE
						   group i by Last into g
						   select g;

			lastItem.GetType().Should().Be<int>();
			lastItem.Should().Be(7);
		}

		[Fact]
		public void CanAggregateIntoSingle() {
			int singleItem = from i in INT_SOURCE
							 where Skip(2)
							 where Take(1)
							 group i by Linq.Single into g
							 select g;

			singleItem.GetType().Should().Be<int>();
			singleItem.Should().Be(4);
		}

		[Fact]
		public void CanAggregateIntoElementAt() {
			int fifthElement = from i in INT_SOURCE
							   group i by ElementAt(4) into g
							   select g;

			fifthElement.GetType().Should().Be<int>();
			fifthElement.Should().Be(0);
		}

		[Fact]
		public void CanAggregateIntoAny() {
			bool anyNumberGreaterThan9 = from i in INT_SOURCE
										 group i by Any(i > 9) into a
										 select a;

			anyNumberGreaterThan9.GetType().Should().Be<bool>();
			anyNumberGreaterThan9.Should().Be(false);
		}

		[Fact]
		public void CanAggregateIntoAll() {
			bool allNumbersLessThan10 = from i in INT_SOURCE
										group i by All(i < 10) into a
										select a;

			allNumbersLessThan10.GetType().Should().Be<bool>();
			allNumbersLessThan10.Should().Be(true);
		}

		[Fact]
		public void CanAggregateIntoCount() {
			int count = from i in INT_SOURCE
						group i by Count() into c
						select c;

			count.GetType().Should().Be<int>();
			count.Should().Be(8);
		}

		[Fact]
		public void CanAggregateIntoMax() {
			int max = from i in INT_SOURCE
					  group i by Max into m
					  select m;

			max.GetType().Should().Be<int>();
			max.Should().Be(9);
		}

		[Fact]
		public void CanAggregateIntoMin() {
			int min = from i in INT_SOURCE
					  group i by Min into m
					  select m;

			min.GetType().Should().Be<int>();
			min.Should().Be(0);
		}

		[Fact]
		public void CanAggregateIntoSum() {
			int sum = from i in INT_SOURCE
					  group i by Sum into s
					  select s;

			sum.GetType().Should().Be<int>();
			sum.Should().Be(1 + 9 + 4 + 5 + 0 + 8 + 1 + 7);
		}

		[Fact]
		public void CanAggregateIntoAverage() {
			double avg = from i in INT_SOURCE
						 group i by Average into a
						 select a;

			avg.GetType().Should().Be<double>();
			avg.Should().Be(35.0 / 8.0);
		}

		[Fact]
		public void CanAggregateIntoStringJoin() {
			string joined = from i in INT_SOURCE
							group i by StringJoin() into sj
							select sj;

			joined.GetType().Should().Be<string>();
			joined.Should().Be("1, 9, 4, 5, 0, 8, 1, 7");
		}

		[Fact]
		public void CanAggregateIntoContains() {
			bool contains3 = from i in INT_SOURCE
							 group i by Contains(3) into c
							 select c;

			contains3.GetType().Should().Be<bool>();
			contains3.Should().BeFalse();
		}

		[Fact]
		public void CanAggregateIntoContainsAny() {
			bool contains3or4 = from i in INT_SOURCE
								group i by ContainsAny(3, 4) into c
								select c;

			contains3or4.GetType().Should().Be<bool>();
			contains3or4.Should().BeTrue();
		}

		[Fact]
		public void CanAggregateIntoContainsAll() {
			bool containsAll0to9 = from i in INT_SOURCE
								   group i by ContainsAll(Enumerable.Range(0, 10)) into c
								   select c;

			containsAll0to9.GetType().Should().Be<bool>();
			containsAll0to9.Should().BeFalse();
		}

		[Fact]
		public void CanAggregateIntoSequenceEqual() {
			bool equals = from i in INT_SOURCE
						  group i by SequenceEqual(INT_SOURCE) into eq
						  select eq;

			equals.GetType().Should().Be<bool>();
			equals.Should().BeTrue();
		}

		[Fact]
		public void CanFilterUsingMethodGroup() {
		}

		[Fact]
		public void CanAggregateUsingMethodGroup() {
			List<int> list = from i in INT_SOURCE
							 group i by Enumerable.ToList into l
							 select l;

			list.GetType().Should().Be<List<int>>();
		}
	}
}
