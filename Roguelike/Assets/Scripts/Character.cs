using UnityEngine;

public abstract class Character : MovementController, ITurnAgent
{
    [SerializeField]
    private TurnHandler _turnHandler;

    public abstract bool TurnAction();

    protected virtual void Start()
    {
        _turnHandler.AddAgent( this );

        CurrentTile.OccupyingCharacter = this;

    }

}
