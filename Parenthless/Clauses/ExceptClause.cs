using System.Collections.Generic;

namespace Parenthless.Clauses {
	public class ExceptClause<TSource> {
		public IEnumerable<TSource> Second { get; }
		public IEqualityComparer<TSource> Comparer { get; }

		public ExceptClause(IEnumerable<TSource> second) {
			Second = second;
			Comparer = EqualityComparer<TSource>.Default;
		}

		public ExceptClause(IEnumerable<TSource> second, IEqualityComparer<TSource> comparer) {
			Second = second;
			Comparer = comparer;
		}
	}
}
