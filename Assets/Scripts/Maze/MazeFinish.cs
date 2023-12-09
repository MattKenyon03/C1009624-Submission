using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeFinish : MonoBehaviour
{
    public GameObject rooms, maze, character, thisDoor, otherDoor, thisMapDoor, otherMapDoor, pauseMenu, mapObjects, map;
    public Vector2 currentPos, translationVector;
    public string doorSide, oppositeDoorSide;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        pauseMenu.SetActive(true);
        mapObjects.SetActive(true);

        //Inverts the gravity if it touches an anti-gravity area
        if (collision.name == "Anti-Grav")
        {
            GetComponent<Rigidbody2D>().gravityScale *= -1;
        }

        if (collision.name == "Finish")
        {
            rooms.SetActive(true);
            currentPos = character.GetComponent<RoomInstantiation>().currentPos;

            //Checks what door was being unlocked
            if (Unlock.collisionName.Contains("Right"))
            {
                doorSide = "Right";
                oppositeDoorSide = "Left";
                translationVector = new Vector2(1, 0);
            }

            if (Unlock.collisionName.Contains("Left"))
            {
                doorSide = "Left";
                oppositeDoorSide = "Right";
                translationVector = new Vector2(-1, 0);
            }

            if (Unlock.collisionName.Contains("Top"))
            {
                doorSide = "Top";
                oppositeDoorSide = "Bottom";
                translationVector = new Vector2(0, 1);
            }

            if (Unlock.collisionName.Contains("Bottom"))
            {
                doorSide = "Bottom";
                oppositeDoorSide = "Top";
                translationVector = new Vector2(0, -1);
            }

            //Finds the door that was being interacted with and disables it in the map and the scene
            thisDoor = GameObject.Find("Rooms/" + currentPos + "/Doors Grid/" + doorSide + " Doorway");
            thisMapDoor = map.transform.Find(currentPos + "/" + doorSide + " Door").gameObject;

            thisDoor.SetActive(false);
            thisMapDoor.SetActive(false);

            //Finds the coordinates of the other door
            Vector2 newPos = currentPos + translationVector;

            //If this other room has already been generated, its locked door will also be deactivated so the player can get through
            if (character.GetComponent<RoomInstantiation>().coordinates.Contains(newPos))
            {
                otherDoor = GameObject.Find("Rooms/" + newPos + "/Doors Grid/" + oppositeDoorSide + " Doorway");
                otherDoor.SetActive(false);

                otherMapDoor = map.transform.Find(newPos + "/" + oppositeDoorSide + " Door").gameObject;
                otherMapDoor.SetActive(false);
            }

            //The maze menu is swapped out for the in game menu
            maze.SetActive(false);
            pauseMenu.SetActive(false);
            mapObjects.SetActive(false);
        }
    }
}
