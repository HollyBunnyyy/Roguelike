using System.Collections.Generic;
using UnityEngine;

public delegate void InventoryDirtyHandler();

public class Inventory<T> where T : class
{
    public int MaxSize          => _maxSize;
    public int OccupiedCount    => _items.Count;
    public int UnoccupiedCount  => MaxSize - OccupiedCount;
    public bool CanAddItems     => OccupiedCount < MaxSize;

    public event InventoryDirtyHandler OnCollectionDirty;

    private SortedDictionary<int, T> _items;

    private int _maxSize;

    public Inventory( int maxSize )
    {
        this._maxSize = maxSize;
        this._items = new SortedDictionary<int, T>();

    }

    public T this[int slotIndex]
    {
        get => _items[slotIndex];
        set 
        {
            _items[slotIndex] = value;

            OnCollectionDirty?.Invoke();

        }
    }


    /// <summary>
    /// Checks whether the given slot index is in bounds of the Inventory's allowed maximum size.
    /// </summary>
    public bool IsSlotIndexInInventory( int slotIndexToCheck )
    {
        return slotIndexToCheck >= 0 && slotIndexToCheck < MaxSize;

    }

    /// <summary>
    /// Checks whether the given slot index is currently occupied by an object.
    /// </summary>
    public bool IsSlotIndexOccupied( int slotIndexToCheck )
    {
        return _items.ContainsKey( slotIndexToCheck );

    }

    /// <summary>
    /// Attempts to add the given item to the specified slot index. 
    /// </summary>
    /// <returns> * True if the slot is in bounds and not occupied. </returns>
    public bool TryAdd( int itemSlotIndex, T itemToAdd )
    {
        if( IsSlotIndexOccupied( itemSlotIndex ) || !IsSlotIndexInInventory( itemSlotIndex ) || itemToAdd == null )
        {
            return false;

        }

        _items.Add( itemSlotIndex, itemToAdd );

        OnCollectionDirty?.Invoke();

        return true;

    }

    /// <summary>
    /// Attemps to remove an item belonging to the given slot index.
    /// </summary>
    /// <returns> * True if the slot is in bounds and occupied.  </returns>
    public bool TryRemove( int itemSlotIndex, out T itemRemoved )
    {
        itemRemoved = null;

        if( !IsSlotIndexOccupied( itemSlotIndex ) || !IsSlotIndexInInventory( itemSlotIndex ) )
        {
            // nothing to remove at the given index.

            return false;

        }

        itemRemoved = _items[itemSlotIndex];

        _items.Remove( itemSlotIndex );

        OnCollectionDirty?.Invoke();

        return true;

    }

    /// <summary>
    /// Attempts to swap two slot's objects with one another.
    /// </summary>
    public bool TrySwap( int itemSlotIndexA, int itemSlotIndexB )
    {
        // First, remove both items from the collection, as keys can't be updated.
        // If one index has a null reference ( not occupied ), it will simply be skipped.
        TryRemove( itemSlotIndexA, out T itemA );
        TryRemove( itemSlotIndexB, out T itemB );

        if( itemA == null && itemB == null )
        {
            // Both given indexes returned a null reference, this method call can be skipped as
            // there's no data to swap with the other index.

            return false;

        }

        // Attemps to add both indexes back to the other's respective index position.
        // TryAdd has a null check, so if any of the results above with TryRemove gave a null reference it will -
        // simply be skipped and the other object will just be set to the index position of the other.
        TryAdd( itemSlotIndexA, itemB );
        TryAdd( itemSlotIndexB, itemA );

        OnCollectionDirty?.Invoke();

        return true;

    }

    /// <summary>
    /// Increases the max size of the inventory collection.
    /// </summary>
    public void IncreaseTotalSize( int amountToIncrease )
    {
        _maxSize += Mathf.Abs( amountToIncrease );

        OnCollectionDirty?.Invoke();

    }

    /// <summary>
    /// Decreases the max size of the inventory collection.
    /// </summary>
    /// <returns> * Returns a list of all items that were removed from the collection. </returns>
    public List<T> DecreaseTotalSize( int amountToDecrease )
    {
        amountToDecrease = Mathf.Min( _maxSize, Mathf.Abs( amountToDecrease ) );

        List<T> itemsRemoved = new List<T>();

        for( int i = 1; i <= amountToDecrease; i++ )
        {
            int slotIndex = _maxSize - i;

            if( TryRemove( slotIndex, out T itemRemoved ) )
            {
                itemsRemoved.Add( itemRemoved );

            }
        }

        _maxSize -= amountToDecrease;

        OnCollectionDirty?.Invoke();

        return itemsRemoved;

    }

    /// <summary>
    /// Returns an IEnumerable list of all currently occupied slots.
    /// </summary>
    public IEnumerable<int> GetOccupiedSlots()
    {
        return _items.Keys;
    }

    /// <summary>
    /// Returns an IEnumerable list of all currently unoccupied slots.
    /// </summary>
    public IEnumerable<int> GetUnoccupiedSlots()
    {
        for( int i = 0; i < MaxSize; i++ )
        {
            if( IsSlotIndexOccupied( i ) )
            {
                continue;
            }

            yield return i;

        }
    }
}
