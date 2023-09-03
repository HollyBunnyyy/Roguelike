using UnityEngine;

[RequireComponent( typeof( EntityPool ) )]
public class AssetManager : MonoBehaviour
{
    [SerializeField]
    private EntityPool _entityPool;

    private ItemLookupTable _itemTable;
    public ItemLookupTable ItemTable => _itemTable ??= new ItemLookupTable( AssetParser.ReadTextFileContents( "/ItemTable.json" ));

    private CharacterLookupTable _characterTable;

    protected void Awake()
    {
        _entityPool = GetComponent<EntityPool>();

        _itemTable      ??= new ItemLookupTable( AssetParser.ReadTextFileContents( "/ItemTable.json" ) );
        _characterTable ??= new CharacterLookupTable( AssetParser.ReadTextFileContents( "/CharacterTable.json" ) );

    }

    public Entity SpawnEntity( Vector3 positionToSpawn, bool isEnabledOnSpawn = true )
    {
        Entity entityPawn = _entityPool.GetNext();
        entityPawn.transform.position = positionToSpawn;
        entityPawn.gameObject.SetActive( isEnabledOnSpawn );

        return entityPawn;
    }

    /// <summary>
    /// Attempts to get the CharacterMetaData of the given ID.
    /// </summary>
    public bool TryGetMetaData( int idToValidate, out CharacterMetaData characterData )
    {
        return _characterTable.TryGetID( idToValidate, out characterData );
    }

    /// <summary>
    /// Attempts to get the ItemMetaData of the given ID.
    /// </summary>
    public bool TryGetMetaData( int idToValidate, out ItemMetaData itemData )
    {
        return ItemTable.TryGetID( idToValidate, out itemData );
    }
}
