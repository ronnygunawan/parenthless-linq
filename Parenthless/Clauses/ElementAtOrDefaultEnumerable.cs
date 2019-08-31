using System.Collections.Generic;

namespace Parenthless.Clauses {
	public class ElementAtOrDefaultEnumerable<T> {
		public IEnumerable<T> Enumerable { get; }
		public int Index { get; }

		public ElementAtOrDefaultEnumerable(IEnumerable<T> enumerable, int index) {
			Enumerable = enumerable;
			Index = index;
		}
	}
}
