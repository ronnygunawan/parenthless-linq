using System.Collections.Generic;

namespace Parenthless.Clauses {
	public class ContainsClause<TSource> {
		public TSource Value { get; }
		public IEqualityComparer<TSource> Comparer { get; }

		public ContainsClause(TSource value) {
			Value = value;
			Comparer = EqualityComparer<TSource>.Default;
		}

		public ContainsClause(TSource value, IEqualityComparer<TSource> comparer) {
			Value = value;
			Comparer = comparer;
		}
	}
}
