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
        _itemTable      = new ItemLookupTable( _itemTableText );
        _characterTable = new CharacterLookupTable( _characterTableText );

    }

    /// <summary>
    /// Attempts to get the sprite from the given filePath.
    /// </summary>
    public bool TryGetSpriteFromPath( string filePath, out Sprite sprite )
    {
        AsyncOperationHandle<Sprite> requestHandler = Addressables.LoadAssetAsync<Sprite>( filePath );

        sprite = null;

        if( requestHandler.Status == AsyncOperationStatus.Failed )
        {
            return false;

        }

        sprite = requestHandler.WaitForCompletion();

        Addressables.ReleaseInstance( requestHandler );

        return true;

    }

    /// <summary>
    /// Attempts to get the CharacterMetaData of the given ID.
    /// </summary>
    public bool TryGetIDMetaData( int idToValidate, out CharacterMetaData characterData )
    {
        return _characterTable.TryGetID( idToValidate, out characterData );

    }

    /// <summary>
    /// Attempts to get the ItemMetaData of the given ID.
    /// </summary>
    public bool TryGetIDMetaData( int idToValidate, out ItemMetaData itemData )
    {
        return _itemTable.TryGetID( idToValidate, out itemData );

    }
}
