using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStateData", menuName = "Game/EnemyStateData")]
public class EnemyStateManager : ScriptableObject
{
    public Dictionary<int, bool> enemyStates = new Dictionary<int, bool>();

    public void SetEnemyDead(int enemyID)
    {
        if (!enemyStates.ContainsKey(enemyID))
            enemyStates.Add(enemyID, true);
        else
            enemyStates[enemyID] = true;
    }

    public bool IsEnemyDead(int enemyID)
    {
        return enemyStates.ContainsKey(enemyID) && enemyStates[enemyID];
    }
}
