using System.Collections.Generic;

namespace Parenthless.Clauses {
	public class ContainsAnyClause<TSource> {
		public IEnumerable<TSource> Values { get; }
		public IEqualityComparer<TSource> Comparer { get; }

		public ContainsAnyClause(IEnumerable<TSource> values) {
			Values = values;
			Comparer = EqualityComparer<TSource>.Default;
		}

		public ContainsAnyClause(IEnumerable<TSource> values, IEqualityComparer<TSource> comparer) {
			Values = values;
			Comparer = comparer;
		}

		public ContainsAnyClause<TSource> Using(IEqualityComparer<TSource> comparer) => new ContainsAnyClause<TSource>(Values, comparer);
	}
}
