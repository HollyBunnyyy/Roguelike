using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Player : Character
{
    [SerializeField]
    private InputHandler _inputHandler;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    public override bool TurnAction()
    {
        if( Input.anyKeyDown )
        {
            if( _inputHandler.WASDAxis != Vector2.zero )
            {
                TryMoveTowardsDirection( Vector2Int.RoundToInt( _inputHandler.WASDAxis ), out Character character );

                if( character )
                {
                    character.Damage( 5.0f );

                }

                return true;

            }

            if( Input.GetKeyDown( KeyCode.Space ) )
            {
                // skip turn


                Debug.Log( Roguelike.Instance.ItemTable[0].Sprite );

                string itemPath = Roguelike.Instance.ItemTable[0].Sprite;

                AsyncOperationHandle<Sprite> requestHandler = Addressables.LoadAssetAsync<Sprite>( itemPath );

                Sprite sprite = requestHandler.WaitForCompletion();

                _spriteRenderer.sprite = sprite;

                return true;

            }

        }

        return false;

    }



}
