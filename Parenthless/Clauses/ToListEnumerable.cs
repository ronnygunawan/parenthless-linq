using System.Collections.Generic;

namespace Parenthless.Clauses {
	public class ToListEnumerable<T> {
		public IEnumerable<T> Enumerable { get; }

		public ToListEnumerable(IEnumerable<T> enumerable) {
			Enumerable = enumerable;
		}
	}
}
