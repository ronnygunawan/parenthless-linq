using System.Collections.Generic;

namespace Parenthless.Clauses {
	public class ContainsAllEnumerable<T> {
		public IEnumerable<T> Enumerable { get; }
		public IEnumerable<T> Values { get; }
		public IEqualityComparer<T> Comparer { get; }

		public ContainsAllEnumerable(IEnumerable<T> enumerable, IEnumerable<T> values, IEqualityComparer<T> comparer) {
			Enumerable = enumerable;
			Values = values;
			Comparer = comparer;
		}
	}
}
