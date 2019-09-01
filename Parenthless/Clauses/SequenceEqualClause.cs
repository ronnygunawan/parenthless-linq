using System.Collections.Generic;

namespace Parenthless.Clauses {
	public class SequenceEqualClause<TSource> {
		public IEnumerable<TSource> Second { get; }
		public IEqualityComparer<TSource> Comparer { get; }

		public SequenceEqualClause(IEnumerable<TSource> second) {
			Second = second;
			Comparer = EqualityComparer<TSource>.Default;
		}

		public SequenceEqualClause(IEnumerable<TSource> second, IEqualityComparer<TSource> comparer) {
			Second = second;
			Comparer = comparer;
		}
	}
}
