using System.Collections.Generic;
using UnityEngine;

public class UILogTextPool : MonoBehaviour
{
    [SerializeField]
    private int _logLimit = 20;

    [SerializeField]
    private RectTransform _contentArea;

    [SerializeField]
    private UILogText _textPrefab;

    private LinkedList<UILogText> _textAssetPool = new LinkedList<UILogText>();

    protected void Awake()
    {
        for( int i = 0; i < _logLimit; i++ )
        {
            UILogText textToAdd = Instantiate( _textPrefab, _contentArea.transform );
            textToAdd.transform.gameObject.SetActive( false );
            _textAssetPool.AddLast( textToAdd );          
        }
    }

    public UILogText GetNextTextAsset()
    {
        LinkedListNode<UILogText> textAssetToCycle = _textAssetPool.First;

        _textAssetPool.RemoveFirst();
        _textAssetPool.AddLast( textAssetToCycle );

        textAssetToCycle.Value.gameObject.SetActive( true );
        textAssetToCycle.Value.transform.SetAsLastSibling();

        return textAssetToCycle.Value;

    }
}
