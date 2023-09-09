using UnityEngine;

// TODO : Clean this class up and abstract.

public class Character : Entity
{
    private Health _health = new Health( 20.0f );
    public Health Health 
    { 
        get 
        {        
            if( _health.Amount <= 0.0f )
                Kill();

            return _health;    
        } 
    }

    public Inventory<Item> Inventory = new Inventory<Item>( 16 );

    public void OnDisable()
    {
        CurrentTile.OccupyingEntity = null;
    }

    public void Kill()
    {
        foreach( Item item in Inventory.GetItems() )
        {
            CurrentTile.OccupyingItems.TryAddNext( item );
        }

        Destroy( gameObject );

    }
}
