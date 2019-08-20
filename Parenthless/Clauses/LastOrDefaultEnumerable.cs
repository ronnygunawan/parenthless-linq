using System.Collections.Generic;

namespace Parenthless.Clauses {
	public class LastOrDefaultEnumerable<T> {
		public IEnumerable<T> Enumerable { get; }

		public LastOrDefaultEnumerable(IEnumerable<T> enumerable) {
			Enumerable = enumerable;
		}
	}
}
