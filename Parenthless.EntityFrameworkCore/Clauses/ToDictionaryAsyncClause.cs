using System.Threading;

namespace Parenthless.EntityFrameworkCore.Clauses {
	public class ToDictionaryAsyncClause {
		public CancellationToken CancellationToken { get; }

		public ToDictionaryAsyncClause(CancellationToken cancellationToken) {
			CancellationToken = cancellationToken;
		}
	}
}
