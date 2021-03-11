namespace Parenthless.Clauses {
	public class ToImmutableDictionaryClause<TKey> {
		public TKey Key { get; }

		public ToImmutableDictionaryClause(TKey key) {
			Key = key;
		}
	}

	public class ToImmutableDictionaryClause<TKey, TElement> {
		public TKey Key { get; }
		public TElement Element { get; }

		public ToImmutableDictionaryClause(TKey key, TElement element) {
			Key = key;
			Element = element;
		}
	}
}
