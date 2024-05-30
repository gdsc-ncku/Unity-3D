using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public bool _lock;
    [SerializeField] GameObject _cameraRoot;
    [SerializeField] float _cameraMovingThreshold, _topClamp, _bottomClamp;
    [SerializeField] PlayerBasicInformationScriptable _playerInformation;
    float _mouseXInput, _mouseYInput;

    private void Start()
    {
        _lock = false;
    }

    void Update()
    {
        //Click right mouse to hide cursor or press ESC to display cursor
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Grab mouse movement
        _mouseXInput = Input.GetAxis("Mouse X");
        _mouseYInput = Input.GetAxis("Mouse Y");

        //Grab present rotation
        float _rotateX = _cameraRoot.transform.eulerAngles.x, _rotateY = _cameraRoot.transform.eulerAngles.y;

        //The range of _rotateX is 0~360, therefore we need if,else if to let its range limit in -180~180
        if (_rotateX > 180)
            _rotateX -= 360;
        else if (_rotateX < -180)
            _rotateX += 360;
        //Detect whether camera open and input >= threshold
        if (!_lock && (Mathf.Abs(_mouseXInput) >= _cameraMovingThreshold || Mathf.Abs(_mouseYInput) >= _cameraMovingThreshold))
        {
            _rotateX -= _mouseYInput * _playerInformation.edpi * Time.deltaTime;
            _rotateY += _mouseXInput * _playerInformation.edpi * Time.deltaTime;
        }

        //Limit camera rotation
        _rotateX = ClampAngle(_rotateX, -1 * _topClamp, _bottomClamp);
        _rotateY = ClampAngle(_rotateY, float.MinValue, float.MaxValue);

        //Rotate camera
        transform.root.rotation = Quaternion.Euler(0, _rotateY, 0);
        _cameraRoot.transform.localRotation = Quaternion.Euler(_rotateX, 0, 0);
    }

    float ClampAngle(float Angle, float Min, float Max)
    {
        if (Angle < -360f) Angle += 360f;
        if (Angle > 360f) Angle -= 360f;
        return Mathf.Clamp(Angle, Min, Max);
    }
}