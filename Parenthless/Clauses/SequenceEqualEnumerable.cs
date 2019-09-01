using System.Collections.Generic;

namespace Parenthless.Clauses {
	public class SequenceEqualEnumerable<T> {
		public IEnumerable<T> Enumerable { get; }
		public IEnumerable<T> Second { get; }
		public IEqualityComparer<T> Comparer { get; }

		public SequenceEqualEnumerable(IEnumerable<T> enumerable, IEnumerable<T> second, IEqualityComparer<T> comparer) {
			Enumerable = enumerable;
			Second = second;
			Comparer = comparer;
		}
	}
}
