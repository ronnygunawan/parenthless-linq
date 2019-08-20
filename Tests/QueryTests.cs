using System;
using System.Linq;
using Xunit;
using Parenthless;
using static Parenthless.Linq;
using FluentAssertions;
using System.Collections.Generic;

namespace Tests {
	public class QueryTests {
		[Fact]
		public void CanSkipAndTakeWithoutParentheses() {
			int[] items = { 1, 9, 4, 5, 0, 8, 1, 7 };

			var middleTwoItems = from i in items
								 where skip(3)
								 where take(2)
								 group i by list into g
								 select g;

			middleTwoItems.GetType().Should().Be<List<int>>();
			middleTwoItems.Count.Should().Be(2);
			middleTwoItems[0].Should().Be(5);
			middleTwoItems[1].Should().Be(0);
		}

		[Fact]
		public void CanSelectDistinctWithoutParentheses() {
			int[] items = { 1, 9, 4, 5, 0, 8, 1, 7 };

			var distinctItems = from i in items
								orderby i ascending
								orderby distinct
								group i by hashset into g
								select g;

			distinctItems.GetType().Should().Be<HashSet<int>>();
			distinctItems.Count().Should().Be(7);
			distinctItems.Should().BeInAscendingOrder();
		}
	}
}
