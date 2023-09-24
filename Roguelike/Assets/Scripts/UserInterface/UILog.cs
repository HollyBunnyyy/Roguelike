public class UILog : UILogTextPool
{
    public void WriteTextToLog( string textToWrite )
    {
        UILogText textAsset = GetNextTextAsset();
        textAsset.DisplayText( textToWrite );
    }
}
