namespace Parenthless.Clauses {
	public class SkipLastClause {
		public int Count { get; }
		
		public SkipLastClause(int count) {
			Count = count;
		}

		public SkipLastThenTakeLastClause TakeLast(int count) => new SkipLastThenTakeLastClause(Count, count);
	}
}
