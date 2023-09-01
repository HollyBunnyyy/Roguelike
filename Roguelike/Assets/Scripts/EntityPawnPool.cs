using System.Collections.Generic;
using UnityEngine;

public class EntityPawnPool : MonoBehaviour, IObjectPool<EntityPawn>
{
    [SerializeField]
    private int _amountToPrewarm;

    [SerializeField]
    private EntityPawn _pawnPrefab;

    private Queue<EntityPawn> _entityPool = new Queue<EntityPawn>();

    protected void Awake()
    {
        _amountToPrewarm = Mathf.Abs( _amountToPrewarm );

        for( int i = 0; i < _amountToPrewarm; i++ )
        {
            GenerateNewPawn();

        }
    }

    public EntityPawn GetNext()
    {
        if( _entityPool.Count <= 1 )
        {
            GenerateNewPawn();

        }

        EntityPawn entityPawnToRelease = _entityPool.Dequeue();
        entityPawnToRelease.Enable();

        return entityPawnToRelease;

    }

    public void ReturnToPool( EntityPawn objectToPool )
    {
        _entityPool.Enqueue( objectToPool );

    }

    public int Count()
    {
        return _entityPool.Count;
    }

    public void GenerateNewPawn()
    {
        EntityPawn pawnToInstantiate = Instantiate( _pawnPrefab, transform );
        pawnToInstantiate.TryBindToPool( this );

        ReturnToPool( pawnToInstantiate );

        pawnToInstantiate.Disable();

    }
}
