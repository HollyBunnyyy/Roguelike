using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVController : MonoBehaviour
{
    [SerializeField]
    private AreaMap _gridMap;

    public float CalculateEuclideanDistance( int xStartingIndex, int yStartingIndex, int xTargetIndex, int yTargetIndex )
    {
        float xNeighborDistance = Mathf.Pow( xTargetIndex - xStartingIndex, 2.0f );
        float yNeighborDistance = Mathf.Pow( yTargetIndex - yStartingIndex, 2.0f );

        return Mathf.Sqrt( xNeighborDistance + yNeighborDistance );

    }

}
