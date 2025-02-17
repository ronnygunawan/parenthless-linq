﻿namespace Parenthless.Clauses {
	public class SkipClause {
		public int Count { get; }

		public SkipClause(int count) {
			Count = count;
		}

		public SkipThenTakeClause Take(int count) => new(Count, count);
	}
}
