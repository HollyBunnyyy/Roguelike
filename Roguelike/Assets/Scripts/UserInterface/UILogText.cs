using TMPro;
using UnityEngine;

[RequireComponent( typeof( TMP_Text ) )]
public class UILogText : MonoBehaviour
{
    private TMP_Text _textAsset;

    protected void Awake()
    {
        _textAsset = GetComponent<TMP_Text>();
    }

    public void DisplayText( string textToDisplay )
    {
        _textAsset.text = textToDisplay;
    }
}
