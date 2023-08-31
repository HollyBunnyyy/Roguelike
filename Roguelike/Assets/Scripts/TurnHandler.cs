using System.Collections.Generic;
using UnityEngine;

public class TurnHandler : MonoBehaviour
{
    private LinkedList<TurnAgent> TurnAgents = new LinkedList<TurnAgent>();

    public TurnAgent CurrentAgent => TurnAgents.First.Value;

    public int Count => TurnAgents.Count;

    protected virtual void Update()
    {
        if( !CurrentAgent )
        {
            TurnAgents.RemoveFirst();

            return;

        }

        if( CurrentAgent.TurnAction() )
        {
            GetNextAgent();

        }

    }

    public TurnAgent GetNextAgent()
    {
        if( Count <= 0 )
        {
            return null;

        }

        LinkedListNode<TurnAgent> currentAgent = TurnAgents.First;

        TurnAgents.RemoveFirst();
        TurnAgents.AddLast( currentAgent );

        return TurnAgents.First.Value;

    }

    public void AddAgent( TurnAgent agentToAdd )
    {
        TurnAgents.AddLast( agentToAdd );

    }

    public void RemoveAgent( TurnAgent agentToRemove )
    {
        if( TurnAgents.Contains( agentToRemove ) )
        {
            TurnAgents.Remove( agentToRemove );

        }
    }
}
