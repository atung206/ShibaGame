// Main player file
// Purpose is to instantiate a partial class that can be edited further
// This file will have the bare bones of the class, and the rest of the 
// class can/will be fleshed out in their respective files.

using UnityEngine;

namespace Character
{
    public partial class Player : MonoBehaviour
    {
        private Persistent.Persistent persistVars;

        private Rigidbody2D body;

        public static Player playerInstance;



        private void Awake()
        {
            if (playerInstance == null)
            {
                DontDestroyOnLoad(this);
                playerInstance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            persistVars = GameObject.Find("Persistent").GetComponent<Persistent.Persistent>();

            body = GetComponent<Rigidbody2D>();
            myCollider = GetComponent<Collider2D>();
            previousPosition = body.position;
            minimumExtent = Mathf.Min(myCollider.bounds.extents.x, myCollider.bounds.extents.y);
            partialExtent = minimumExtent * (1.0f - skinWidth);
            sqrMinimumExtent = minimumExtent * minimumExtent;
        }

        private void Update()
        {
            if (!persistVars.thruPortal)
            {
                MoveVertical();
            }
        }

        private void FixedUpdate()
        {
            if (!persistVars.thruPortal)
                MoveHorizontal();

            CollisionCheck();
        }
    }
}

