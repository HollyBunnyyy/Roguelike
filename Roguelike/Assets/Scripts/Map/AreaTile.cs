using UnityEngine;

// TODO : add a way to show items on the ground - store them ( Inventory ) and allow the player to interact with them.
// TODO : make tile meta data - consider scriptable tilebase for tilemap.

public class AreaTile
{
    public bool IsWalkable = true;

    public Entity OccupyingEntity;

    public Inventory<Item> OccupyingItems = new Inventory<Item>( 2 );

    public readonly Vector3     WorldPosition;
    public readonly Vector2Int  LocalPosition;

    private Entity _entityInventoryGraphic;

    public AreaTile( Vector3 worldPosition, Vector2Int localPosition )
    {
        this.WorldPosition = worldPosition;
        this.LocalPosition = localPosition;

        OccupyingItems.OnCollectionDirty += Test;

    }

    ~AreaTile()
    {
        OccupyingItems.OnCollectionDirty -= Test;

    }

    private void Test()
    {
        if( OccupyingItems.HasItems )
        {
            if( !_entityInventoryGraphic )
            {
                // Combine these two functions below into one method, debug for now.
                _entityInventoryGraphic = Roguelike.Instance.AssetManager.SpawnEntity( WorldPosition );

            }
            //_entityInventoryGraphic.SpriteRenderer.sprite = OccupyingItems.GetEarliestItem().ID;

            if( OccupyingItems.GetEarliestItem().ID != _entityInventoryGraphic.ID )
            {
                _entityInventoryGraphic.ID = OccupyingItems.GetEarliestItem().ID;

                Roguelike.Instance.AssetManager.TryGetMetaData( _entityInventoryGraphic.ID, out ItemMetaData itemData );

                _entityInventoryGraphic.SpriteRenderer.sprite = itemData.Sprite;

            }
        } else {
            _entityInventoryGraphic?.Disable();

        }
    }
}
