using System.Collections.Generic;
using UnityEngine;

public class TurnHandler : MonoBehaviour
{
    private LinkedList<ITurnAgent> TurnAgents = new LinkedList<ITurnAgent>();

    public ITurnAgent CurrentAgent => TurnAgents.First.Value;

    public int Count => TurnAgents.Count;

    protected virtual void Update()
    {
        // == operator doesn't work here for some reason.
        if( CurrentAgent.Equals( null ) )
        {
            TurnAgents.RemoveFirst();

            return;

        }

        if( CurrentAgent.TurnAction() )
        {
            GetNextAgent();

        }

    }

    public ITurnAgent GetNextAgent()
    {
        if( Count <= 0 )
        {
            return null;

        }

        LinkedListNode<ITurnAgent> currentAgent = TurnAgents.First;

        TurnAgents.RemoveFirst();
        TurnAgents.AddLast( currentAgent );

        return TurnAgents.First.Value;

    }

    public void AddAgent( ITurnAgent agentToAdd )
    {
        TurnAgents.AddLast( agentToAdd );

    }

    public void RemoveAgent( ITurnAgent agentToRemove )
    {
        if( TurnAgents.Contains( agentToRemove ) )
        {
            TurnAgents.Remove( agentToRemove );

        }
    }
}
