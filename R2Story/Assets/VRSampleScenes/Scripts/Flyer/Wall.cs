using System;
using UnityEngine;
using VRStandardAssets.Common;
using Random = UnityEngine.Random;

namespace VRStandardAssets.Flyer
{
    // This class controls each asteroid in the flyer scene.
    public class Wall : MonoBehaviour
    {
        public event Action<Wall> OnWallRemovalDistance;    // This event is triggered when it is far enough behind the camera to be removed.
        public event Action<Wall> OnWallHit;                // This event is triggered when the asteroid is hit either the ship or a laser. 


      
        [SerializeField] private float m_MinSpeed = 0f;             // The minimum speed the asteroid will move towards the camera.
        [SerializeField] private float m_MaxSpeed = 10f;            // The maximum speed the asteroid will move towards the camera.
        [SerializeField] private float m_MinRotationSpeed = 100f;   // The minimum speed the asteroid will rotate at.
        [SerializeField] private float m_MaxRotationSpeed = 140f;   // The maximum speed the asteroid will rotate at.
        [SerializeField] private int m_PlayerDamage = 20;           // The amount of damage the asteroid will do to the ship if it hits.
        [SerializeField] private int m_Score = 10;                  // The amount added to the score when the asteroid hits either the ship or a laser.


        private Rigidbody m_RigidBody;                              // Reference to the asteroid's rigidbody, used to move and rotate it.
        private FlyerHealthController m_FlyerHealthController;      // Reference to the flyer's health script, used to damage it.
        private GameObject m_Flyer;                                 // Reference to the flyer itself, used to determine what was hit.
        private Transform m_Cam;                                    // Reference to the camera so this can be destroyed when it's behind the camera.
        private float m_Speed;                                      // How fast asteroid will move towards the camera.
        private Vector3 m_RotationAxis;                             // The axis around which the asteroid will rotate.
        private float m_RotationSpeed;                              // How fast the asteroid will rotate.


        private const float k_RemovalDistance = 6000f;                // How far behind the camera the asteroid must be before it is removed.


        public int Score { get { return m_Score; } }
        

        private void Awake ()
        {
            m_RigidBody = GetComponent<Rigidbody>();
            
            m_FlyerHealthController = FindObjectOfType<FlyerHealthController>();
            m_Flyer = m_FlyerHealthController.gameObject;

            m_Cam = Camera.main.transform;
        }


        private void Start()
        {
            // Set a random scale for the asteroid.
         

            // Set a random rotation for the asteroid.
           

            // Set a random speed for the asteroid.
            m_Speed = Random.Range(m_MinSpeed, m_MaxSpeed);

            // Setup a random spin for the asteroid.
           
        }


        private void Update()
        {
            // Move and rotate the asteroid given the previously set up parameters.
            m_RigidBody.MovePosition(m_RigidBody.position - Vector3.forward * m_Speed * Time.deltaTime);
            if (transform.position.z < m_Cam.position.z - k_RemovalDistance)
                if (OnWallRemovalDistance != null)
                    OnWallRemovalDistance(this);
        }


        private void OnTriggerEnter(Collider other)
        {
            // Only continue if the asteroid has hit the flyer.
            if (other.gameObject != m_Flyer)
                return;

            // Damage the flyer.
            m_FlyerHealthController.TakeDamage(m_PlayerDamage);

            // If the damage didn't kill the flyer add to the score and call the appropriate events.
            if (!m_FlyerHealthController.IsDead)
                Hit();
        }


        private void OnDestroy()
        {
            // Ensure the events are completely unsubscribed when the asteroid is destroyed.
            OnWallRemovalDistance = null;
            OnWallHit = null;
        }


        public void Hit()
        {
            // Add to the score.
            SessionData.AddScore(m_Score);

            // If OnAsteroidHit has any subscribers call it.
            if (OnWallHit != null)
                OnWallHit(this);
        }
    }
}