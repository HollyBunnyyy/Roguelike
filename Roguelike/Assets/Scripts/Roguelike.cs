using UnityEngine;

[RequireComponent( typeof( GameManager ), typeof( AssetManager ), typeof( InputHandler ) )]
public class Roguelike : MonoBehaviour
{
    private static Roguelike _instance = null;
    public static Roguelike Instance => FindRoguelikeUniqueInstance();

    [SerializeField]
    private GameManager _gameManager;
    public GameManager GameManager => _gameManager;

    [SerializeField]
    private AssetManager _assetManager;
    public AssetManager AssetManager => _assetManager;

    [SerializeField]
    private InputHandler _inputHandler;
    public InputHandler InputHandler => _inputHandler;

    protected void Awake()
    {
        _gameManager  = GetComponent<GameManager>();
        _assetManager = GetComponent<AssetManager>();
        _inputHandler = GetComponent<InputHandler>();

    }

    public static Roguelike FindRoguelikeUniqueInstance()
    {
        if( _instance != null )
        {
            return _instance;

        }

        Roguelike roguelikeInstance = GameObject.FindObjectsOfType<Roguelike>()[0];

        if( roguelikeInstance == null )
        {
            roguelikeInstance = new GameObject( "Roguelike" ).AddComponent<Roguelike>();
        }

        return _instance = roguelikeInstance;

    }
}
