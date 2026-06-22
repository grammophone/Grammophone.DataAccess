using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Grammophone.DataAccess.Collections
{
	/// <summary>
	/// A <see cref="HashSet{T}"/> implementation that also implements <see cref="INotifyCollectionChanged"/> 
	/// and <see cref="INotifyPropertyChanged"/> to support EF Core change-tracking proxies 
	/// and notification-based change tracking without depending on EF Core types 
	/// in the domain model.
	/// </summary>
	/// <remarks>
	/// This provides the set semantics and lookup characteristics of <see cref="HashSet{T}"/>
	/// while enabling immediate collection change detection.
	/// Use this in entity collection navigations when notification-based change tracking is required,
	/// such as with Entity Framework Core change-tracking proxies.
	/// </remarks>
	[Serializable]
	public class ObservableHashSet<T> : HashSet<T>, INotifyCollectionChanged, INotifyPropertyChanged, ISet<T>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ObservableHashSet{T}"/> class.
		/// </summary>
		public ObservableHashSet() { }

		/// <summary>
		/// Initializes a new instance of the <see cref="ObservableHashSet{T}"/> class
		/// with the specified collection.
		/// </summary>
		/// <param name="collection">The collection whose elements are copied to the new set.</param>
		public ObservableHashSet(IEnumerable<T> collection) : base(collection) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="ObservableHashSet{T}"/> class
		/// with the specified equality comparer.
		/// </summary>
		/// <param name="comparer">The equality comparer.</param>
		public ObservableHashSet(IEqualityComparer<T> comparer) : base(comparer) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="ObservableHashSet{T}"/> class
		/// with the specified collection and equality comparer.
		/// </summary>
		/// <param name="collection">The collection whose elements are copied.</param>
		/// <param name="comparer">The equality comparer.</param>
		public ObservableHashSet(IEnumerable<T> collection, IEqualityComparer<T> comparer)
				: base(collection, comparer) { }

		/// <summary>
		/// Occurs when the collection changes (items added, removed, etc.).
		/// </summary>
		[field: NonSerialized]
		public event NotifyCollectionChangedEventHandler CollectionChanged;

		/// <summary>
		/// Occurs when a property value changes.
		/// </summary>
		[field: NonSerialized]
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Raises the <see cref="CollectionChanged"/> event and notifies that <see cref="HashSet{T}.Count"/> changed.
		/// </summary>
		/// <param name="e">The event arguments.</param>
		protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
		{
			CollectionChanged?.Invoke(this, e);
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Count)));
		}

		/// <summary>
		/// Raises the PropertyChanged event.
		/// </summary>
		/// <param name="propertyName">The name of the changed property.</param>
		protected virtual void OnPropertyChanged(string propertyName)
				=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

		/// <summary>
		/// Adds an element to the set and raises the appropriate collection changed event.
		/// </summary>
		/// <param name="item">The item to add.</param>
		/// <returns>true if the item was added; false if it was already present.</returns>
		public new bool Add(T item)
		{
			bool added = base.Add(item);

			if (added)
			{
				OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
			}

			return added;
		}

		/// <summary>
		/// Adds an element to the set and raises the appropriate collection changed event.
		/// </summary>
		/// <param name="item">The item to add.</param>
		void ICollection<T>.Add(T item)
		{
			Add(item);
		}

		/// <summary>
		/// Removes an element from the set and raises the appropriate collection changed event.
		/// </summary>
		/// <param name="item">The item to remove.</param>
		/// <returns>true if the item was removed; false if it was not present.</returns>
		public new bool Remove(T item)
		{
			bool removed = base.Remove(item);

			if (removed)
			{
				OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
			}

			return removed;
		}

		/// <summary>
		/// Removes an element from the set and raises the appropriate collection changed event.
		/// </summary>
		/// <param name="item">The item to remove.</param>
		/// <returns>true if the item was removed; false if it was not present.</returns>
		bool ICollection<T>.Remove(T item)
		{
			return Remove(item);
		}

		/// <summary>
		/// Removes all elements from the set and raises the appropriate collection changed event.
		/// </summary>
		public new void Clear()
		{
			var items = this.ToList();
			base.Clear();

			if (items.Count > 0)
			{
				OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, items));
			}
		}

		/// <summary>
		/// Removes all elements from the set and raises the appropriate collection changed event.
		/// </summary>
		void ICollection<T>.Clear()
		{
			Clear();
		}

		/// <summary>
		/// Modifies the current set to contain all elements that are present in itself,
		/// the specified collection, or both. Raises collection changed events for added items.
		/// </summary>
		public new void UnionWith(IEnumerable<T> other)
		{
			if (other == null) throw new ArgumentNullException(nameof(other));

			var added = new List<T>();

			foreach (var item in other)
			{
				if (base.Add(item))
				{
					added.Add(item);
				}
			}

			if (added.Count == 0) return;

			OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, added));
		}

		/// <summary>
		/// Modifies the current set to contain all elements that are present in itself,
		/// the specified collection, or both.
		/// </summary>
		void ISet<T>.UnionWith(IEnumerable<T> other)
		{
			UnionWith(other);
		}

		/// <summary>
		/// Removes all elements in the specified collection from the current set.
		/// Raises collection changed events for removed items.
		/// </summary>
		public new void ExceptWith(IEnumerable<T> other)
		{
			if (other == null) throw new ArgumentNullException(nameof(other));

			var otherSet = new HashSet<T>(other, Comparer);
			var removed = this.Where(otherSet.Contains).ToList();

			if (removed.Count == 0) return;

			foreach (var item in removed)
			{
				base.Remove(item);
			}

			OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, removed));
		}

		/// <summary>
		/// Removes all elements in the specified collection from the current set.
		/// </summary>
		void ISet<T>.ExceptWith(IEnumerable<T> other)
		{
			ExceptWith(other);
		}

		/// <summary>
		/// Modifies the current set to contain only elements that are present in both
		/// the current set and the specified collection.
		/// </summary>
		public new void IntersectWith(IEnumerable<T> other)
		{
			if (other == null) throw new ArgumentNullException(nameof(other));

			var otherSet = new HashSet<T>(other, Comparer);
			var toRemove = this.Where(item => !otherSet.Contains(item)).ToList();

			if (toRemove.Count == 0) return;

			foreach (var item in toRemove)
			{
				base.Remove(item);
			}

			OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, toRemove));
		}

		/// <summary>
		/// Modifies the current set to contain only elements that are present in both
		/// the current set and the specified collection.
		/// </summary>
		void ISet<T>.IntersectWith(IEnumerable<T> other)
		{
			IntersectWith(other);
		}

		/// <summary>
		/// Modifies the current set to contain only elements that are present either in the current set
		/// or in the specified collection, but not both.
		/// </summary>
		public new void SymmetricExceptWith(IEnumerable<T> other)
		{
			if (other == null) throw new ArgumentNullException(nameof(other));

			var currentSet = new HashSet<T>(this, Comparer);
			var otherSet = new HashSet<T>(other, Comparer);
			var removed = currentSet.Where(otherSet.Contains).ToList();
			var added = otherSet.Where(item => !currentSet.Contains(item)).ToList();

			if (removed.Count == 0 && added.Count == 0) return;

			foreach (var item in removed)
			{
				base.Remove(item);
			}

			foreach (var item in added)
			{
				base.Add(item);
			}

			if (removed.Count > 0)
			{
				OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, removed));
			}

			if (added.Count > 0)
			{
				OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, added));
			}
		}

		/// <summary>
		/// Modifies the current set to contain only elements that are present either in the current set
		/// or in the specified collection, but not both.
		/// </summary>
		void ISet<T>.SymmetricExceptWith(IEnumerable<T> other)
		{
			SymmetricExceptWith(other);
		}

		/// <summary>
		/// Removes all elements that match the conditions defined by the specified predicate.
		/// </summary>
		/// <param name="match">The predicate that defines the conditions of the elements to remove.</param>
		/// <returns>The number of elements that were removed.</returns>
		public new int RemoveWhere(Predicate<T> match)
		{
			if (match == null) throw new ArgumentNullException(nameof(match));

			var removed = this.Where(item => match(item)).ToList();

			if (removed.Count == 0) return 0;

			foreach (var item in removed)
			{
				base.Remove(item);
			}

			OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, removed));

			return removed.Count;
		}
	}
}
