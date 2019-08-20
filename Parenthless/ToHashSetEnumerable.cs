using System.Collections.Generic;

namespace Parenthless {
	public class ToHashSetEnumerable<T> {
		public IEnumerable<T> Enumerable { get; }

		public ToHashSetEnumerable(IEnumerable<T> enumerable) {
			Enumerable = enumerable;
		}
	}
}
