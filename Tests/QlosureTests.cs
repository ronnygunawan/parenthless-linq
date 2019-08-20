using System.Collections.Generic;
using System.Linq;
using Parenthless;
using static Parenthless.Linq;
using FluentAssertions;
using Xunit;

namespace Tests {
	public class QlosureTests {
		[Fact]
		public void ComboWomboWorks() {
			int[] items = { 1, 9, 4, 5, 0, 8, 1, 7 };

			int sum = from q in One.Value(items)

					  let firstThreeAsc = from i in items
										  where Take(3)
										  orderby i ascending
										  select i
					  let test1 = firstThreeAsc.GetType().Should().Implement<IOrderedEnumerable<int>>()
					  let test2 = firstThreeAsc.Should().BeEquivalentTo(1, 4, 9)

					  let lastThreeDesc = from i in items
										  orderby Reverse
										  where Take(3)
										  orderby i descending
										  select i
					  let test3 = lastThreeDesc.GetType().Should().Implement<IOrderedEnumerable<int>>()
					  let test4 = lastThreeDesc.Should().BeEquivalentTo(8, 7, 1)

					  let floorByTwo = from i in items
									   let even = i % 2 == 0
									   let floored = even switch
									   {
										   true => i,
										   false => i - 1
									   }
									   group floored by ToList into g
									   select g
					  let test5 = floorByTwo.GetType().Should().Be<List<int>>()
					  let test6 = floorByTwo.Should().BeEquivalentTo(0, 8, 4, 4, 0, 8, 0, 6)

					  let uniqueFlooredByTwo = from i in floorByTwo
											   orderby Distinct
											   select i
					  let test7 = uniqueFlooredByTwo.GetType().Should().Implement<IEnumerable<int>>()
					  let test8 = uniqueFlooredByTwo.Should().BeEquivalentTo(0, 8, 4, 6)

					  let union = from i in firstThreeAsc.Union(lastThreeDesc).Union(uniqueFlooredByTwo)
								  orderby Distinct
								  group i by ToHashSet into g
								  select g
					  let test9 = union.GetType().Should().Be<HashSet<int>>()
					  let test10 = union.Should().BeEquivalentTo(1, 4, 9, 8, 7, 0, 6)

					  select union.Sum();

			sum.Should().Be(1 + 4 + 9 + 8 + 7 + 0 + 6);
		}
	}
}
