using UnityEngine;
using UnityEngine.UI;

public class InventoryUISlot : MonoBehaviour
{
    [SerializeField]
    private Image _graphic;

    [SerializeField]
    private int _currentItemID;
    public int CurrentItemID => _currentItemID;

    public bool TrySetImage( int itemID )
    {
        if( !Roguelike.Instance.ItemTable.IsIDValid( itemID, out ItemMetaData itemMetaData ) )
        {
            return false;

        }

        _graphic.sprite = AssetManager.GetSpriteFromPath( itemMetaData.Sprite );

        return true;

    }

    public void ClearImage()
    {
        _graphic.sprite = null;

    }

}
