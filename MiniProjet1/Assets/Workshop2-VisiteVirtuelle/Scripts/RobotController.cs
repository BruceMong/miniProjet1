using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum State
{
    Idle,
    EditingTrajectory,
    MovingToNextPoint,
    AutoMoving
}


public class RobotController : MonoBehaviour
{
    private int currentId = 0;
    private State currentState;
    public RobotManager RobotManager;
    public NavMeshAgent Agent;
    PositionStruct currentPosition;
    public bool modeAuto = false;

    void Start()
    {

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
            RobotManager = playerObject.GetComponent<RobotManager>();
        if (RobotManager == null)
        {
            Debug.LogError("RobotManager component not found on player object");
        }
        currentState = State.Idle;
    }

    void Update()
    {
        if (modeAuto && currentState == State.AutoMoving)
        {
            if (IsAtDestination())
            {
                currentId = (currentId + 1) % RobotManager.GetTrajectoryList().Count; // Boucle à travers les points
                getNextPoint();
                MoveToPoint();
            }
        }
    }

    bool IsAtDestination()
    {
        return !Agent.pathPending && Agent.remainingDistance <= Agent.stoppingDistance &&
               (!Agent.hasPath || Agent.velocity.sqrMagnitude == 0f);
    }

    public void getNextPoint()
    {
        List<PointOfTrajectory> trajectoryList = RobotManager.GetTrajectoryList();
        if (currentId >= 0 && currentId < trajectoryList.Count)
        {
            PointOfTrajectory currentPoint = trajectoryList[currentId];
            PointOfTrajectoryStruct currentPointData = currentPoint.PointOfTrajectoryData;
            currentPosition = currentPointData.Position;
            Debug.Log("Point ID: " + currentPointData.Id + ", Position: (" + currentPosition.x + ", " + currentPosition.y + ")");
        }
        else
        {
            Debug.LogError("currentId est hors de portée: " + currentId);
        }
    }

    public void MoveToPoint()
    {
        Vector3 destination = new Vector3(currentPosition.x, currentPosition.y, 0);
        Agent.SetDestination(destination);
    }

    // Appeler cette méthode pour activer/désactiver le mode automatique
    public void ToggleAutoMode()
    {
        modeAuto = !modeAuto;
        currentState = modeAuto ? State.AutoMoving : State.Idle;
        if (modeAuto)
        {
            MoveToPoint(); // Commencer à se déplacer vers le point actuel
        }
    }
}
