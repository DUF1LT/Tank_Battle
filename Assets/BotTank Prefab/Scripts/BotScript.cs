using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.BotTank_Prefab.Scripts
{
    public class BotScript : MonoBehaviour
    {
        private float moveSpeed, rotSpeed, rotSpeedTower, life = 4;
        public Transform Tower, Barell;
        private bool canShoot = true;
        public Transform[] WheelMeshes;
        public WheelCollider[] WheelColls;
        public GameObject Projectile;

        private Quaternion towerRotation;

        void OnTriggerStay(Collider col)
        {
            if (col.tag == "Player")
            {
                float distance = Vector3.Distance(col.transform.position, transform.position);

                Vector3 relativePos = col.transform.position - transform.position;
                towerRotation = Quaternion.LookRotation(relativePos);


                Debug.Log(Time.deltaTime * rotSpeedTower);

                Tower.rotation = Quaternion.Slerp(Tower.rotation, towerRotation, Time.deltaTime * rotSpeedTower);

                if (Physics.Raycast(Barell.position, Barell.TransformDirection(Vector3.forward), out var hit))
                {
                    Debug.Log(hit.transform.tag);
                    if ((hit.transform.tag == "Player") && canShoot)
                    {
                        StartCoroutine(botShoot());
                    }
                }

                if (distance < 40)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, towerRotation, Time.deltaTime * rotSpeed);
                    //transform.position = Vector3.Lerp(transform.position, col.transform.position,
                    //    Time.deltaTime * moveSpeed);

                    for (int i = 0; i < WheelColls.Length; i++)
                    {
                        WheelColls[i].GetWorldPose(out var pos, out var quat);
                        WheelMeshes[i].rotation = quat;
                    }
                    foreach (var wheelcols in WheelColls)
                    {
                        wheelcols.brakeTorque = 0f;
                        wheelcols.motorTorque = moveSpeed * 30000;
                    }


                }
                else
                {
                    foreach (var wheelcols in WheelColls)
                    {
                        wheelcols.brakeTorque = wheelcols.mass * GetComponent<Rigidbody>().mass;
                    }
                }
            }
            else
            {
                towerRotation = new Quaternion(0, 0, 0, 0);
            }
        }



        IEnumerator botShoot()
        {
            canShoot = false;
            Vector3 barrelForward = Barell.transform.position +
                                    Barell.transform.TransformDirection(Vector3.forward * 3f);
            var projectile = Instantiate(Projectile, barrelForward, Barell.transform.rotation);
            projectile.transform.Rotate(90, 0, 0);

            yield return new WaitForSeconds(5f);
            canShoot = true;
        }

        void OnCollisionEnter(Collision col)
        {
            if (col.gameObject.tag == "Projectile")
            {
                if (--life < 1)
                {
                    Destroy(gameObject);
                }
            }
        }
        void Start()
        {
            moveSpeed = 2f;
            rotSpeed = 1.2f;
            rotSpeedTower = 1.2f;
        }

        // Update is called once per frame
        void Update()
        {
        }

    }
}
