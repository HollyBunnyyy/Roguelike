using UnityEngine;

// TODO : add a way to show items on the ground - store them ( Inventory ) and allow the player to interact with them.
// TODO : make tile meta data - consider scriptable tilebase for tilemap.

public class AreaTile
{
    public bool IsWalkable = true;

    public Entity OccupyingEntity = null;

    private EntityPawn _entityPawn;

    public Inventory<Item> OccupyingItems = new Inventory<Item>( 2 );

    public readonly Vector3     WorldPosition;
    public readonly Vector2Int  LocalPosition;

    public AreaTile( Vector3 worldPosition, Vector2Int localPosition )
    {
        this.WorldPosition = worldPosition;
        this.LocalPosition = localPosition;

        OccupyingItems.OnCollectionDirty += FillTile;

    }

    // I'm sure this can be handled better, for now it's debug.
    private void FillTile()
    {
        if( OccupyingItems.HasItems )
        {
            if( !_entityPawn )
            {
                _entityPawn = Roguelike.Instance.AssetManager.GetBlankPawn( WorldPosition );

                Roguelike.Instance.AssetManager.TryGetSpriteFromPath( "Debug_Items_2.png", out Sprite sprite );

                _entityPawn.SetEntityToDisplay( sprite );

            }

            return;

        }

        _entityPawn?.Disable();

    }
}
