// Method TakeLastIterator and SkipLastIterator are licensed to the .NET Foundation under one or more agreements.
// See the LICENSE file in the project root of corefx for more information: https://github.com/dotnet/corefx/blob/master/LICENSE.TXT

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Parenthless {
	internal static class Implementations {
		internal static IEnumerable<TSource> TakeLast<TSource>(IEnumerable<TSource> source, int count) {
			if (source is null) {
				throw new System.ArgumentNullException(nameof(source));
			}

			return count <= 0 ? Enumerable.Empty<TSource>() : TakeLastIterator(source, count);
		}

		// Taken from TakeLastIterator in https://github.com/dotnet/corefx/blob/50fc80c8023060d61a826b01733a93840018fe92/src/System.Linq/src/System/Linq/Take.cs
		// Licensed to the .NET Foundation under one or more agreements.
		private static IEnumerable<TSource> TakeLastIterator<TSource>(IEnumerable<TSource> source, int count) {
			Debug.Assert(source != null);
			Debug.Assert(count > 0);

			Queue<TSource> queue;
			using (IEnumerator<TSource> e = source.GetEnumerator()) {
				if (!e.MoveNext()) {
					yield break;
				}

				queue = new Queue<TSource>();
				queue.Enqueue(e.Current);

				while (e.MoveNext()) {
					if (queue.Count < count) {
						queue.Enqueue(e.Current);
					} else {
						do {
							queue.Dequeue();
							queue.Enqueue(e.Current);
						}
						while (e.MoveNext());
						break;
					}
				}
			}

			Debug.Assert(queue.Count <= count);
			do {
				yield return queue.Dequeue();
			}
			while (queue.Count > 0);
		}

		internal static IEnumerable<TSource> SkipLast<TSource>(IEnumerable<TSource> source, int count) {
			if (source is null) {
				throw new System.ArgumentNullException(nameof(source));
			}

			return count <= 0 ? source : SkipLastIterator(source, count);
		}

		// Taken from SkipLastIterator in https://github.com/dotnet/corefx/blob/8750960d3fafa46a9b838c351e995a01fa8b599f/src/System.Linq/src/System/Linq/Skip.cs
		// Licensed to the .NET Foundation under one or more agreements.
		private static IEnumerable<TSource> SkipLastIterator<TSource>(IEnumerable<TSource> source, int count) {
			Debug.Assert(source != null);
			Debug.Assert(count > 0);

			var queue = new Queue<TSource>();

			using (IEnumerator<TSource> e = source.GetEnumerator()) {
				while (e.MoveNext()) {
					if (queue.Count == count) {
						do {
							yield return queue.Dequeue();
							queue.Enqueue(e.Current);
						}
						while (e.MoveNext());
						break;
					} else {
						queue.Enqueue(e.Current);
					}
				}
			}
		}

		internal static IEnumerable<TSource> SkipLastThenTakeLast<TSource>(IEnumerable<TSource> source, int skipLastCount, int takeLastCount) {
			if (source is null) {
				throw new System.ArgumentNullException(nameof(source));
			}

			if (takeLastCount <= 0) {
				return Enumerable.Empty<TSource>();
			} else if (skipLastCount <= 0) {
				return TakeLastIterator(source, takeLastCount);
			} else {
				return SkipLastThenTakeLastIterator(source, skipLastCount, takeLastCount);
			}
		}

		private static IEnumerable<TSource> SkipLastThenTakeLastIterator<TSource>(IEnumerable<TSource> source, int skipLastCount, int takeLastCount) {
			Debug.Assert(source != null);
			Debug.Assert(takeLastCount > 0);

			int count = skipLastCount + takeLastCount;

			Queue<TSource> queue;
			using (IEnumerator<TSource> e = source.GetEnumerator()) {
				if (!e.MoveNext()) {
					yield break;
				}

				queue = new Queue<TSource>();
				queue.Enqueue(e.Current);

				while (e.MoveNext()) {
					if (queue.Count < count) {
						queue.Enqueue(e.Current);
					} else {
						do {
							queue.Dequeue();
							queue.Enqueue(e.Current);
						}
						while (e.MoveNext());
						break;
					}
				}
			}

			Debug.Assert(queue.Count <= count);
			do {
				yield return queue.Dequeue();
			}
			while (queue.Count > skipLastCount);
		}
	}
}
