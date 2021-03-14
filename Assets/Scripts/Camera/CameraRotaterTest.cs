using System.Collections;
using UnityEngine;
//courtesy of artichokeCubed

public class CameraRotaterTest : MonoBehaviour
{

    [SerializeField]
    Transform pointToOrbit;

    [SerializeField]
    float distanceAway = 1;

    [SerializeField]
    float slerpLengthInSeconds = 1f;

    [SerializeField]
    float slerpsASecond = 100f;

    Vector3 firstPointClicked;
    Vector3 secondPointClicked;

    void Start()
    {
        transform.position = pointToOrbit.position + (Vector3.up * distanceAway);
        transform.LookAt(pointToOrbit);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            firstPointClicked = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            secondPointClicked = Input.mousePosition;
            StartCoroutine(Slerp(RotationBetween(transform.rotation * Vector3.forward, transform.rotation * (secondPointClicked - firstPointClicked))));
        }

        distanceAway += Input.mouseScrollDelta.y;
        distanceAway = distanceAway >= 0 ? distanceAway : 0;
        if (Input.mouseScrollDelta.y != 0)
        {
            transform.position = pointToOrbit.position;
            transform.position += transform.forward * -distanceAway;
        }


    }//end update

    private IEnumerator Slerp(Quaternion rotation)
    {
        Quaternion init = transform.rotation;
        for (int i = 1; i < (slerpsASecond * slerpLengthInSeconds) + 1; i++)
        {
            transform.position = pointToOrbit.position;

            transform.rotation = Quaternion.Lerp(init, rotation * init, i / (slerpsASecond * slerpLengthInSeconds));

            transform.position += transform.forward * -distanceAway;
            transform.LookAt(pointToOrbit);

            yield return new WaitForSeconds(1 / slerpsASecond);
        }
    }

    public Quaternion RotationBetween(Vector3 directionOne, Vector3 directionTwo)
    {
        directionOne.Normalize();
        directionTwo.Normalize();

        if (Vector3.Angle(directionTwo, directionOne) == 0)
        {
            return new Quaternion(0, 0, 0, 1);
        }
        else if (Vector3.Angle(directionTwo, directionOne) == 180)
        {
            return new Quaternion(1, 0, 0, 0);
        }
        else
        {
            Vector3 cross = Vector3.Cross(directionOne, directionTwo);
            float w = Mathf.Sqrt((directionTwo.sqrMagnitude * directionOne.sqrMagnitude)) + Vector3.Dot(directionOne, directionTwo);
            return new Quaternion(cross.x, cross.y, cross.z, w).normalized;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(Vector3.zero, Vector3.forward);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(Vector3.zero, (secondPointClicked - firstPointClicked).normalized);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(Vector3.zero, transform.rotation * Vector3.forward);
        Gizmos.DrawLine(Vector3.zero, transform.forward);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(Vector3.zero, transform.rotation * (secondPointClicked - firstPointClicked).normalized);

    }//end method
}//end class