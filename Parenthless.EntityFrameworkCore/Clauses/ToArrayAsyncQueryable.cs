using System.Linq;
using System.Threading;

namespace Parenthless.EntityFrameworkCore.Clauses {
	public class ToArrayAsyncQueryable<T> {
		public IQueryable<T> Queryable { get; }
		public CancellationToken CancellationToken { get; }

		public ToArrayAsyncQueryable(IQueryable<T> queryable, CancellationToken cancellationToken) {
			Queryable = queryable;
			CancellationToken = cancellationToken;
		}
	}
}
