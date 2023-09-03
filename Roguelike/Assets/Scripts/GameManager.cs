using UnityEngine;

// Empty for now, will add game control logic later.

[RequireComponent( typeof( TurnHandler ) )]
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TurnHandler _turnHandler;
    public TurnHandler TurnHandler => _turnHandler;

    public void Awake()
    {
        _turnHandler = GetComponent<TurnHandler>();

    }
}
