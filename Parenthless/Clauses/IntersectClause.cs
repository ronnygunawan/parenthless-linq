using System.Collections.Generic;

namespace Parenthless.Clauses {
	public class IntersectClause<TSource> {
		public IEnumerable<TSource> Second { get; }
		public IEqualityComparer<TSource> Comparer { get; }

		public IntersectClause(IEnumerable<TSource> second) {
			Second = second;
			Comparer = EqualityComparer<TSource>.Default;
		}

		public IntersectClause(IEnumerable<TSource> second, IEqualityComparer<TSource> comparer) {
			Second = second;
			Comparer = comparer;
		}
	}
}
