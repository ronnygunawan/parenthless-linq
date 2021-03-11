using System.Collections.Generic;

namespace Parenthless.Clauses {
	public class ContainsAllClause<TSource> {
		public IEnumerable<TSource> Values { get; }
		public IEqualityComparer<TSource> Comparer { get; }

		public ContainsAllClause(IEnumerable<TSource> values) {
			Values = values;
			Comparer = EqualityComparer<TSource>.Default;
		}

		public ContainsAllClause(IEnumerable<TSource> values, IEqualityComparer<TSource> comparer) {
			Values = values;
			Comparer = comparer;
		}

		public ContainsAllClause<TSource> Using(IEqualityComparer<TSource> comparer) => new(Values, comparer);
	}
}
