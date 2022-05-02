using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace p28
{
    // Destructible object class
    public class DestructibleObject : MonoBehaviour
    {
        // Explosion effect prefab
        public GameObject explosionPrefab;
        private GameObject _explosionObj;

        // Player Object
        public GameObject player;

        // Initial position and rotation of the parts of the object
        private List<Vector3> _initPos = new List<Vector3>();
        private List<Quaternion> _initRot = new List<Quaternion>();
        // Parts of the object
        private List<Rigidbody> _parts = new List<Rigidbody>();
        // Is the object destroyed
        private bool _isDestroyed;
        // Is the player in range
        private bool _inRange;



        /// <summary>
        /// Make sure every rigidbody is kinematic on init
        /// </summary>
        void Start()
        {
            foreach (Transform obj in transform)
            {
                _initPos.Add(obj.position);
                _initRot.Add(obj.rotation);
                Rigidbody rb = obj.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = true;
                    _parts.Add(rb);
                }
            }
            _explosionObj = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            _explosionObj.SetActive(false);
        }


        /// <summary>
        /// Destroys the destructible object 
        /// </summary>        
        public void DestroyObject()
        {
            foreach (Rigidbody rb in _parts)
            {
                rb.isKinematic = false;
                Vector3 v = new Vector3(Random.Range(0, 100), Random.Range(0, 100), Random.Range(0, 100));
                rb.velocity = v;
            }
            _explosionObj.SetActive(true);
            player.GetComponent<PlayerDisplay>().DestroyEffect();
        }

        /// <summary>
        /// Restores the destructible object
        /// </summary>
        public void RestoreObject()
        {
            int i = 0;
            foreach (Transform obj in transform)
            {
                obj.GetComponent<Rigidbody>().isKinematic = true;
                obj.position = _initPos[i];
                obj.rotation = _initRot[i];
                i++;
            }
            _explosionObj.SetActive(false);
            player.GetComponent<PlayerDisplay>().RestoreEffect();
        }



        /// <summary>
        /// TODO > remove when EEG enabled
        /// </summary>
        private void Update()
        {
            Debug.Log("Focus level " + BCIControlGBA.instance.focusLevel);
            // TODO > change with focus value
            /*if (Input.GetKeyDown(KeyCode.T) && _inRange)
            {
                Debug.Log("boom");
                DestroyObject();
            }
            // TODO > change with focus value
            if (Input.GetKeyDown(KeyCode.R) && _inRange)
            {
                Debug.Log("tada");
                RestoreObject();
            }*/
            // TODO > change with focus value
            if (BCIControlGBA.instance.focusLevel > 90 && _inRange)
            {
                Debug.Log("boom");
                DestroyObject();
            }
            // TODO > change with focus value
            if (BCIControlGBA.instance.focusLevel < 15 && _inRange)
            {
                Debug.Log("tada");
                RestoreObject();
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                _inRange = true;
                GetComponent<Outline>().OutlineColor = Color.blue;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
            {
                _inRange = false;
                GetComponent<Outline>().OutlineColor = Color.white;
            }
        }
    }
}
