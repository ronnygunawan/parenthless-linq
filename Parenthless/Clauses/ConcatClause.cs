using System.Collections.Generic;

namespace Parenthless.Clauses {
	public class ConcatClause<TSource> {
		public IEnumerable<TSource> Second { get; }

		public ConcatClause(IEnumerable<TSource> second) {
			Second = second;
		}
	}
}
