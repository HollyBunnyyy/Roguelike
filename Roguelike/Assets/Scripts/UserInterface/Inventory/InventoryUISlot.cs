using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUISlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private Image _graphic;

    [SerializeField]
    public TMP_Text _stackAmount;

    [SerializeField]
    private RectTransform _slot;

    private int _currentItemID;
    public int CurrentItemID => _currentItemID;

    /// <summary>
    /// Attempts to set the slot's image to the given item ID.
    /// </summary>
    public bool TrySetImage( int itemID )
    {
        if( !Roguelike.Instance.AssetManager.TryGetMetaData( itemID, out ItemMetaData itemMetaData ) )
        {
            return false;

        }

        _currentItemID = itemID;
        _graphic.sprite = itemMetaData.Sprite;

        return true;

    }

    /// <summary>
    /// Resets the slot's image sprite to null.
    /// </summary>
    public void ResetImage()
    {
        _graphic.sprite = null;

    }

    public void OnBeginDrag( PointerEventData eventData )
    {
        _graphic.transform.SetParent( transform.root );

    }

    public void OnDrag( PointerEventData eventData )
    {
        _graphic.transform.position = Input.mousePosition;

    }

    public void OnEndDrag( PointerEventData eventData )
    {
        _graphic.transform.SetParent( _slot );
        _graphic.transform.position = _slot.transform.position;

    }

}
