#if NETCOREAPP3_1 || NET5_0
using System.Collections.Generic;

namespace Parenthless.Clauses {
	public class ToImmutableListEnumerable<T> {
		public IEnumerable<T> Enumerable { get; }

		public ToImmutableListEnumerable(IEnumerable<T> enumerable) {
			Enumerable = enumerable;
		}
	}
}
#endif
