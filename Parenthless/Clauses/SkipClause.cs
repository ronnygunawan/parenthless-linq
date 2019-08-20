using System.Diagnostics.CodeAnalysis;

namespace Parenthless.Clauses {
	public class SkipClause {
		public int Count { get; }

		public SkipClause(int count) {
			Count = count;
		}

		[SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Pseudo keyword should be lowercase.")]
		public SkipThenTakeClause take(int count) => new SkipThenTakeClause(Count, count);
	}
}
