using UnityEngine;

[RequireComponent( typeof( TurnHandler ) )]
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TurnHandler _turnHandler;
    public TurnHandler TurnHandler 
    { 
        get { return _turnHandler != null ? _turnHandler : _turnHandler = GetComponent<TurnHandler>(); } 
    }

    public void Awake()
    {
        _turnHandler = GetComponent<TurnHandler>();

    }
}
