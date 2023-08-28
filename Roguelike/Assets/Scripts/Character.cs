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
    public Inventory<int> Inventory = new Inventory<int>( 10 );

    public abstract bool TurnAction();

    protected virtual void Start()
    {
        _turnHandler.AddAgent( this );

        TryMoveToTile( CurrentTile, out Character character );

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
