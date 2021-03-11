using Parenthless.EntityFrameworkCore.Clauses;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Reflection;

namespace Parenthless.EntityFrameworkCore {
	public static class LinqExtensions {

		// group x by ToListAsync into g
		public static ToListAsyncQueryable<TSource> GroupBy<TSource>(this IQueryable<TSource> source, Expression<Func<TSource?, ToListAsyncClause>> toListAsyncClauseSelector) where TSource : class {
			ToListAsyncClause toListAsyncClause = toListAsyncClauseSelector.Compile().Invoke(default);
			return new ToListAsyncQueryable<TSource>(source, toListAsyncClause.CancellationToken);
		}
		public static ToListAsyncQueryable<TElement> GroupBy<TSource, TElement>(this IQueryable<TSource> source, Expression<Func<TSource?, ToListAsyncClause>> toListAsyncClauseSelector, Expression<Func<TSource, TElement>> elementSelector) where TSource : class {
			ToListAsyncClause toListAsyncClause = toListAsyncClauseSelector.Compile().Invoke(default);
			return new ToListAsyncQueryable<TElement>(source.Select(elementSelector), toListAsyncClause.CancellationToken);
		}
		public static Task<List<TSource>> Select<TSource, TResult>(this ToListAsyncQueryable<TSource> source, Expression<Func<TSource, TResult>> selector) {
			return source.Queryable.ToListAsync(source.CancellationToken);
		}

		// group x by ToArrayAsync into g
		public static ToArrayAsyncQueryable<TSource> GroupBy<TSource>(this IQueryable<TSource> source, Expression<Func<TSource?, ToArrayAsyncClause>> toArrayAsyncClauseSelector) where TSource : class {
			ToArrayAsyncClause toArrayAsyncClause = toArrayAsyncClauseSelector.Compile().Invoke(default);
			return new ToArrayAsyncQueryable<TSource>(source, toArrayAsyncClause.CancellationToken);
		}
		public static ToArrayAsyncQueryable<TElement> GroupBy<TSource, TElement>(this IQueryable<TSource> source, Expression<Func<TSource?, ToArrayAsyncClause>> toArrayAsyncClauseSelector, Expression<Func<TSource, TElement>> elementSelector) where TSource : class {
			ToArrayAsyncClause toArrayAsyncClause = toArrayAsyncClauseSelector.Compile().Invoke(default);
			return new ToArrayAsyncQueryable<TElement>(source.Select(elementSelector), toArrayAsyncClause.CancellationToken);
		}
		public static Task<TSource[]> Select<TSource, TResult>(this ToArrayAsyncQueryable<TSource> source, Expression<Func<TSource, TResult>> selector) {
			return source.Queryable.ToArrayAsync(source.CancellationToken);
		}

		// group x by ToDictionaryAsync into g
		public static ToDictionaryAsyncQueryable<TSource, TKey> GroupBy<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource?, (ToDictionaryAsyncClause, TKey)>> toDictionaryAsyncClauseSelector) where TSource : class {
			MethodCallExpression methodCall = (MethodCallExpression)toDictionaryAsyncClauseSelector.Body;
			MemberExpression memberExpression = (MemberExpression)methodCall.Arguments.First();
			ParameterExpression sourceParam = Expression.Parameter(typeof(TSource));
			Expression<Func<TSource, TKey>> keySelector = Expression.Lambda<Func<TSource, TKey>>(
				body: Expression.Property(
					expression: sourceParam,
					property: (PropertyInfo)memberExpression.Member
				),
				parameters: sourceParam
			);
			CancellationToken cancellationToken = CancellationToken.None;
			if (methodCall.Arguments.Count == 2) {
				MemberExpression fieldExpression = (MemberExpression)methodCall.Arguments.Last();
				Expression<Func<TSource?, CancellationToken>> cancellationTokenSelector = Expression.Lambda<Func<TSource?, CancellationToken>>(
					body: fieldExpression,
					parameters: sourceParam
				);
				cancellationToken = cancellationTokenSelector.Compile().Invoke(default);
			}
			return new ToDictionaryAsyncQueryable<TSource, TKey>(source,  keySelector.Compile(), cancellationToken);
		}
		public static ToDictionaryAsyncQueryable<TSource, TKey, TElement> GroupBy<TSource, TKey, TElement>(this IQueryable<TSource> source, Expression<Func<TSource?, (ToDictionaryAsyncClause, TKey)>> toDictionaryAsyncClauseSelector, Expression<Func<TSource, TElement>> elementSelector) where TSource : class {
			MethodCallExpression methodCall = (MethodCallExpression)toDictionaryAsyncClauseSelector.Body;
			MemberExpression propertyExpression = (MemberExpression)methodCall.Arguments.First();
			ParameterExpression sourceParam = Expression.Parameter(typeof(TSource));
			Expression<Func<TSource, TKey>> keySelector = Expression.Lambda<Func<TSource, TKey>>(
				body: Expression.Property(
					expression: sourceParam,
					property: (PropertyInfo)propertyExpression.Member
				),
				parameters: sourceParam
			);
			CancellationToken cancellationToken = CancellationToken.None;
			if (methodCall.Arguments.Count == 2) {
				MemberExpression fieldExpression = (MemberExpression)methodCall.Arguments.Last();
				Expression<Func<TSource?, CancellationToken>> cancellationTokenSelector = Expression.Lambda<Func<TSource?, CancellationToken>>(
					body: fieldExpression,
					parameters: sourceParam
				);
				cancellationToken = cancellationTokenSelector.Compile().Invoke(default);
			}
			return new ToDictionaryAsyncQueryable<TSource, TKey, TElement>(source, keySelector.Compile(), elementSelector.Compile(), cancellationToken);
		}
		public static Task<Dictionary<TKey, TSource>> Select<TSource, TKey, TResult>(this ToDictionaryAsyncQueryable<TSource, TKey> source, Expression<Func<TSource, TResult>> selector) where TKey : notnull {
			return source.Queryable.ToDictionaryAsync(source.KeySelector, source.CancellationToken);
		}
		public static Task<Dictionary<TKey, TElement>> Select<TSource, TKey, TElement, TResult>(this ToDictionaryAsyncQueryable<TSource, TKey, TElement> source, Expression<Func<TSource, TResult>> selector) where TKey : notnull {
			return source.Queryable.ToDictionaryAsync(source.KeySelector, source.ElementSelector, source.CancellationToken);
		}
	}
}
