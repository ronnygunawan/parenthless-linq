using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

namespace Parenthless.EntityFrameworkCore.Clauses {
	public class ToDictionaryAsyncQueryable<T, TKey> {
		public IQueryable<T> Queryable { get; }
		public Func<T, TKey> KeySelector { get; }
		public CancellationToken CancellationToken { get; }

		public ToDictionaryAsyncQueryable(IQueryable<T> queryable, Func<T, TKey> keySelector, CancellationToken cancellationToken) {
			Queryable = queryable;
			KeySelector = keySelector;
			CancellationToken = cancellationToken;
		}
	}

	public class ToDictionaryAsyncQueryable<T, TKey, TElement> {
		public IQueryable<T> Queryable { get; }
		public Func<T, TKey> KeySelector { get; }
		public Func<T, TElement> ElementSelector { get; }
		public CancellationToken CancellationToken { get; }

		public ToDictionaryAsyncQueryable(IQueryable<T> queryable, Func<T, TKey> keySelector, Func<T, TElement> elementSelector, CancellationToken cancellationToken) {
			Queryable = queryable;
			KeySelector = keySelector;
			ElementSelector = elementSelector;
			CancellationToken = cancellationToken;
		}
	}
}
