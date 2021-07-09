using UnityEngine;

namespace Assets.TankPrefab.Scripts
{
    public class TankController : MonoBehaviour
    {
        private bool isPlaying = false;
        private Vector3 pos, rotation;
        private Quaternion quat;

        private float force;
        private float speed = 20f;

        public Transform[] WheelMeshes;
        public WheelCollider[] WheelColls;
        public float RotSpeed, maxSpeed;


        private void Start()
        {
            rotation = transform.eulerAngles;
           
        }

        private void Update()
        {
            force = speed * 500;

            transform.eulerAngles = rotation;
            var verAxis = Input.GetAxis("Vertical");
            var horAxis = Input.GetAxis("Horizontal");


            for (int i = 0; i < WheelColls.Length; i++)
            {
                WheelColls[i].GetWorldPose(out pos, out quat);
                WheelMeshes[i].rotation = quat;
            }

            if (verAxis != 0 || horAxis != 0)
            {
                if (!isPlaying)
                {
                    GetComponent<AudioSource>().Play();
                    isPlaying = true;
                }


                foreach (var wheelcols in WheelColls) //привет Вадим Наркевич
                {
                    wheelcols.brakeTorque = 0f;
                    wheelcols.motorTorque = verAxis * force;
                }

                if (GetComponent<Rigidbody>().velocity.magnitude > maxSpeed)
                    GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * maxSpeed;

                if (Input.GetKey(KeyCode.Space))
                {
                    foreach (var wheelcols in WheelColls)
                    {
                        wheelcols.brakeTorque = wheelcols.mass * GetComponent<Rigidbody>().mass;
                    }
                }

                rotation.y += horAxis * RotSpeed;

            }
            if (verAxis == 0 && horAxis == 0 && isPlaying)
            {
                GetComponent<AudioSource>().Stop();
                isPlaying = false;
            }
        }
    }
}
