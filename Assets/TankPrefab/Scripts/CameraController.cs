using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera _camera;
    private bool RMBclicked = false;
    private Vector3 startingPosition;
    private float сameraAngle;
    private float angle;
    

    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();
        startingPosition = _camera.gameObject.transform.localPosition;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RMBclicked = !RMBclicked;
            transform.localPosition = RMBclicked ? new Vector3(-20,25 , 50) : startingPosition;
        }

        angle = Input.GetAxis("Mouse Y") * 4;
        if (angle == 0) return;
        сameraAngle -= angle;
        сameraAngle = Mathf.Clamp(сameraAngle, -30, 10);
        transform.localEulerAngles = new Vector3(сameraAngle, 0, 0);
    }


}
