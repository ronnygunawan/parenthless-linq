using System.Collections.Generic;

namespace Parenthless.Clauses {
	public class ContainsEnumerable<T> {
		public IEnumerable<T> Enumerable { get; }
		public T Value { get; }
		public IEqualityComparer<T> Comparer { get; }

		public ContainsEnumerable(IEnumerable<T> enumerable, T value, IEqualityComparer<T> comparer) {
			Enumerable = enumerable;
			Value = value;
			Comparer = comparer;
		}
	}
}
