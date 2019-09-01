namespace Parenthless.Clauses {
	public class SkipLastThenTakeLastClause {
		public int SkipLastCount { get; }

		public int TakeLastCount { get; }

		public SkipLastThenTakeLastClause(int skipLastCount, int takeLastCount) {
			SkipLastCount = skipLastCount;
			TakeLastCount = takeLastCount;
		}
	}
}
