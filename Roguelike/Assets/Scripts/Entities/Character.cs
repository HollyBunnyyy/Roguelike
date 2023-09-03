using UnityEngine;

// TODO : Clean this class up and abstract.

public class Character : Entity
{
    [SerializeField]
    private float _health = 20.0f;
    public float Health 
    { 
        get { return _health; }
        private set 
        {
            _health = value;

            if( _health <= 0.0f )
            {
                Kill();

            }     
        }
    }

    public Inventory<Item> Inventory = new Inventory<Item>( 16 );

    public void OnDisable()
    {
        CurrentTile.OccupyingEntity = null;
    }

    public void Heal( float amountToHeal )
    {
        Health += amountToHeal;

    }

    public void Damage( float amountToDamage )
    {
        Health -= amountToDamage;

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
