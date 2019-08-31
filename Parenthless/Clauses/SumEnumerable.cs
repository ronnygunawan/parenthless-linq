using System;
using System.Collections.Generic;

namespace Parenthless.Clauses {
	public class SumEnumerable<T> {
		public IEnumerable<T> Enumerable { get; }

		public SumEnumerable(IEnumerable<T> enumerable) {
			Enumerable = enumerable;
		}
	}
}
