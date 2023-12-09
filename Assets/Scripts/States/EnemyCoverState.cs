using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Sets the cover state when the user has positioned itself behind objects
public class EnemyCoverState : EnemyBaseState
{
    public override void EnterState(PlayerTrackerAI enemy) 
    {
        enemy.GetComponent<Gun>().appropriateStateActive = false;
    }

    public override void UpdateState(PlayerTrackerAI enemy)
    {
        //If the enemy can see the enemy
        if (enemy.transform.Find("Viewport").GetComponent<View>().canBeSeen == true)
        {
            //If its weapon has been destroyed, it will keep retreating using another cover state
            if (enemy.transform.Find("Gun") == null)
            {
                enemy.currentState = enemy.moveToCoverState;
                enemy.moveToCoverState.EnterState(enemy);
            }
            //If it is equipped with a gun, it will shoot
            else
            {
                enemy.currentState = enemy.shootState;
                enemy.shootState.EnterState(enemy);
            }
            
        }

        //Randomly changes the state to attack and push the enemy so its constantly moving around
        int randomOutcome = Random.Range(0, 2000);

        if(randomOutcome == 0 && enemy.transform.Find("Gun") != null)
        {
            enemy.currentState = enemy.attackPositionState;
            enemy.attackPositionState.EnterState(enemy);
        }
    }

}
