using UnityEngine;

public abstract class Character : MovementController, ITurnAgent
{
    [SerializeField]
    private float _health = 20.0f;
    public float Health 
    { 
        get 
        { 
            return _health;
        }

        private set 
        {
            _health = value;

            if( _health <= 0.0f )
            {
                Kill();

            }     
        }
    }

    [SerializeField]
    private TurnHandler _turnHandler;

    // TODO - will expand on inventory later, going to work on new features currently.
    public Inventory<Item> Inventory = new Inventory<Item>( 12 );

    public abstract bool TurnAction();

    protected virtual void Start()
    {
        _turnHandler.AddAgent( this );

        TryMoveToTile( CurrentTile, out Character character );

        Inventory.TryAdd( 1, new Item( 120001 ) );
        Inventory.TryAdd( 2, new Item( 120002 ) );
        Inventory.TryAdd( 6, new Item( 120003 ) );

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
        Destroy( gameObject );

    }

}
