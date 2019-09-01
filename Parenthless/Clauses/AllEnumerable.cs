using System;
using System.Collections.Generic;

namespace Parenthless.Clauses {
	public class AllEnumerable<T> {
		public IEnumerable<T> Enumerable { get; }
		public Func<T, AllClause> AllClauseSelector { get; }

		public AllEnumerable(IEnumerable<T> enumerable, Func<T, AllClause> allClauseSelector) {
			Enumerable = enumerable;
			AllClauseSelector = allClauseSelector;
		}
	}
}
