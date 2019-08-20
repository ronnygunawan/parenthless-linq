using System.Collections.Generic;

namespace Parenthless.Clauses {
	public class FirstOrDefaultEnumerable<T> {
		public IEnumerable<T> Enumerable { get; }

		public FirstOrDefaultEnumerable(IEnumerable<T> enumerable) {
			Enumerable = enumerable;
		}
	}
}
