public interface ILookupTable<T>
{
    public bool TryGetID( int id, out T objOut );
    public bool HasID( int id );

}
