using System;
using System.Collections.Generic;

namespace Parenthless.Clauses {
	public class ToImmutableDictionaryEnumerable<T, TKey> {
		public IEnumerable<T> Enumerable { get; }
		public Func<T, ToImmutableDictionaryClause<TKey>> ToImmutableDictionaryClauseSelector { get; }

		public ToImmutableDictionaryEnumerable(IEnumerable<T> enumerable, Func<T, ToImmutableDictionaryClause<TKey>> toImmutableDictionaryClauseSelector) {
			Enumerable = enumerable;
			ToImmutableDictionaryClauseSelector = toImmutableDictionaryClauseSelector;
		}
	}

	public class ToImmutableDictionaryEnumerable<T, TKey, TElement> {
		public IEnumerable<T> Enumerable { get; }
		public Func<T, ToImmutableDictionaryClause<TKey, TElement>> ToImmutableDictionaryClauseSelector { get; }

		public ToImmutableDictionaryEnumerable(IEnumerable<T> enumerable, Func<T, ToImmutableDictionaryClause<TKey, TElement>> toImmutableDictionaryClauseSelector) {
			Enumerable = enumerable;
			ToImmutableDictionaryClauseSelector = toImmutableDictionaryClauseSelector;
		}
	}
}
