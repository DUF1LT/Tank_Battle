using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarellRotate : MonoBehaviour
{
    private float barrelAngle;

    private float angle;
    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        angle= Input.GetAxis("Mouse Y")*4;
        if(angle == 0) return;
        barrelAngle -= angle;
        barrelAngle = Mathf.Clamp(barrelAngle, -30, 10);
        transform.localEulerAngles = new Vector3(barrelAngle, 0 , 0);
    }
}
