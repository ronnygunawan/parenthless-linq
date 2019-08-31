using System;
using System.Collections.Generic;

namespace Parenthless.Clauses {
	public class CountEnumerable<T> {
		public IEnumerable<T> Enumerable { get; }
		public Func<T, CountClause> CountClauseSelector { get; }

		public CountEnumerable(IEnumerable<T> enumerable, Func<T, CountClause> countClauseSelector) {
			Enumerable = enumerable;
			CountClauseSelector = countClauseSelector;
		}
	}
}
