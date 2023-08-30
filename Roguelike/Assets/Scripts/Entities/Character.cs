using UnityEngine;

public abstract class Character : TurnAgent
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

    [SerializeField]
    private TurnHandler _turnHandler;

    public Inventory<Item> Inventory = new Inventory<Item>( 16 );

    protected virtual void Start()
    {
        _turnHandler.AddAgent( this );

        TryMoveToTile( CurrentTile, out Entity entityOccupying );

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
