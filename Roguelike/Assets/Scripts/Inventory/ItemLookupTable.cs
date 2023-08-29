using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemLookupTable
{
    private ItemLookupArray _jsonItemArray;

    private Dictionary<int, ItemMetaData> _itemLookuptable = new Dictionary<int, ItemMetaData>();

    public ItemLookupTable( TextAsset itemTableJSON )
    {
        _jsonItemArray = JsonUtility.FromJson<ItemLookupArray>( itemTableJSON.text );

        _itemLookuptable = _jsonItemArray.ItemTable.ToDictionary( key => key.ID, ItemMetaData => ItemMetaData );

    }

    public ItemMetaData this[int itemId]
    {
        get => _itemLookuptable[itemId];

    }

    public bool IsIDValid( int idToValidate, out ItemMetaData itemdata )
    {
        itemdata = null;

        if( !_itemLookuptable.ContainsKey( idToValidate ) )
        {
            return false;

        }

        itemdata = _itemLookuptable[idToValidate];

        return true;

    }

}
