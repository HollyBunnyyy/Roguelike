using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCharacter : Character
{
    private Direction2D _currentDirection = Direction2D.Right;

    public override bool TurnAction()
    {
        TryMoveTowardsDirection( _currentDirection = Direction2D.Opposite( _currentDirection ) );

        return true;

    }


}
