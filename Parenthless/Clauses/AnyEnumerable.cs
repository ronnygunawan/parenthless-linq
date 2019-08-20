using System.Collections.Generic;

namespace Parenthless.Clauses {
	public class AnyEnumerable<T> {
		public IEnumerable<T> Enumerable { get; }

		public AnyEnumerable(IEnumerable<T> enumerable) {
			Enumerable = enumerable;
		}
	}
}
