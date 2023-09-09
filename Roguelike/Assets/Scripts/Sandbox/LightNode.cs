using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LightNode : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer;
    public TMP_Text Text;

    public bool IsWall = false;

    public Vector3 WorldPosition;
    public Vector2Int LocalPosition;

    public void Show()
    {
        SpriteRenderer.color = Color.white;
    }

    public void Hide()
    {
        SpriteRenderer.color = Color.grey;
    }
}
