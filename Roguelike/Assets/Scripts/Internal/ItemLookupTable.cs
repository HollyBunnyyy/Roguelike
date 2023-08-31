using System.Collections.Generic;
using UnityEngine;

// JSON item data has to be mutable by default so the JSON parser can write to it, constructors are not used when they 
// do this, records as well can't use { get; init; } properties - I don't want devs or players to change metadata at runtime so 
// this class is both a lookup table and a way to convert JSON items to immutable record types of our desired MetaData.

public class ItemLookupTable : ILookupTable<ItemMetaData>
{
    private JSONItemLookupArray _jsonItemArray;

    // This just sorts the dictionary by ID's.
    private SortedDictionary<int, ItemMetaData> _itemLookupTable = new SortedDictionary<int, ItemMetaData>();

    public ItemLookupTable( TextAsset itemTableJSON )
    {
        _jsonItemArray = JsonUtility.FromJson<JSONItemLookupArray>( itemTableJSON.text );

        for( int i = 0; i < _jsonItemArray.ItemTable.Length; i++ )
        {
            JSONItemMetaData jsonItemToAdd = _jsonItemArray.ItemTable[i];

            Roguelike.Instance.AssetManager.TryGetSpriteFromPath( jsonItemToAdd.Sprite, out Sprite sprite );

            _itemLookupTable.Add( jsonItemToAdd.ID, new ItemMetaData() 
            { 
                ID          = jsonItemToAdd.ID,
                Name        = jsonItemToAdd.Name,
                Sprite      = sprite,
                Description = jsonItemToAdd.Description,
                Rarity      = jsonItemToAdd.Rarity
                         
            } );
        }
    }

    public bool HasID( int idToCheck )
    {
        return _itemLookupTable.ContainsKey( idToCheck );

    }

    public bool TryGetID( int id, out ItemMetaData itemMetaData )
    {
        itemMetaData = null;

        if( !HasID( id ) )
        {
            return false;

        }

        itemMetaData = _itemLookupTable[id];

        return true;

    }

    public IEnumerable<int> GetKeys()
    {
        return _itemLookupTable.Keys;

    }
}
