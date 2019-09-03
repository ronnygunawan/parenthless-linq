using System.Linq;
using System.Threading;

namespace Parenthless.EntityFrameworkCore.Clauses {
	public class ToListAsyncQueryable<T> {
		public IQueryable<T> Queryable { get; }
		public CancellationToken CancellationToken { get; }

		public ToListAsyncQueryable(IQueryable<T> queryable, CancellationToken cancellationToken) {
			Queryable = queryable;
			CancellationToken = cancellationToken;
		}
	}
}
