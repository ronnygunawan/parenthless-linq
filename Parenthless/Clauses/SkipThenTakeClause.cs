namespace Parenthless.Clauses {
	public class SkipThenTakeClause {
		public int SkipCount { get; }

		public int TakeCount { get; }

		public SkipThenTakeClause(int skipCount, int takeCount) {
			SkipCount = skipCount;
			TakeCount = takeCount;
		}
	}
}
