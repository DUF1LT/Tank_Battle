using UnityEngine;

namespace Assets.TankPrefab.Scripts
{
    public class ProjectileScript : MonoBehaviour
    {
        public float ProjectileSpeed;
        public GameObject Explosion;

        private bool canMove;

        // Start is called before the first frame update
        void Start()
        {
            Destroy(gameObject, 10f);
            canMove = true;
        }

        // Update is called once per frame
        void Update()
        {
            if(canMove)
                transform.position += transform.TransformDirection(Vector3.up * ProjectileSpeed);
        }

        private void OnCollisionEnter(Collision col)
        {
            canMove = false;
            GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
            GetComponent<Renderer>().enabled = false;
            GameObject explosion = Instantiate(Explosion, gameObject.transform);
            explosion.transform.localScale = new Vector3(20, 20, 20);
            Destroy(gameObject, 2f);
        }
    }
}
