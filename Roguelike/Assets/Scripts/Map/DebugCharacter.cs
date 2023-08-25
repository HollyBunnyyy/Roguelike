using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCharacter : Character
{
    [SerializeField]
    private bool _shouldMove = true;

    private Direction2D _currentDirection = Direction2D.Right;

    public override bool TurnAction()
    {
        if( _shouldMove )
        {
            TryMoveTowardsDirection( _currentDirection = Direction2D.Opposite( _currentDirection ) );


        }

        return true;

    }


}
