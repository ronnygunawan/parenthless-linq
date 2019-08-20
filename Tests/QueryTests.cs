using System.Linq;
using Xunit;
using Parenthless;
using static Parenthless.Linq;
using FluentAssertions;
using System.Collections.Generic;

namespace Tests {
	public class QueryTests {
		[Fact]
		public void CanSkipAndTake() {
			int[] items = { 1, 9, 4, 5, 0, 8, 1, 7 };

			var middleTwoItems = from i in items
								 where Skip(3)
								 where Take(2)
								 group i by ToList into g
								 select g;

			middleTwoItems.GetType().Should().Be<List<int>>();
			middleTwoItems.Count.Should().Be(2);
			middleTwoItems[0].Should().Be(5);
			middleTwoItems[1].Should().Be(0);
		}

		[Fact]
		public void CanSelectDistinct() {
			int[] items = { 1, 9, 4, 5, 0, 8, 1, 7 };

			var distinctItems = from i in items
								orderby i ascending
								orderby Distinct
								group i by ToHashSet into g
								select g;

			distinctItems.GetType().Should().Be<HashSet<int>>();
			distinctItems.Count().Should().Be(7);
			distinctItems.Should().BeInAscendingOrder();
		}

		[Fact]
		public void CanAggregateUsingFirst() {
			int[] items = { 1, 9, 4, 5, 0, 8, 1, 7 };

			var firstItem = from i in items
							group i by First into g
							select g;

			firstItem.GetType().Should().Be<int>();
			firstItem.Should().Be(1);
		}

		[Fact]
		public void CanAggregateUsingLast() {
			int[] items = { 1, 9, 4, 5, 0, 8, 1, 7 };

			var lastItem = from i in items
							group i by Last into g
							select g;

			lastItem.GetType().Should().Be<int>();
			lastItem.Should().Be(7);
		}

		[Fact]
		public void CanAggregateUsingSingle() {
			int[] items = { 1, 9, 4, 5, 0, 8, 1, 7 };

			var singleItem = from i in items
							 where Skip(2)
							 where Take(1)
							 group i by Single into g
							 select g;

			singleItem.GetType().Should().Be<int>();
			singleItem.Should().Be(4);
		}

		[Fact]
		public void CanAggregateUsingFirstOrDefault() {
			int[] items = { 1, 9, 4, 5, 0, 8, 1, 7 };

			var firstItem = from i in items
							group i by FirstOrDefault into g
							select g;

			firstItem.GetType().Should().Be<int>();
			firstItem.Should().Be(1);
		}

		[Fact]
		public void CanAggregateUsingLastOrDefault() {
			int[] items = { 1, 9, 4, 5, 0, 8, 1, 7 };

			var lastItem = from i in items
						   group i by LastOrDefault into g
						   select g;

			lastItem.GetType().Should().Be<int>();
			lastItem.Should().Be(7);
		}

		[Fact]
		public void CanAggregateUsingSingleOrDefault() {
			int[] items = { 1, 9, 4, 5, 0, 8, 1, 7 };

			var singleItem = from i in items
							 where Skip(2)
							 where Take(1)
							 group i by SingleOrDefault into g
							 select g;

			singleItem.GetType().Should().Be<int>();
			singleItem.Should().Be(4);
		}

		[Fact]
		public void CanAggregateUsingAny() {
			int[] items = { 1, 9, 4, 5, 0, 8, 1, 7 };

			var anyItem = from i in items
						  group i by Any into g
						  select g;

			anyItem.GetType().Should().Be<bool>();
			anyItem.Should().Be(true);
		}
	}
}
