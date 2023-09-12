using System.Collections.Generic;
using UnityEngine;

public class CharacterLookupTable : ILookupTable<CharacterMetaData>
{
    private JSONCharacterLookupArray _jsonCharacterTable;

    private SortedDictionary<int, CharacterMetaData> _characterLookupTable = new SortedDictionary<int, CharacterMetaData>();

    public CharacterLookupTable( string RawJSONText )
    {
        _jsonCharacterTable = JsonUtility.FromJson<JSONCharacterLookupArray>( RawJSONText );

        for( int i = 0; i < _jsonCharacterTable.CharacterTable.Length; i++ )
        {
            JSONCharacterMetaData jsonItemToAdd = _jsonCharacterTable.CharacterTable[i];

            if( !AssetParser.TryGetSpriteFromPath( "/Textures/Characters/" + jsonItemToAdd.Sprite, out Sprite spriteData ) )
            {               
                Debug.LogWarning( "Sprite data from JSON character meta data not found." );

                spriteData = Resources.Load<Sprite>( "null" );

            }

            spriteData.name = jsonItemToAdd.Sprite;

            _characterLookupTable.Add( jsonItemToAdd.ID, new CharacterMetaData() 
            {
                ID          = jsonItemToAdd.ID,
                Name        = jsonItemToAdd.Name,
                Sprite      = spriteData,
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

    public int Count()
    {
        return _characterLookupTable.Count;

    }
}
