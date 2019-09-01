using System.Collections.Generic;

namespace Parenthless.Clauses {
	public class StringJoinEnumerable<T> {
		public IEnumerable<T> Enumerable { get; }
		public string Separator { get; }

		public StringJoinEnumerable(IEnumerable<T> enumerable, string separator) {
			Enumerable = enumerable;
			Separator = separator;
		}
	}
}
