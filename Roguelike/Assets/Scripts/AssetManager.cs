using UnityEngine;

public class AssetManager : MonoBehaviour
{
    // More lazy-instantiation as seen in the Roguelike singleton - this is especially important for
    // The asset manager as this is where the game sources ALL data from.

    private ItemLookupTable _itemTable { get; init; }
    public ItemLookupTable ItemLookupTable
    {
        get { return _itemTable ?? new ItemLookupTable( AssetParser.ReadTextFileContents( "/ItemTable.json" ) ); }
    }

    private CharacterLookupTable _characterTable { get; init; }
    public CharacterLookupTable CharacterLookupTable
    {
        get { return _characterTable ?? new CharacterLookupTable( AssetParser.ReadTextFileContents( "/CharacterTable.json" ) ); }
    }

    /// <summary>
    /// Attempts to get the CharacterMetaData of the given ID.
    /// </summary>
    public bool TryGetMetaData( int entityID, out CharacterMetaData characterData )
    {
        return CharacterLookupTable.TryGetID( entityID, out characterData );
    }

    /// <summary>
    /// Attempts to get the ItemMetaData of the given ID.
    /// </summary>
    public bool TryGetMetaData( int entityID, out ItemMetaData itemData )
    {
        return ItemLookupTable.TryGetID( entityID, out itemData );
    }
}
