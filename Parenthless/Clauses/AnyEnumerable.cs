using System;
using System.Collections.Generic;

namespace Parenthless.Clauses {
	public class AnyEnumerable<T> {
		public IEnumerable<T> Enumerable { get; }
		public Func<T, AnyClause> AnyClauseSelector { get; }

		public AnyEnumerable(IEnumerable<T> enumerable, Func<T, AnyClause> anyClauseSelector) {
			Enumerable = enumerable;
			AnyClauseSelector = anyClauseSelector;
		}
	}
}
