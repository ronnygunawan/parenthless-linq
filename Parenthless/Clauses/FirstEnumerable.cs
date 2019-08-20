using System.Collections.Generic;

namespace Parenthless.Clauses {
	public class FirstEnumerable<T> {
		public IEnumerable<T> Enumerable { get; }

		public FirstEnumerable(IEnumerable<T> enumerable) {
			Enumerable = enumerable;
		}
	}
}
