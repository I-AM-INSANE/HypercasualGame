using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    #region Fields

    [SerializeField] private GameObject prefabTrajectoryPoint;
    [SerializeField] private int trajectoryPointsNumber;
    private GameObject[] trajectoryPoints;
    private float accuracyPoints = 0.8f;   // Кучность точек траектории

    private Vector3 startPosition;

    #endregion

    #region Methods

    private void Awake()
    {
        startPosition = transform.position;

        trajectoryPoints = new GameObject[trajectoryPointsNumber];
        for (int i = 2; i < trajectoryPointsNumber; i++)    // Не спавню несколько первых точек
        {
            trajectoryPoints[i] = Instantiate(prefabTrajectoryPoint, transform.position, Quaternion.identity);
        }
    }

    private void Update()
    {
        for (int i = 2; i < trajectoryPoints.Length; i++)
        {
            trajectoryPoints[i].transform.position = PointPosition(i);
        }
    }

    private Vector2 PointPosition(float t)
    {
        Vector2 direction = startPosition - transform.position;
        Vector2 currentPosition = (Vector2)transform.position + (direction.normalized * accuracyPoints * t) + 0.5f * Physics2D.gravity * (t * t);

        return currentPosition;
    }

    public void DestroyTrajectory()
    {
        for (int i = 0; i < trajectoryPoints.Length; i++)
        {
            Destroy(trajectoryPoints[i]);
        }
        enabled = false;
    }
    #endregion
}
