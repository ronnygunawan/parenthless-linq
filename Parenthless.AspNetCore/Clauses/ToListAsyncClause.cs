using System.Threading;

namespace Parenthless.EntityFrameworkCore.Clauses {
	public class ToListAsyncClause {
		public CancellationToken CancellationToken { get; }

		public ToListAsyncClause(CancellationToken cancellationToken) {
			CancellationToken = cancellationToken;
		}
	}
}
