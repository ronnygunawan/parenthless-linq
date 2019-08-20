using System.Collections.Generic;

namespace Parenthless.Clauses {
	public class LastEnumerable<T> {
		public IEnumerable<T> Enumerable { get; }

		public LastEnumerable(IEnumerable<T> enumerable) {
			Enumerable = enumerable;
		}
	}
}
