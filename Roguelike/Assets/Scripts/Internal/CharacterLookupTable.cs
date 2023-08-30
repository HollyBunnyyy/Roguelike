using System.Collections.Generic;
using UnityEngine;

public class CharacterLookupTable : ILookupTable<CharacterMetaData>
{
    private JSONCharacterLookupArray _jsonCharacterTable;

    private SortedDictionary<int, CharacterMetaData> _characterLookupTable = new SortedDictionary<int, CharacterMetaData>();

    public CharacterLookupTable( TextAsset characterTableJSON )
    {
        _jsonCharacterTable = JsonUtility.FromJson<JSONCharacterLookupArray>( characterTableJSON.text );

        for( int i = 0; i < _jsonCharacterTable.CharacterTable.Length; i++ )
        {
            JSONCharacterMetaData jsonItemToAdd = _jsonCharacterTable.CharacterTable[i];

            _characterLookupTable.Add( jsonItemToAdd.ID, new CharacterMetaData() 
            {
                ID          = jsonItemToAdd.ID,
                Name        = jsonItemToAdd.Name,
                Sprite      = jsonItemToAdd.Sprite,
                Description = jsonItemToAdd.Description,
                Heart       = jsonItemToAdd.Heart,
                Ego         = jsonItemToAdd.Ego,
                Dream       = jsonItemToAdd.Dream,
                Flow        = jsonItemToAdd.Flow,
                Pain        = jsonItemToAdd.Pain

            } );
        }
    }

    public bool HasID( int idToCheck )
    {
        return _characterLookupTable.ContainsKey( idToCheck );

    }

    public bool TryGetID( int id, out CharacterMetaData characterMetaData )
    {
        characterMetaData = null;

        if( !HasID( id ) )
        {
            return false;

        }

        characterMetaData = _characterLookupTable[id];

        return true;

    }
}
