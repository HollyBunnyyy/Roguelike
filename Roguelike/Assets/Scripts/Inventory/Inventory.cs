using System.Collections.Generic;
using UnityEngine;

public class Inventory<T> where T : class
{
    public int MaxSize          => _maxSize;
    public int OccupiedCount    => _items.Count;
    public int UnoccupiedCount  => MaxSize - OccupiedCount;
    public bool CanAddItems     => OccupiedCount < MaxSize;

    private int _maxSize;

    private SortedDictionary<int, T> _items;

    public Inventory( int maxSize )
    {
        this._maxSize = maxSize;
        this._items = new SortedDictionary<int, T>();

    }

    public T this[int slotIndex]
    {
        get => _items[slotIndex];
        set => _items[slotIndex] = value;

    }

    public bool IsSlotIndexInInventory( int slotIndexToCheck )
    {
        return slotIndexToCheck >= 0 && slotIndexToCheck < MaxSize;

    }

    public bool IsSlotIndexOccupied( int slotIndexToCheck )
    {
        return _items.ContainsKey( slotIndexToCheck );


    }

    public bool TryAdd( int itemSlotIndex, T itemToAdd )
    {
        if( IsSlotIndexOccupied( itemSlotIndex ) || !IsSlotIndexInInventory( itemSlotIndex ) || !CanAddItems )
        {
            return false;

        }

        _items.Add( itemSlotIndex, itemToAdd );

        return true;

    }

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

        return true;

    }

    public void IncreaseTotalSize( int amountToIncrease )
    {
        _maxSize += Mathf.Abs( amountToIncrease );

    }

    public List<T> DecreaseTotalSize( int amountToDecrease )
    {
        amountToDecrease = Mathf.Min( _maxSize, Mathf.Abs( amountToDecrease ) );

        List<T> itemsRemoved = new List<T>();

        if( OccupiedCount > 0 )
        {
            for( int i = 1; i <= amountToDecrease; i++ )
            {
                int slotIndex = _maxSize - i;

                if( TryRemove( slotIndex, out T itemRemoved ) )
                {
                    itemsRemoved.Add( itemRemoved );

                }
            }
        }

        _maxSize -= amountToDecrease;

        return itemsRemoved;

    }
}

/*
    private int _maxSize;

    private List<T> _inventory;

    public int MaxSize          => _maxSize;
    public int OccupiedCount    => _inventory.Count;
    public int UnoccupiedCount  => MaxSize - OccupiedCount;
    public bool CanAddItems     => OccupiedCount < MaxSize;

    public Inventory( int maxSize )
    {
        this._maxSize = maxSize;
        this._inventory = new List<T>();

    }


    public T this[int slotIndex]
    {
        get => _inventory[slotIndex];
        set => _inventory[slotIndex] = value;

    }


    /// <summary>
    /// Checks if the given slot's index is in range of the inventory's total slots available.
    /// </summary>
    /// <returns> * True if in range. </returns>
    public bool IsSlotValid( int slotIndexToCheck )
    {
        return slotIndexToCheck >= 0 && slotIndexToCheck < MaxSize;

    }

    /// <summary>
    /// Attempts to add an object at the specified slot index.
    /// </summary>
    /// <returns> * True if inventory has enough space and the given object can be added. </returns>
    public bool TryAdd( T objectToAdd, int indexToInsertAt )
    {
        if( !IsSlotValid( indexToInsertAt ) || !CanAddItems )
        {
            return false;

        }

        _inventory.Insert( indexToInsertAt, objectToAdd );

        return true;

    }


    /// <summary>
    /// Attempts to add the given object to the next available slot.
    /// </summary>
    /// <returns> * True if inventory has enough space and the given object can be added.</returns>
    public bool TryAddLast( T objectToAdd )
    {
        return TryAdd( objectToAdd, _inventory.Count );

    }

    /// <summary>
    /// Attempts to remove the item assosciated with the given slot index.
    /// </summary>
    /// <remarks> Does not decrease the max size of the inventory. </remarks>
    /// <returns> * True if the given slot index is valid. </returns>
    public bool TryRemove( int slotIndex )
    {
        if( !IsSlotValid( slotIndex ) )
        {
            return false;

        }

        _inventory.RemoveAt( slotIndex );

        return true;

    }

    /// <summary>
    /// Increases the total maximum size of the inventory.
    /// </summary>
    public void IncreaseTotalSize( int amountToIncrease )
    {
        _maxSize += Mathf.Abs( amountToIncrease );

    }

    /// <summary>
    /// Decreases the total maximum size of the inventory. 
    /// </summary>
    /// <returns> * Returns an IEnumerable of any items that were removed. </returns>
    public List<T> DecreaseTotalSize( int amountToDecrease )
    {
        amountToDecrease = Mathf.Abs( amountToDecrease );

        List<T> itemsRemoved = new List<T>();

        if( OccupiedCount > 0 )
        {
            for( int i = 1; i <= amountToDecrease; i++ )
            {
                T objectOccupyingIndex = _inventory[_maxSize - i];

                if( objectOccupyingIndex != null )
                {
                    _inventory.Remove( objectOccupyingIndex );

                    itemsRemoved.Add( objectOccupyingIndex );

                }
            }
        }

        _maxSize -= amountToDecrease;

        return itemsRemoved;

    }
 */