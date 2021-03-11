#if NETCOREAPP3_1 || NET5_0
using System.Collections.Generic;

namespace Parenthless.Clauses {
	public class ToImmutableArrayEnumerable<T> {
		public IEnumerable<T> Enumerable { get; }

		public ToImmutableArrayEnumerable(IEnumerable<T> enumerable) {
			Enumerable = enumerable;
		}
	}
}
#endif
