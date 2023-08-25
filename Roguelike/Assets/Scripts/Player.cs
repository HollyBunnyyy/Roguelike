using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField]
    private InputHandler _inputHandler;

    protected virtual void Update()
    {
        if( Input.anyKeyDown )
        {
            if( _inputHandler.WASDAxis != Vector2.zero )
            {
                TryMoveTowardsDirection( Vector2Int.RoundToInt( _inputHandler.WASDAxis ) );

            }

        }

    }


}
