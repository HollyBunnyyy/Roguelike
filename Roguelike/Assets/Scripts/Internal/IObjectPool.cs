public interface IObjectPool<T> where T : class
{
    public T GetNext();
    public void ReturnToPool( T objectToPool );
    public int Count();
}
