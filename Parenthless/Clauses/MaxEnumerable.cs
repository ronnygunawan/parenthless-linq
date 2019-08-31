using System;
using System.Collections.Generic;

namespace Parenthless.Clauses {
	public class MaxEnumerable<T> where T : IComparable<T> {
		public IEnumerable<T> Enumerable { get; }

		public MaxEnumerable(IEnumerable<T> enumerable) {
			Enumerable = enumerable;
		}
	}
}
