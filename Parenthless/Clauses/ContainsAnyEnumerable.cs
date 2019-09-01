using System.Collections.Generic;

namespace Parenthless.Clauses {
	public class ContainsAnyEnumerable<T> {
		public IEnumerable<T> Enumerable { get; }
		public IEnumerable<T> Values { get; }
		public IEqualityComparer<T> Comparer { get; }

		public ContainsAnyEnumerable(IEnumerable<T> enumerable, IEnumerable<T> values, IEqualityComparer<T> comparer) {
			Enumerable = enumerable;
			Values = values;
			Comparer = comparer;
		}
	}
}
