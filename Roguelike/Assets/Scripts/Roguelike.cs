using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

// All components use lazy-instantiation incase something is needed BEFORE the awake method runs.
// This is very common and anything that uses object construction or delegates will need data
// Before unity runs its main Awake method.

[RequireComponent( typeof( GameManager ), typeof( AssetManager ), typeof( InputHandler ) )]
public class Roguelike : MonoBehaviour
{
    private static Roguelike _instance = null;
    public static Roguelike Instance => FindRoguelikeUniqueInstance();

    [SerializeField]
    private GameManager _gameManager;
    public GameManager GameManager
    {
        get { return _gameManager != null ? _gameManager : _gameManager = GetComponent<GameManager>(); }
    }

    [SerializeField]
    private AssetManager _assetManager;
    public AssetManager AssetManager
    {
        get { return _assetManager != null ? _assetManager : _assetManager = GetComponent<AssetManager>(); }
    }

    [SerializeField]
    private InputHandler _inputHandler;
    public InputHandler InputHandler
    {
        get { return _inputHandler != null ? _inputHandler : _inputHandler = GetComponent<InputHandler>(); }
    }

    [SerializeField]
    private UIManager _uiManager;
    public UIManager UIManager
    {
        get { return _uiManager != null ? _uiManager : _uiManager = Instance.GetOrAddComponent<UIManager>(); }
    }

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

        Roguelike roguelikeInstance = GameObject.FindObjectsOfType<Roguelike>().FirstOrDefault();

        if( roguelikeInstance == null )
        {
            roguelikeInstance = new GameObject( "Roguelike" ).AddComponent<Roguelike>();
        }

        return _instance = roguelikeInstance;

    }
}
