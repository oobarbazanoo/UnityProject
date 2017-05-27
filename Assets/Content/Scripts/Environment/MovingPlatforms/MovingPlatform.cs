using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 MoveBy;
    public float movingSpeed = 0.2f;
    public float waitTime = 4f;

    private float _waitTime;

    Vector3 pointA;
    Vector3 pointB;
    Vector3 target;
    Vector3 destinationVector;

    void Start()
    {
        initializePointsToMoveToAndFrom();
        setFirstTargetToInitializedPointB();
        setFirstDestinationVector();
        setFirstWaitTime();
    }

    private void initializePointsToMoveToAndFrom()
    {
        this.pointA = this.transform.position;
        this.pointB = this.pointA + MoveBy;
    }

    private void setFirstTargetToInitializedPointB()
    {
        this.target = this.pointB;
    }

    private void setFirstDestinationVector()
    {
        Vector3 _destinationVector = this.target - this.transform.position;
        _destinationVector.z = 0;
        this.destinationVector = Vector3.Normalize(_destinationVector);
    }

    private void setFirstWaitTime()
    {
        _waitTime = waitTime;
    }

    void Update()
    {
        moveAndIfArrivedWait();
    }

    private void moveAndIfArrivedWait()
    {
        if (isArrived(this.transform.position, target))
        {
            _waitTime -= Time.deltaTime;
            if (_waitTime <= 0)
            {
                target = (target == pointA) ? pointB : pointA;
                _waitTime = waitTime;
                destinationVector = (-1) * destinationVector;
            }
        }
        else
        {
            this.transform.Translate(destinationVector * movingSpeed);
        }
    }

    bool isArrived(Vector3 pos, Vector3 target)
    {
        pos.z = 0;
        target.z = 0;
        return Vector3.Distance(pos, target) < 0.05f;
    }
}
