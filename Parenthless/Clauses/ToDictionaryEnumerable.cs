using System;
using System.Collections.Generic;

namespace Parenthless.Clauses {
	public class ToDictionaryEnumerable<T, TKey> {
		public IEnumerable<T> Enumerable { get; }
		public Func<T, ToDictionaryClause<TKey>> ToDictionaryClauseSelector { get; }

		public ToDictionaryEnumerable(IEnumerable<T> enumerable, Func<T, ToDictionaryClause<TKey>> toDictionaryClauseSelector) {
			Enumerable = enumerable;
			ToDictionaryClauseSelector = toDictionaryClauseSelector;
		}
	}

	public class ToDictionaryEnumerable<T, TKey, TElement> {
		public IEnumerable<T> Enumerable { get; }
		public Func<T, ToDictionaryClause<TKey, TElement>> ToDictionaryClauseSelector { get; }

		public ToDictionaryEnumerable(IEnumerable<T> enumerable, Func<T, ToDictionaryClause<TKey, TElement>> toDictionaryClauseSelector) {
			Enumerable = enumerable;
			ToDictionaryClauseSelector = toDictionaryClauseSelector;
		}
	}
}
