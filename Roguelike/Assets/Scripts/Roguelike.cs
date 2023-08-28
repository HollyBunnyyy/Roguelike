using UnityEngine;

// Trust me, I wouldn't use singleton patterns if I didn't have to.
public class Roguelike : MonoBehaviour
{
    [SerializeField]
    private TextAsset _itemTableJSONAsset;

    private ItemLookupTable _itemTable;

    private static Roguelike _instance = null;
    public static Roguelike Instance
    {
        get 
        { 
            if( _instance == null )
            {
                if( !FindRoguelikeUniqueInstance( out Roguelike roguelikeInstance ) )
                {
                    GameObject singletonGameObject = new GameObject()
                    {
                        name = "Roguelike"

                    };

                    _instance = singletonGameObject.AddComponent<Roguelike>();

                } else {
                    _instance = roguelikeInstance;

                }

            }

            return _instance;

        }

    }

    public ItemLookupTable ItemTable
    {
        get { return _itemTable; }
    }


    protected virtual void Awake()
    {
        _itemTableJSONAsset ??= Resources.Load<TextAsset>( "ItemTable" );

        _itemTable = new ItemLookupTable( _itemTableJSONAsset );

    }

    public static bool FindRoguelikeUniqueInstance( out Roguelike roguelikeInstance )
    {
        roguelikeInstance = GameObject.FindObjectOfType<Roguelike>();

        return roguelikeInstance;

    }

}

/*
 * 
 *             dfUnityOut = GameObject.FindObjectOfType<DaggerfallUnity>();
 * 
        private void SetupSingleton()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
            {
                if (Application.isPlaying)
                {
                    LogMessage("Multiple DaggerfallUnity instances detected in scene!", true);
                    Destroy(gameObject);
                }
            }
        }
*/