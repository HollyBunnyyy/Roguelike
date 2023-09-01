using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AssetManager : MonoBehaviour
{
    [SerializeField]
    private TextAsset _itemTableText;

    [SerializeField]
    private TextAsset _characterTableText;

    [SerializeField]
    private EntityPawnPool _entityPawnPool;

    private ItemLookupTable _itemTable;
    private CharacterLookupTable _characterTable;

    private Dictionary<string, AsyncOperationHandle<Sprite>> _asyncHandles = new Dictionary<string, AsyncOperationHandle<Sprite>>();

    protected virtual void Awake()
    {
        // I want to use the ternary operator ??= for assignment so bad... unfortunately unity overrides it :(
        if( !_itemTableText )
        {
            _itemTableText = Resources.Load<TextAsset>( "ItemTable" );
        }

        if( !_characterTableText )
        {
            _characterTableText = Resources.Load<TextAsset>( "CharacterTable" );
        }

        _itemTable = new ItemLookupTable( _itemTableText );
        _characterTable = new CharacterLookupTable( _characterTableText );

    }

    /// <summary>
    /// Attempts to get the sprite from the given filePath.
    /// </summary>
    public bool TryGetSpriteFromPath( string filePath, out Sprite sprite )
    {
        sprite = null;

        if( !_asyncHandles.ContainsKey( filePath ) )
        {
            AsyncOperationHandle<Sprite> requestHandler = Addressables.LoadAssetAsync<Sprite>( filePath );

            if( requestHandler.Status == AsyncOperationStatus.Failed )
            {
                return false;
            }

            _asyncHandles.Add( filePath, requestHandler );

        }

        sprite = _asyncHandles[filePath].WaitForCompletion();

        return true;

    }

    public EntityPawn GetBlankPawn( Vector3 positionToSpawn )
    {
        EntityPawn entityPawn = _entityPawnPool.GetNext();
        entityPawn.transform.position = positionToSpawn;   

        return entityPawn;
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

    protected void OnDisable()
    {       
        // Unloads all data from disk.
        // I'm sure addressables does it itself, but I'm always worried about leaks.

        foreach( string asyncHandle in _asyncHandles.Keys )
        {
            Addressables.Release( _asyncHandles[asyncHandle] );
        }
    }
}
