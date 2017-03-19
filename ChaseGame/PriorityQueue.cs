﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures.PriorityQueues
{
    public class PriorityQueue<T>
	{
		private const int InitialCapacity = 100;
		private T[] queue;
		private Dictionary<T, int> indices;
		private IComparer<T> comparer;
		public int Count { get; private set; }

		public PriorityQueue() : this(Enumerable.Empty<T>(), Comparer<T>.Default)
		{			
		}

		public PriorityQueue(IEnumerable<T> keys) : this(keys, Comparer<T>.Default)
		{
		}

		public PriorityQueue(IComparer<T> comparer) : this(Enumerable.Empty<T>(), comparer)
		{
		}

		public PriorityQueue(IEnumerable<T> keys, IComparer<T> comparer)
		{
			if (keys == null)
			{
				throw new ArgumentNullException("keys", "Specify a non-null argument.");
			}

			if (comparer == null)
			{
				throw new ArgumentNullException("comparer", "Specify a non-null argument.");
			}

			this.queue = new T[InitialCapacity];
			this.indices = new Dictionary<T, int>();
			this.comparer = comparer;
			this.Count = 0;
			foreach (T key in keys)
			{
				this.Enqueue(key);
			}
		}

		public bool IsEmpty()
		{
			return this.Count == 0;
		}

		public T Peek()
		{
			if (this.IsEmpty())
			{
				throw new InvalidOperationException("Queue does not contain any elements.");
			}
			
			return this.queue[1];
		}

		public void Enqueue(T key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", "Specify a non-null argument.");
			}

			if (this.IsFull())
			{
				this.Resize(2 * this.queue.Length);
			}

			this.queue[++this.Count] = key;
			this.indices[key] = this.Count;
			this.BubbleUp(this.Count);
		}

		public T Dequeue()
		{
			if (this.IsEmpty())
			{
				throw new InvalidOperationException("Queue does not contain any elements.");
			}

			this.Swap(1, this.Count);
			T result = this.queue[this.Count--];
			this.BubbleDown(1);
			this.indices.Remove(result);
			this.queue[this.Count + 1] = default(T);
			if (this.Count > 0 && this.Count == (this.queue.Length - 1) / 4)
			{
				this.Resize(this.queue.Length / 2);
			}

			return result;
		}

		public T Dequeue(T key)
		{
			if (this.IsEmpty())
			{
				throw new InvalidOperationException("Queue does not contain any elements.");
			}

			if (!this.indices.ContainsKey(key))
			{
				throw new InvalidOperationException("Queue does not contain given key.");
			}

			int keyIndex = this.indices[key];
			this.Swap(keyIndex, this.Count);
			T result = this.queue[this.Count--];
			this.BubbleDown(keyIndex);
			this.queue[this.Count + 1] = default(T);
			this.indices.Remove(result);
			if (this.Count > 0 && this.Count == (this.queue.Length - 1) / 4)
			{
				this.Resize(this.queue.Length / 2);
			}

			return result;
		}

		private bool IsFull()
		{
			return this.Count == this.queue.Length - 1;
		}

		private void Resize(int capacity)
		{
			T[] newQueue = new T[capacity];
			for (int i = 1; i <= this.Count; i++)
			{
				newQueue[i] = this.queue[i];
			}

			this.queue = newQueue;
		}

		private bool IsGreater(int i, int j)
		{
			return this.comparer.Compare(this.queue[i], this.queue[j]) > 0;
		}

		private void BubbleUp(int k)
		{
			while (k > 1 && this.IsGreater(k / 2, k))
			{
				this.Swap(k, k / 2);
				k = k / 2;
			}
		}

		private void BubbleDown(int k)
		{
			while (2 * k <= this.Count)
			{
				int i = 2 * k;
				if (i < this.Count && this.IsGreater(i, i + 1))
				{
					i++;
				}

				if (!this.IsGreater(k, i))
				{
					break;
				}

				this.Swap(k, i);
				k = i;
			}
		}				

		private void Swap(int i, int j)
		{
			T swapper = this.queue[i];
			this.queue[i] = this.queue[j];
			this.queue[j] = swapper;
			this.indices[this.queue[i]] = i;
			this.indices[this.queue[j]] = j;
		}
	}
}