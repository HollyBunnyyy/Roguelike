using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AssetManager : MonoBehaviour
{
    [SerializeField]
    private TextAsset _itemTableText;

    [SerializeField]
    private TextAsset _characterTableText;

    private ItemLookupTable _itemTable;
    private CharacterLookupTable _characterTable;

    protected virtual void Awake()
    {
        if( _itemTableText == null )
        {
            _itemTableText = Resources.Load<TextAsset>( "ItemTable" );
        }

        if( _itemTableText == null )
        {
            _characterTableText = Resources.Load<TextAsset>( "CharacterTable" );
        }

        _itemTable = new ItemLookupTable( _itemTableText );
        _characterTable = new CharacterLookupTable( _characterTableText );

    }

    public bool TryGetSpriteFromPath( string filePath, out Sprite sprite )
    {
        AsyncOperationHandle<Sprite> requestHandler = Addressables.LoadAssetAsync<Sprite>( filePath );

        sprite = null;

        if( requestHandler.Status == AsyncOperationStatus.Failed )
        {
            return false;

        }

        sprite = requestHandler.WaitForCompletion();

        return true;

    }

    public bool TryGetIDMetaData( int idToValidate, out CharacterMetaData characterData )
    {
        characterData = null;

        if( !_characterTable.HasID( idToValidate ) )
        {
            return false;
        }

        characterData = _characterTable[idToValidate];

        return true;

    }

    public bool TryGetIDMetaData( int idToValidate, out ItemMetaData itemData )
    {
        itemData = null;

        if( !_itemTable.HasID( idToValidate ) )
        {
            return false;
        }

        itemData = _itemTable[idToValidate];

        return true;

    }

}
