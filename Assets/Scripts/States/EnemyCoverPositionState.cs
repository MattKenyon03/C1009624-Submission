using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Selects position of the cover and moves the enemy until it is covered by obstacles
public class EnemyCoverPositionState : EnemyBaseState
{
    public List<GameObject> viewNodeList = new();
    public int randomInt;

    public override void EnterState(PlayerTrackerAI enemy)
    {
        //Stops the enemy from being able to shoot
        enemy.GetComponent<Gun>().appropriateStateActive = false;
        //Checks if the user can be seen from the raycast areas
        GameObject[] nodes = GameObject.FindGameObjectsWithTag("Raycast Areas");
        //If it cant be seen, it is added to a list
        foreach (GameObject node in nodes)
        {
            if (node.GetComponent<View>().canBeSeen == false)
            {
                viewNodeList.Add(node);
            }
        }

        if (viewNodeList.Count == 0)
        {

        }
        else
        {
            //A random node from the list is chosen as the destination
            randomInt = Random.Range(0, viewNodeList.Count);
            GameObject chosenNode = viewNodeList[randomInt];
            enemy.destination = chosenNode;
        }
    }

    //The player tracks this area until it cannot see the enemy, which works as a cover
    public override void UpdateState(PlayerTrackerAI enemy)
    {
        if (enemy.transform.Find("Viewport").GetComponent<View>().canBeSeen == false)
        {
            enemy.currentState = enemy.coverState;
            enemy.coverState.EnterState(enemy);
        }
    }
}
