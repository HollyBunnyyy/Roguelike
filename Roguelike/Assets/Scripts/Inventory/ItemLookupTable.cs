using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLookupTable : MonoBehaviour
{
    [SerializeField]
    private TextAsset _jsonItemTableAsset;

    private ItemLookupArray _jsonItemArray;

    protected virtual void Start()
    {
        _jsonItemArray = JsonUtility.FromJson<ItemLookupArray>( _jsonItemTableAsset.text );

        Debug.Log( _jsonItemArray.ItemTable[0].Description );

    }



}
