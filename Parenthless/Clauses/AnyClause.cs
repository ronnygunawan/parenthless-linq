﻿namespace Parenthless.Clauses {
	public class AnyClause {
		public bool Condition { get; }

		public AnyClause(bool condition) {
			Condition = condition;
		}
	}
}
