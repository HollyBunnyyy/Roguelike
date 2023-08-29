using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

// Heavy WIP, idk what I'm doing here :)
public static class AssetManager 
{

    public static Sprite GetSpriteFromPath( string filePath )
    {
        AsyncOperationHandle<Sprite> requestHandler = Addressables.LoadAssetAsync<Sprite>( filePath );

        return requestHandler.WaitForCompletion();

    }

}
