namespace Parenthless.Clauses {
	public class ToDictionaryClause<TKey> {
		public TKey Key { get; }

		public ToDictionaryClause(TKey key) {
			Key = key;
		}
	}

	public class ToDictionaryClause<TKey, TElement> {
		public TKey Key { get; }
		public TElement Element { get; }

		public ToDictionaryClause(TKey key, TElement element) {
			Key = key;
			Element = element;
		}
	}
}
