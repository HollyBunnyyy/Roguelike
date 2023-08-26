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

    public bool IsSlotIndexValid( int slotIndexToCheck )
    {
        return slotIndexToCheck >= 0 && slotIndexToCheck < MaxSize;

    }

    public bool TryAdd( T objectToAdd, int indexToInsertAt )
    {
        if( !IsSlotIndexValid( indexToInsertAt ) || !CanAddItems )
        {
            return false;

        }

        _inventory.Insert( indexToInsertAt, objectToAdd );

        return true;

    }

    public bool TryAddLast( T objectToAdd )
    {
        return TryAdd( objectToAdd, _inventory.Count );

    }

    public bool TryRemove( int slotIndex )
    {
        if( !IsSlotIndexValid( slotIndex ) )
        {
            return false;

        }

        _inventory.RemoveAt( slotIndex );

        return true;

    }

    public void IncreaseTotalSize( int amountToIncrease )
    {
        if( amountToIncrease <= 0 )
        {
            return;

        }

        _maxSize += amountToIncrease;

    }

    public IEnumerable<T> DecreaseTotalSize( int amountToDecrease )
    {
        if( amountToDecrease <= 0 )
        {
            yield break;

        }

        if( amountToDecrease > MaxSize )
        {
            amountToDecrease = MaxSize;

        }

        _maxSize -= amountToDecrease;

        for( int i = 0; i < amountToDecrease; i++ )
        {
            T objectOccupyingIndex = _inventory[^1];

            if( objectOccupyingIndex != null )
            {
                _inventory.Remove( objectOccupyingIndex );

                yield return objectOccupyingIndex;

            }
        }
    }
}
