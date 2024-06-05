using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float Sensitivity
    {
        get { return sensitivity; }
        set { sensitivity = value; }
    }
    [Range(0.1f, 9f)][SerializeField] float sensitivity = 2f;

    //[Range(0f, 90f)][SerializeField] float yRotationLimit = 88f;
    //[Range(0f, 90f)][SerializeField] float xRotationLimit = 88f;
    Vector2 rotation = Vector2.zero;
    Vector3 delta = Vector3.zero;
    const string xAxis = "Mouse X";
    const string yAxis = "Mouse Y";

    void Update()
    {
        rotation.x += Input.GetAxis(xAxis) * sensitivity;
        rotation.y += Input.GetAxis(yAxis) * sensitivity;
        //rotation.x = Mathf.Clamp(rotation.y, -xRotationLimit, xRotationLimit);
        //rotation.y = Mathf.Clamp(rotation.y, -yRotationLimit, yRotationLimit);
        var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
        var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);

        transform.localRotation = xQuat * yQuat;
        transform.localPosition = transform.localPosition - new Vector3(delta.x, 0f,delta.y);
    }
}
