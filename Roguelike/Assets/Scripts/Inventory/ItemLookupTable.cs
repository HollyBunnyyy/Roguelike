using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemLookupTable : MonoBehaviour
{
    [SerializeField]
    private TextAsset _jsonItemTableAsset;

    private ItemLookupArray _jsonItemArray;

    private Dictionary<int, ItemMetaData> _itemLookuptable = new Dictionary<int, ItemMetaData>();

    public ItemMetaData this[int itemId]
    {
        get => _itemLookuptable[itemId];
        set => _itemLookuptable[itemId] = value;

    }

    protected virtual void Awake()
    {
        _jsonItemArray = JsonUtility.FromJson<ItemLookupArray>( _jsonItemTableAsset.text );

        _itemLookuptable = _jsonItemArray.ItemTable.ToDictionary( key => key.ID, ItemMetaData => ItemMetaData );

        Debug.Log( this[1].Description );



    }



}
