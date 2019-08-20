using System;

namespace Parenthless {
	public static class Linq {
		public static SkipClause skip(int count) => new SkipClause(count);
		public static TakeClause take(int count) => new TakeClause(count);
		public static DistinctClause distinct { get; } = new DistinctClause();
		public static ToListClause list { get; } = new ToListClause();
		public static ToHashSetClause hashset { get; } = new ToHashSetClause();
	}
}
