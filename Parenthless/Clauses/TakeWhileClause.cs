namespace Parenthless.Clauses {
	public class TakeWhileClause {
		public bool Condition { get; }

		public TakeWhileClause(bool condition) {
			Condition = condition;
		}
	}
}
