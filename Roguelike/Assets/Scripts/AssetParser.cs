using UnityEngine;

public static class AssetParser
{
    public static string GetStreamingAssetFilePath( string fileName )
    {
        return string.Concat( Application.streamingAssetsPath, fileName );

    }

    public static bool DoesFilePathExist( string filePath )
    {
        if( !System.IO.File.Exists( filePath ) )
        {
            Debug.LogWarning( filePath + " does not exist in the given StreamingAsset's path." );

            return false;

        }

        return true;

    }

    public static string ReadTextFileContents( string filePath )
    {
        string completeFilePath = GetStreamingAssetFilePath( filePath );

        if( !DoesFilePathExist( completeFilePath ) )
        {
            return string.Empty;
        }

        return System.IO.File.ReadAllText( completeFilePath );

    }

    public static bool TryGetTextureFromPath( string filePath, out Texture2D textureData )
    {
        textureData = null;

        string completeFilePath = GetStreamingAssetFilePath( filePath );

        if( !DoesFilePathExist( completeFilePath ) )
        {
            return false;
        }

        byte[] rawImageData = System.IO.File.ReadAllBytes( completeFilePath );

        textureData = new Texture2D( 24, 24 );
        textureData.filterMode = FilterMode.Point;
        textureData.LoadImage( rawImageData );
        
        return true;

    }

    public static bool TryGetSpriteFromPath( string filePath, out Sprite spriteData, float pixelsPerUnit = 24.0f )
    {
        spriteData = null;

        if( !TryGetTextureFromPath( filePath, out Texture2D textureData ) )
        {
            return false;
        }
        
        spriteData = Sprite.Create( textureData, new Rect( 0.0f, 0.0f, textureData.width, textureData.height ), new Vector2( 0.5f, 0.5f ), pixelsPerUnit );

        return true;

    }


}
