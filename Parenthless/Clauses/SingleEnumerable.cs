using System.Collections.Generic;

namespace Parenthless.Clauses {
	public class SingleEnumerable<T> {
		public IEnumerable<T> Enumerable { get; }

		public SingleEnumerable(IEnumerable<T> enumerable) {
			Enumerable = enumerable;
		}
	}
}
