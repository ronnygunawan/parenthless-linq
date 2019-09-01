using System.Collections.Generic;

namespace Parenthless.Clauses {
	public class UnionClause<TSource> {
		public IEnumerable<TSource> Second { get; }
		public IEqualityComparer<TSource> Comparer { get; }

		public UnionClause(IEnumerable<TSource> second) {
			Second = second;
			Comparer = EqualityComparer<TSource>.Default;
		}

		public UnionClause(IEnumerable<TSource> second, IEqualityComparer<TSource> comparer) {
			Second = second;
			Comparer = comparer;
		}
	}
}
