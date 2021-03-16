using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleetController : MonoBehaviour
{
    public GameObject topAlien;
    public GameObject midAlien;
    public GameObject botAlien;

    public int testValue = 19;

    private const int alienRows = 5;
    private const int alienColumns = 11;
    private const int alienRevealDelay = 2;
    private const float xSpeed = 0.33f;
    private const float ySpeed = 0.5f;

    private FleetState state;
    private FleetDirection direction;
    private GameObject[] fleet = new GameObject[alienRows * alienColumns];
    
    private int fleetIndex = 0;
    private int fleetRevealCounter = 0;
    private int pauseCounter;

    private float minX = 0;
    private float maxX = 0;
    private float minY = 0;

    private enum FleetState {
        InitialiseFleet,
        RevealFleet,
        MoveFleet,
        PauseFleet,
        FleetLanded
    }

    private enum FleetDirection {
        LeftToRight,
        DownRight,
        DownLeft,
        RightToLeft
    }
    
    void Start()
    {
        state = FleetState.InitialiseFleet;
        direction = FleetDirection.LeftToRight;
    }

    void Update()
    {
        switch (state) {
            case FleetState.InitialiseFleet:
                initialiseFleet();
                break;

            case FleetState.RevealFleet:
                revealFleet();
                break;

            case FleetState.MoveFleet:
                moveFleet();
                break;

            case FleetState.PauseFleet:
                pauseFleet();
                break;

            case FleetState.FleetLanded:
                break;
        }   
    }

    private void initialiseFleet() {
        int objectIndex = 0;
        for (float x = 0; x < alienColumns; x += 1) {
            float alienX = (x * 1.2f) - 9;
            GameObject top  = Instantiate(topAlien, new Vector2(alienX, 5), new Quaternion(0, 0, 0, 0));
            GameObject mid1 = Instantiate(midAlien, new Vector2(alienX, 4), new Quaternion(0, 0, 0, 0));
            GameObject mid2 = Instantiate(midAlien, new Vector2(alienX, 3), new Quaternion(0, 0, 0, 0));            
            GameObject bot1 = Instantiate(botAlien, new Vector2(alienX, 2), new Quaternion(0, 0, 0, 0));
            GameObject bot2 = Instantiate(botAlien, new Vector2(alienX, 1), new Quaternion(0, 0, 0, 0));

            fleet[objectIndex] = bot2;
            fleet[objectIndex + alienColumns] = bot1;
            fleet[objectIndex + (alienColumns * 2)] = mid2;
            fleet[objectIndex + (alienColumns * 3)] = mid1;
            fleet[objectIndex + (alienColumns * 4)] = top;

            objectIndex++;
        }

        for (int ix = 0; ix < fleet.Length; ix++) {
            fleet[ix].SetActive(false);
        }

        fleetIndex = 0;
        state = FleetState.RevealFleet;
        GameManager.log("Fleet initialised");
    }

    private void revealFleet() {
        fleetRevealCounter++;
        if (fleetRevealCounter < alienRevealDelay) {
            return;
        }

        fleetRevealCounter = 0;
        fleet[fleetIndex].SetActive(true);

        fleetIndex++;
        if (fleetIndex == fleet.Length) {
            fleetIndex = 0;
            state = FleetState.MoveFleet;
        }        
    }

    private void moveFleet() {
        GameObject alien = fleet[fleetIndex];
        float currentX = alien.transform.position.x;
        float currentY = alien.transform.position.y;

        switch (direction) {
            case FleetDirection.LeftToRight:                
                alien.transform.position = new Vector2(currentX + xSpeed, currentY);
                break;

            case FleetDirection.DownLeft:
            case FleetDirection.DownRight:
                alien.transform.position = new Vector2(currentX, currentY - ySpeed);
                break;

            case FleetDirection.RightToLeft:
                alien.transform.position = new Vector2(currentX - xSpeed, currentY);
                break;
        }

        if (alien.transform.position.x < minX) {
            minX = alien.transform.position.x;
        }

        if (alien.transform.position.x > maxX) {
            maxX = alien.transform.position.x;
        }

        if (alien.transform.position.y < minY) {
            minY = alien.transform.position.y;
        }

        fleetIndex++;
        if (fleetIndex == fleet.Length) {
            fleetIndex = 0;

            if (changeDirection()) {
                switch (direction) {
                    case FleetDirection.LeftToRight:
                        direction = FleetDirection.DownRight;
                        break;                    

                    case FleetDirection.DownLeft:
                        direction = FleetDirection.LeftToRight;
                        break;

                    case FleetDirection.DownRight:
                        direction = FleetDirection.RightToLeft;
                        break;

                    case FleetDirection.RightToLeft:
                        direction = FleetDirection.DownLeft;
                        break;                    
                }        
                if (minY < -3) {
                    state = FleetState.FleetLanded;
                }

                minX = 0;
                maxX = 0;
                minY = 0;                
            }

            if (state != FleetState.FleetLanded) {
                pauseCounter = 16;
                state = FleetState.PauseFleet;
            }
        }
    }

    private void pauseFleet() {
        pauseCounter--;
        if (pauseCounter == 0) {
            state = FleetState.MoveFleet;
        }
    }

    private bool changeDirection() {
        if (direction == FleetDirection.DownLeft || direction == FleetDirection.DownRight) {
            return true;
        }

        if (direction == FleetDirection.LeftToRight && maxX > 8) {
            return true;
        }

        if (direction == FleetDirection.RightToLeft && minX < -8) {
            return true;
        }

        return false;
    }
}
