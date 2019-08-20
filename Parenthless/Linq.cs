using Parenthless.Clauses;

namespace Parenthless {
	public static class Linq {
		// where clauses
		public static SkipClause Skip(int count) => new SkipClause(count);
		public static TakeClause Take(int count) => new TakeClause(count);

		// orderby clauses
		public static DistinctClause Distinct { get; } = new DistinctClause();
		public static ReverseClause Reverse { get; } = new ReverseClause();

		// groupby clauses
		public static ToListClause ToList { get; } = new ToListClause();
		public static ToHashSetClause ToHashSet { get; } = new ToHashSetClause();
		public static FirstClause First { get; } = new FirstClause();
		public static FirstOrDefaultClause FirstOrDefault { get; } = new FirstOrDefaultClause();
		public static LastClause Last { get; } = new LastClause();
		public static LastOrDefaultClause LastOrDefault { get; } = new LastOrDefaultClause();
		public static SingleClause Single { get; } = new SingleClause();
		public static SingleOrDefaultClause SingleOrDefault { get; } = new SingleOrDefaultClause();
		public static AnyClause Any { get; } = new AnyClause();
	}
}
