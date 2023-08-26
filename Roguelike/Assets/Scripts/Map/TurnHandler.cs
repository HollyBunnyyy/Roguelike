using System.Collections.Generic;
using UnityEngine;

public class TurnHandler : MonoBehaviour
{
    private LinkedList<Character> TurnAgents = new LinkedList<Character>();

    public Character CurrentAgent => TurnAgents.First.Value;

    public int Count => TurnAgents.Count;

    protected virtual void Update()
    {
        if( Count <= 0 )
        {
            return;

        }

        if( !CurrentAgent )
        {
            TurnAgents.RemoveFirst();

            GetNextAgent();

            return;

        }

        if( CurrentAgent.TurnAction() )
        {
            GetNextAgent();

        }

    }

    public Character GetNextAgent()
    {
        LinkedListNode<Character> currentAgent = TurnAgents.First;

        TurnAgents.RemoveFirst();
        TurnAgents.AddLast( currentAgent );

        return TurnAgents.First.Value;

    }

    public void AddAgent( Character agentToAdd )
    {
        TurnAgents.AddLast( agentToAdd );

    }

    public void RemoveAgent( Character agentToRemove )
    {
        if( TurnAgents.Contains( agentToRemove ) )
        {
            TurnAgents.Remove( agentToRemove );

        }

    }

}
