using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FleetDirection {
    LeftToRight,
    DownRight,
    DownLeft,
    RightToLeft
}

public class FleetController : MonoBehaviour {
    public GameObject topAlienPrefab;
    public GameObject midAlienPrefab;
    public GameObject botAlienPrefab;
    public GameObject bombPrefab;

    private const int alienRows = 5;
    private const int alienColumns = 11;
    private const int alienRevealDelay = 2;

    private FleetState state;
    private FleetDirection direction;
    private Alien[] fleet = new Alien[alienRows * alienColumns];

    private int fleetIndex = 0;
    private int fleetRevealCounter = 0;
    private int pauseCounter;
    private int fleetDelayCounter;

    private float minX = 0;
    private float maxX = 0;
    private float minY = 0;

    private enum FleetState {
        InitialiseFleet,
        RevealFleet,
        MoveFleet,
        MoveFleetDelay,
        PauseBetweenFleets,
        FleetLanded
    }

    void Start() {
        state = FleetState.InitialiseFleet;
    }

    void FixedUpdate() {
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

            case FleetState.MoveFleetDelay:
                delayFleetMove();
                break;

            case FleetState.PauseBetweenFleets:
                delayFleetAppearing();
                break;

            case FleetState.FleetLanded:
                break;
        }
    }

    private void initialiseFleet() {
        int objectIndex = 0;
        for (float x = 0; x < alienColumns; x += 1) {
            float alienX = (x * 1.2f) - 9;
            Alien top  = new Alien(topAlienPrefab, new Vector2(alienX + 0.1f, 5));
            Alien mid1 = new Alien(midAlienPrefab, new Vector2(alienX, 4));
            Alien mid2 = new Alien(midAlienPrefab, new Vector2(alienX, 3));
            Alien bot1 = new Alien(botAlienPrefab, new Vector2(alienX, 2));
            Alien bot2 = new Alien(botAlienPrefab, new Vector2(alienX, 1));

            fleet[objectIndex] = bot2;
            fleet[objectIndex + alienColumns] = bot1;
            fleet[objectIndex + (alienColumns * 2)] = mid2;
            fleet[objectIndex + (alienColumns * 3)] = mid1;
            fleet[objectIndex + (alienColumns * 4)] = top;

            objectIndex++;
        }    

        direction = FleetDirection.LeftToRight;
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
        fleet[fleetIndex].activate();

        fleetIndex++;
        if (fleetIndex == fleet.Length) {
            fleetIndex = 0;
            state = FleetState.MoveFleet;
        }
    }

    private void moveFleet() {
        Alien lastAlien = lastAlienInFleet;
        if (lastAlien == null) {
            fleetDelayCounter = 120;
            GameManager.starSpeedMultiplier = 8.0f;
            state = FleetState.PauseBetweenFleets;
            return;
        }

        Alien alien = null;
        for (int ix = fleetIndex; ix < fleet.Length; ix++) {
            if (fleet[fleetIndex].isAlive) {
                alien = fleet[fleetIndex];
                break;
            } else {
                fleetIndex++;
            }
        }

        if (alien != null) {
            alien.move(direction);

            if (alien.x < minX) {
                minX = alien.x;
            }

            if (alien.x > maxX) {
                maxX = alien.x;
            }

            if (alien.y < minY) {
                minY = alien.y;
            }
        }

        if (lastAlien == alien || alien == null) {
            fleetIndex = 0;
        } else {
            fleetIndex++;
        }

        if (fleetIndex == 0) {
            if (shouldChangeDirection) {
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

                if (minY < (GameManager.boundsRect.yMin + 3.0f)) {
                    state = FleetState.FleetLanded;
                }

                minX = 0;
                maxX = 0;
                minY = 0;
            }

            if (state != FleetState.FleetLanded) {
                pauseCounter = remainingAlienCount > 40 ? 40 : remainingAlienCount;
                state = FleetState.MoveFleetDelay;
            }
        }
    }

    private Alien lastAlienInFleet {
        get {
            for (int ix = fleet.Length - 1; ix >= 0; ix--) {
                if (fleet[ix].isAlive) {
                    return fleet[ix];
                }
            }
            return null;
        }
    }

    private int remainingAlienCount {
        get {
            int count = 0;
            foreach (Alien alien in fleet) {
                if (alien.isAlive) {
                    count++;
                }
            }
            return count;
        }
    }

    private void delayFleetMove() {
        pauseCounter--;
        if (pauseCounter == 0) {
            state = FleetState.MoveFleet;
        }
    }

    private void delayFleetAppearing() {
        fleetDelayCounter--;
        if (fleetDelayCounter == 0) {
            state = FleetState.InitialiseFleet;
            GameManager.starSpeedMultiplier = 1.0f;
        }
    }

    private bool shouldChangeDirection {
        get {
            if (direction == FleetDirection.DownLeft || direction == FleetDirection.DownRight) {
                return true;
            }

            if (direction == FleetDirection.LeftToRight && maxX > (GameManager.boundsRect.xMax - 5.0f)) {
                return true;
            }

            if (direction == FleetDirection.RightToLeft && minX < (GameManager.boundsRect.xMin + 4.0f)) {
                return true;
            }

            return false;
        }
    }
}

class Alien {
    private GameObject alien;
    private const float xSpeed = 0.33f;
    private const float ySpeed = 0.5f;

    public float x {
        get {
            return alien.transform.position.x;
        }
    }

    public float y {
        get {
            return alien.transform.position.y;
        }
    }

    public Alien(GameObject alienPrefab, Vector2 position) {
        alien = GameObject.Instantiate(alienPrefab, position, new Quaternion(0, 0, 0, 0));
        alien.SetActive(false);
    }

    public void activate() {
        alien.SetActive(true);
    }

    public bool isAlive {
        get {
            return alien != null;
        }
    }

    public void move(FleetDirection direction) {
        Animator animator = alien.GetComponent<Animator>();
        bool walk = animator.GetBool("walk");
        animator.SetBool("walk", !walk);

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
    }
}
