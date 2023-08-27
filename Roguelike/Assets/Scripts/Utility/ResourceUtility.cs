using UnityEngine;

public static class ResourceUtility 
{
    public static bool TryLoadPNG( string filePath, out Texture2D texture )
    {
        texture = null;

        if( !System.IO.File.Exists( filePath ) )
        {
            return false;

        }

        byte[] fileData = System.IO.File.ReadAllBytes( filePath );

        texture = new Texture2D( 16, 16 );
        texture.LoadImage( fileData );

        return true;


    }

}
