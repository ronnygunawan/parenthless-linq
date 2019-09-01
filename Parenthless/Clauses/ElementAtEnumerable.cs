using System.Collections.Generic;

namespace Parenthless.Clauses {
	public class ElementAtEnumerable<T> {
		public IEnumerable<T> Enumerable { get; }
		public int Index { get; }

		public ElementAtEnumerable(IEnumerable<T> enumerable, int index) {
			Enumerable = enumerable;
			Index = index;
		}
	}
}
