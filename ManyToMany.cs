using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Grammophone.DataAccess
{
	/// <summary>
	/// Base type for many-to-many join entities.
	/// <para>
	/// Can be used directly for simple relationships that only need the two
	/// foreign keys, or subclassed when the join table carries additional
	/// payload (order, timestamps, reviewers, etc.).
	/// </para>
	/// <para>
	/// Implements <see cref="INotifyPropertyChanging"/> and
	/// <see cref="INotifyPropertyChanged"/> so that the type works correctly
	/// both with change-tracking proxies and when instantiated directly.
	/// </para>
	/// </summary>
	/// <typeparam name="TLeft">The left-side entity type.</typeparam>
	/// <typeparam name="TRight">The right-side entity type.</typeparam>
	[Serializable]
	public class ManyToMany<TLeft, TRight> :
			INotifyPropertyChanging,
			INotifyPropertyChanged
			where TLeft : class
			where TRight : class
	{
		#region Private fields

		private TLeft left;
		private TRight right;

		#endregion

		#region Public properties

		/// <summary>
		/// Gets or sets the left-side entity.
		/// </summary>
		public virtual TLeft Left
		{
			get { return left; }
			set
			{
				if (!ReferenceEquals(left, value))
				{
					OnPropertyChanging();
					left = value;
					OnPropertyChanged();
				}
			}
		}

		/// <summary>
		/// Gets or sets the right-side entity.
		/// </summary>
		public virtual TRight Right
		{
			get { return right; }
			set
			{
				if (!ReferenceEquals(right, value))
				{
					OnPropertyChanging();
					right = value;
					OnPropertyChanged();
				}
			}
		}

		#endregion

		#region Events

		/// <summary>
		/// Occurs when a property value is about to change.
		/// </summary>
		public event PropertyChangingEventHandler PropertyChanging;

		/// <summary>
		/// Occurs when a property value has changed.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		#region Protected methods

		/// <summary>
		/// Raises the <see cref="PropertyChanging"/> event.
		/// </summary>
		/// <param name="propertyName">
		/// The name of the property that is about to change.
		/// Automatically supplied by the compiler when omitted.
		/// </param>
		protected virtual void OnPropertyChanging([CallerMemberName] string propertyName = null)
		{
			PropertyChangingEventHandler handler = PropertyChanging;
			if (handler != null)
			{
				handler(this, new PropertyChangingEventArgs(propertyName));
			}
		}

		/// <summary>
		/// Raises the <see cref="PropertyChanged"/> event.
		/// </summary>
		/// <param name="propertyName">
		/// The name of the property that changed.
		/// Automatically supplied by the compiler when omitted.
		/// </param>
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion
	}

	/// <summary>
	/// Concrete many-to-many join entity with explicit foreign-key properties.
	/// <para>
	/// Use this instead of <see cref="ManyToMany{TLeft, TRight}"/> when you need
	/// to reference the FK values as CLR properties (e.g., for column rename via
	/// <c>.Property(mm => mm.LeftID).HasColumnName("X_ID")</c> in <c>OnModelCreating</c>).
	/// </para>
	/// </summary>
	/// <typeparam name="TLeft">The left-side entity type.</typeparam>
	/// <typeparam name="TLeftKey">The primary-key type of the left-side entity.</typeparam>
	/// <typeparam name="TRight">The right-side entity type.</typeparam>
	/// <typeparam name="TRightKey">The primary-key type of the right-side entity.</typeparam>
	[Serializable]
	public class ManyToMany<TLeft, TLeftKey, TRight, TRightKey> : ManyToMany<TLeft, TRight>
		where TLeft : class
		where TRight : class
	{
		#region Private fields

		private TLeftKey leftID;
		private TRightKey rightID;

		#endregion

		#region Public properties

		/// <summary>
		/// Gets or sets the foreign-key value that references the left-side entity.
		/// </summary>
		public virtual TLeftKey LeftID
		{
			get { return leftID; }
			set
			{
				if (!Equals(leftID, value))
				{
					OnPropertyChanging();
					leftID = value;
					OnPropertyChanged();
				}
			}
		}

		/// <summary>
		/// Gets or sets the foreign-key value that references the right-side entity.
		/// </summary>
		public virtual TRightKey RightID
		{
			get { return rightID; }
			set
			{
				if (!Equals(rightID, value))
				{
					OnPropertyChanging();
					rightID = value;
					OnPropertyChanged();
				}
			}
		}

		#endregion
	}
}
