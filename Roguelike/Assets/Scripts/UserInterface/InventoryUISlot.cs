using UnityEngine;
using UnityEngine.UI;

public class InventoryUISlot : MonoBehaviour
{
    [SerializeField]
    private Image _graphic;

    private int _currentItemID;
    public int CurrentItemID => _currentItemID;

    /// <summary>
    /// Attempts to set the slot's image to the given item ID.
    /// </summary>
    public bool TrySetImage( int itemID )
    {
        if( !Roguelike.Instance.ItemTable.IsIDValid( itemID, out ItemMetaData itemMetaData ) )
        {
            return false;

        }

        _graphic.sprite = AssetManager.GetSpriteFromPath( itemMetaData.Sprite );

        _currentItemID = itemID;

        return true;

    }

    /// <summary>
    /// Resets the slot's image sprite to null.
    /// </summary>
    public void ResetImage()
    {
        _graphic.sprite = null;

    }

}
