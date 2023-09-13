using UnityEngine;

public static class DiceRoller
{
    /// <summary>
    /// Rolls a random dice between 1 and the given sides.
    /// </summary>
    public static int Roll( int sides )
    {
        return Random.Range( 1, sides + 1 );
    }

    /// <summary>
    /// Rolls a random dice between 1 and the given sides - returns true if it's at or greater than the minimum roll.
    /// </summary>
    public static bool RollForSuccess( int sides, int minimumToRollForSuccess, out int result )
    {
        result = Roll( sides );

        return result >= minimumToRollForSuccess;
    }
}
