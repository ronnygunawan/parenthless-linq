using System.Collections.Generic;

namespace Parenthless.Clauses {
	public class ToArrayEnumerable<T> {
		public IEnumerable<T> Enumerable { get; }

		public ToArrayEnumerable(IEnumerable<T> enumerable) {
			Enumerable = enumerable;
		}
	}
}
