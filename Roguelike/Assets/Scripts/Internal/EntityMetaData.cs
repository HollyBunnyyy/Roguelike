using UnityEngine;

public record EntityMetaData
{
    public int ID               { get; init; }
    public string Name          { get; init; }
    public Sprite Sprite        { get; init; }
    public string Description   { get; init; }

}
