using System.Collections.Generic;

public class Inventory<T>
{
    private int _maxSize;

    private List<T> _inventory;

    public int MaxSize          => _maxSize;
    public int OccupiedCount    => _inventory.Count;
    public int UnoccupiedCount  => MaxSize - OccupiedCount;
    public bool CanAddItems     => OccupiedCount < MaxSize;

    public Inventory( int maxSize )
    {
        this._inventory = new List<T>();
        this._maxSize = maxSize;

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
        if( amountToIncrease <= 0 )
        {
            return;

        }

        _maxSize += amountToIncrease;

    }

    /// <summary>
    /// Decreases the total maximum size of the inventory. 
    /// </summary>
    /// <returns> * Returns an IEnumerable of any items that were removed. </returns>
    public List<T> DecreaseTotalSize( int amountToDecrease )
    {
        if( amountToDecrease > MaxSize )
        {
            amountToDecrease = MaxSize;

        }

        _maxSize -= amountToDecrease;

        List<T> itemsRemoved = new List<T>();

        if( amountToDecrease >= 0 || OccupiedCount >= 0 )
        {
            for( int i = 0; i < amountToDecrease; i++ )
            {
                T objectOccupyingIndex = _inventory[^1];

                if( objectOccupyingIndex != null )
                {
                    _inventory.Remove( objectOccupyingIndex );

                    itemsRemoved.Add( objectOccupyingIndex );

                }
            }
        }

        return itemsRemoved;

    }
}
