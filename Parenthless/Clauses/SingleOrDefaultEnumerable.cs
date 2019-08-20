using System.Collections.Generic;

namespace Parenthless.Clauses {
	public class SingleOrDefaultEnumerable<T> {
		public IEnumerable<T> Enumerable { get; }

		public SingleOrDefaultEnumerable(IEnumerable<T> enumerable) {
			Enumerable = enumerable;
		}
	}
}
