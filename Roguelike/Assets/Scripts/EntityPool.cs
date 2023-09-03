using System.Collections.Generic;
using UnityEngine;

public class EntityPool : MonoBehaviour, IObjectPool<Entity>
{
    [SerializeField]
    private int _amountToPrewarm;

    private Queue<Entity> _entityPool = new Queue<Entity>();

    protected void Awake()
    {
        _amountToPrewarm = Mathf.Abs( _amountToPrewarm );

        for( int i = 0; i < _amountToPrewarm; i++ )
        {
            GenerateNewPawn();

        }
    }

    public Entity GetNext()
    {
        if( _entityPool.Count <= 1 )
        {
            GenerateNewPawn();

        }

        Entity entityPawnToRelease = _entityPool.Dequeue();
        entityPawnToRelease.Enable();

        return entityPawnToRelease;

    }

    public void ReturnToPool( Entity objectToPool )
    {
        _entityPool.Enqueue( objectToPool );

    }

    public int Count()
    {
        return _entityPool.Count;
    }

    public void GenerateNewPawn()
    {
        Entity pawnToInstantiate = new GameObject( "BlankEntity" ).AddComponent<Entity>();
        pawnToInstantiate.transform.SetParent( transform );
        pawnToInstantiate.TryBindToPool( this );

        ReturnToPool( pawnToInstantiate );

        pawnToInstantiate.Disable();

    }
}