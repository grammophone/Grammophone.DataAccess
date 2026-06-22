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
	/// This provides the performance characteristics of <see cref="HashSet{T}"/> (O(1) lookups, 
	/// no duplicates) while enabling immediate collection change detection.
	/// Use this in entity collection navigations when using UseChangeTrackingProxies()
	/// or notification entities.
	/// 
	/// Designed to maintain maximum POCO compatibility and serialization support.
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
		/// Occurs when a property value changes (primarily Count).
		/// </summary>
		[field: NonSerialized]
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Raises the CollectionChanged event.
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
				OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));

			return added;
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
				OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));

			return removed;
		}

		/// <summary>
		/// Removes all elements from the set and raises the appropriate collection changed event.
		/// </summary>
		public new void Clear()
		{
			var items = this.ToList();
			base.Clear();

			if (items.Count > 0)
				OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, items));
		}

		/// <summary>
		/// Modifies the current set to contain all elements that are present in itself,
		/// the specified collection, or both. Raises collection changed events for added items.
		/// </summary>
		public new void UnionWith(IEnumerable<T> other)
		{
			var added = other.Where(x => !Contains(x)).ToList();
			if (added.Count == 0) return;

			base.UnionWith(added);
			OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, added));
		}

		/// <summary>
		/// Removes all elements in the specified collection from the current set.
		/// Raises collection changed events for removed items.
		/// </summary>
		public new void ExceptWith(IEnumerable<T> other)
		{
			var removed = other.Where(Contains).ToList();
			if (removed.Count == 0) return;

			base.ExceptWith(removed);
			OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, removed));
		}

		/// <summary>
		/// Modifies the current set to contain only elements that are present in both
		/// the current set and the specified collection.
		/// </summary>
		public new void IntersectWith(IEnumerable<T> other)
		{
			var toRemove = this.Except(other).ToList();
			if (toRemove.Count == 0) return;

			base.IntersectWith(other);
			OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, toRemove));
		}
	}
}
