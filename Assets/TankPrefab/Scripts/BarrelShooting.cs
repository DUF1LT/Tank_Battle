using System.Collections;
using System.Collections.Generic;
using Assets.TankPrefab.Scripts;
using UnityEngine;

public class BarrelShooting : MonoBehaviour
{
    public GameObject Projectile;
    public GameObject FireBall;


    private Vector3 screenPos;
    private GUIStyle style = new GUIStyle();
    void OnGUI()
    {
        GUI.Label(new Rect(screenPos.x, screenPos.y, 14, 14), "+", style);
    }
    // Start is called before the first frame update
    void Start()
    {
        style.normal.textColor = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
        if (Physics.Raycast(ray, out var hit))
        {
            screenPos = Camera.main.WorldToScreenPoint(hit.point);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 barrelForward = transform.position +
                                     transform.TransformDirection(Vector3.forward * 3f);
            var newProjectile = Instantiate(Projectile, barrelForward, transform.rotation);
            newProjectile.transform.Rotate(90, 0, 0);

            GameObject fireBall = Instantiate(FireBall, barrelForward, transform.rotation);
            GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
            Destroy(fireBall, 0.1f);
        }

    }
}
