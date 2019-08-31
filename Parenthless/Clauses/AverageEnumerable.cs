using System.Collections.Generic;

namespace Parenthless.Clauses {
	public class AverageEnumerable<T> {
		public IEnumerable<T> Enumerable { get; }

		public AverageEnumerable(IEnumerable<T> enumerable) {
			Enumerable = enumerable;
		}
	}
}
