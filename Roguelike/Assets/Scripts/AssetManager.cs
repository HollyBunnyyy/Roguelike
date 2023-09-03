using UnityEngine;

public class AssetManager : MonoBehaviour
{
    private ItemLookupTable _itemTable;
    private CharacterLookupTable _characterTable;

    public void Awake()
    {
        _itemTable      ??= new ItemLookupTable( AssetParser.ReadTextFileContents( "/ItemTable.json" ) );
        _characterTable ??= new CharacterLookupTable( AssetParser.ReadTextFileContents( "/CharacterTable.json" ) );

    }

    /// <summary>
    /// Attempts to get the CharacterMetaData of the given ID.
    /// </summary>
    public bool TryGetMetaData( int entityID, out CharacterMetaData characterData )
    {
        return _characterTable.TryGetID( entityID, out characterData );
    }

    /// <summary>
    /// Attempts to get the ItemMetaData of the given ID.
    /// </summary>
    public bool TryGetMetaData( int entityID, out ItemMetaData itemData )
    {
        return _itemTable.TryGetID( entityID, out itemData );
    }
}
