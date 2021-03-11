using System.Collections.Generic;

namespace Parenthless.Clauses {
	public class DistinctClause {
		public DistinctClause<TSource> Using<TSource>(IEqualityComparer<TSource> comparer) => new(comparer);
	}

	public class DistinctClause<TSource> {
		public IEqualityComparer<TSource> Comparer { get; }

		public DistinctClause(IEqualityComparer<TSource> comparer) {
			Comparer = comparer;
		}
	}
}
