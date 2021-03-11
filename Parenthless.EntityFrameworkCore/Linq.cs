using Parenthless.EntityFrameworkCore.Clauses;
using System.Threading;

namespace Parenthless.EntityFrameworkCore {
	public static class Linq {

		// 1.0.3
		public static ToListAsyncClause ToListAsync() => new ToListAsyncClause(CancellationToken.None);
		// 1.0.3
		public static ToListAsyncClause ToListAsync(CancellationToken cancellationToken) => new ToListAsyncClause(cancellationToken);
		// 1.0.3
		public static ToArrayAsyncClause ToArrayAsync() => new ToArrayAsyncClause(CancellationToken.None);
		// 1.0.3
		public static ToArrayAsyncClause ToArrayAsync(CancellationToken cancellationToken) => new ToArrayAsyncClause(cancellationToken);
		// 1.0.3
		public static (ToDictionaryAsyncClause, TKey) ToDictionaryAsync<TKey>(TKey key) => (new ToDictionaryAsyncClause(CancellationToken.None), key);
		// 1.0.3
		public static (ToDictionaryAsyncClause, TKey) ToDictionaryAsync<TKey>(TKey key, CancellationToken cancellationToken) => (new ToDictionaryAsyncClause(cancellationToken), key);
	}
}
