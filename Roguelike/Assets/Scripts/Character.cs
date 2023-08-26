using UnityEngine;

public abstract class Character : MovementController, ITurnAgent
{
    [SerializeField]
    private TurnHandler _turnHandler;

    // TODO - will expand on inventory later, going to work on new features currently.
    public Inventory<int> Inventory = new Inventory<int>( 10 );

    public abstract bool TurnAction();

    protected virtual void Start()
    {
        _turnHandler.AddAgent( this );

        TryMoveToTile( CurrentTile );

    }

}
