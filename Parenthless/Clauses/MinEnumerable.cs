using System;
using System.Collections.Generic;

namespace Parenthless.Clauses {
	public class MinEnumerable<T> where T : IComparable<T> {
		public IEnumerable<T> Enumerable { get; }

		public MinEnumerable(IEnumerable<T> enumerable) {
			Enumerable = enumerable;
		}
	}
}
