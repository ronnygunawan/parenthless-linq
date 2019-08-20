namespace Parenthless {
	public class SkipClause {
		public int Count { get; }

		public SkipClause(int count) {
			Count = count;
		}

		public SkipThenTakeClause take(int count) => new SkipThenTakeClause(Count, count);
	}
}
