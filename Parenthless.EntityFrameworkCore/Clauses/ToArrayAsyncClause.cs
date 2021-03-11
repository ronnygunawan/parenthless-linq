using System.Threading;

namespace Parenthless.EntityFrameworkCore.Clauses {
	public class ToArrayAsyncClause {
		public CancellationToken CancellationToken { get; }

		public ToArrayAsyncClause(CancellationToken cancellationToken) {
			CancellationToken = cancellationToken;
		}
	}
}
