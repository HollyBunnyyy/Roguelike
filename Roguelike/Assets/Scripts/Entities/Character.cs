using UnityEngine;

// TODO : Clean this class up and abstract.

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

    public Inventory<Item> Inventory = new Inventory<Item>( 16 );

    protected override void Start()
    {
        base.Start();

        Roguelike.Instance.TurnHandler.AddAgent( this );

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
