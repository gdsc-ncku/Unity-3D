using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fort_arc : MonoBehaviour
{
    [SerializeField] private Transform yaw;
    [SerializeField] private Transform yaw_rotatespeed;
    [SerializeField] private Transform pitch;
    [SerializeField] private Transform pitch_rotatespeed;
    // Update is called once per frame
void Update()
{
    float horizontalInput = Input.GetAxis("Horizontal");
    float verticalInput = Input.GetAxis("Vertical");
    Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput);
    if (direction != Vector3.zero)
    {
        RotateToDirection(direction);
    }
}

void RotateToDirection(Vector3 direction)
    {
    Quaternion targetRotation = Quaternion.LookRotation(direction);
    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
void RotateToTarget(Vector3 p)
    {
        YawRotateTo(p);
    }

void YawRotateTo(Vector3 p)
{
    Vector3 p_xz = new Vector3(p.x, 0, p.z);
    Vector3 cur_xz = new Vector3(yaw.forward.x, 0,yaw.forward.z);
    float angle = Vector3.Angle(p_xz, cur_xz);
    yaw.localRotation = Quaternion.RotateTowards(
        yaw.localRotation,
        Quaternion.LookRotation(p_xz, yaw.up),
        Math.Min(angle, yaw_rotate_speed * Time.deltaTime));
}
   //弹道线
List<Vector3> GetParabolaLine(Vector3 lanchPoint,Vector3 vdir,float maxVelocity)
{
    parabolaPath=new List<Vector3>();
    Vector3 s;

    if (mFcm.TryGet_flyTime(lanchPoint,vdir,maxVelocity,
            out float t,out Vector3 dp))
    {
        estimateFlyTime = t;
        int max_count = 1000;
        float t0 = 0f;
        float dt = 0.02f;
        parabolaPath.Add(lanchPoint);
        while (t0< t && max_count > 0)
        {
            t0 = Mathf.Min(t0 + dt, t);
            s = lanchPoint + 0.5f * g * t0 * t0 * Vector3.down + vdir * maxVelocity * t0;
            parabolaPath.Add(s);
            max_count--;
        }
    }
    return parabolaPath;
}

private void OnDrawGizmos()
{
    var path= GetParabolaLine(pitch.position,-pitch.forward,max_muzzle_velocity);
    GizmosDrawPath(path, Color.green, Vector3.zero);
} 

}
