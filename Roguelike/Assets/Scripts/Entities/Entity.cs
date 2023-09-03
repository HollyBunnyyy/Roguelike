using UnityEngine;

// TODO : Add logic to set the sprite of the gameobject when the ID is changed.

[RequireComponent( typeof( SpriteRenderer ) )]
public class Entity : MovementController
{
    public int ID;

    [HideInInspector]
    public SpriteRenderer SpriteRenderer;

    protected void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }
}
