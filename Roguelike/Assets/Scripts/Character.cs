using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MovementController, ITurnAgent
{
    public abstract bool TurnAction();

}
