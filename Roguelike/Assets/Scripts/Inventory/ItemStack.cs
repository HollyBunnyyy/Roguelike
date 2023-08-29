public struct ItemStack<T> where T : class
{
    public readonly T Item;
    public readonly int Count;

    public ItemStack( T item, int count )
    {
        this.Item = item;
        this.Count = count;

    }

}
