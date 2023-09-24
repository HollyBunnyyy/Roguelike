using UnityEngine;

[RequireComponent( typeof( UILog ) )]
public class UIManager : MonoBehaviour
{
    [SerializeField]
    private UILog _eventLog;
    public UILog EventLog
    {
        get { return _eventLog != null ? _eventLog : _eventLog = GetComponent<UILog>(); }
    }

    protected void Awake()
    {
        _eventLog = GetComponent<UILog>();
    }

    protected void Start()
    {
        EventLog.WriteTextToLog( "<color=green>UI Log Initialized.</color>" );
    }
}
