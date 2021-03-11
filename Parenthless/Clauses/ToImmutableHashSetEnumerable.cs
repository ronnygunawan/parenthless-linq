using System.Collections.Generic;

namespace Parenthless.Clauses {
	public class ToImmutableHashSetEnumerable<T> {
		public IEnumerable<T> Enumerable { get; }

		public ToImmutableHashSetEnumerable(IEnumerable<T> enumerable) {
			Enumerable = enumerable;
		}
	}
}
