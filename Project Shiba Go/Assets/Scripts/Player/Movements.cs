// Player movement functions and guidelines

using UnityEngine;
using System.Collections;

namespace Character
{
    public partial class Player
    {
        private bool sendTriggerMessage = false;
        private bool isGrounded = true;
        private bool isJumping = false;
        private bool isRight = true;
        private bool isDoubleJump = false;
        private float checkRadius = 2.5f;
        private float jumpTimer = 0.0f;
        private float minimumExtent;
        private float partialExtent;
        private float sqrMinimumExtent;
        private float skinWidth = 0.075f;
        private LayerMask layerMask = -1;
        private Vector2 previousPosition;
        private Collider2D myCollider;

        [SerializeField] private Transform feet;
        [SerializeField] private LayerMask ground;
        [SerializeField] private Animator anim;

        [SerializeField] private float jumpTime = 0.35f;
        [SerializeField] private float speed = 25.0f;
        [SerializeField] private float jump = 30.0f;

        public bool canDoubleJump = false;



        private void MoveHorizontal()
        {
            float hAxis = Input.GetAxisRaw("Horizontal");
            float move = hAxis * speed;
            body.velocity = new Vector2(move, body.velocity.y);

            if ((hAxis > 0 && !isRight) || (hAxis < 0 && isRight))
            {
                isRight = !isRight;

                Vector3 scale = transform.localScale;
                scale.x *= -1;

                transform.localScale = scale;
            }

            anim.SetFloat("moveSpeed", Mathf.Abs(move));
        }

        private void MoveVertical()
        {
            isGrounded = Physics2D.OverlapCircle(feet.position, checkRadius, ground);

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (isGrounded)
                {
                    isJumping = true;
                    jumpTimer = jumpTime;
                    body.velocity = Vector2.up * jump;
                    isDoubleJump = true;
                }
                else
                {
                    if (isDoubleJump && canDoubleJump)
                    {
                        isDoubleJump = false;
                        jumpTimer = jumpTime;
                        body.velocity = Vector2.up * jump * 2.5f;
                    }
                }
            }

            if (Input.GetKey(KeyCode.UpArrow) && isJumping)
            {
                if(jumpTimer > 0)
                {
                    body.velocity = Vector2.up * jump;
                    jumpTimer -= Time.deltaTime;
                }
            }

            if (Input.GetKeyUp(KeyCode.UpArrow))
                isJumping = false;
        }

        private void CollisionCheck()
        {
            Vector3 movementThisStep = body.position - previousPosition;
            float movementSqrMagnitude = movementThisStep.sqrMagnitude;

            if (movementSqrMagnitude > sqrMinimumExtent)
            {
                float movementMagnitude = Mathf.Sqrt(movementSqrMagnitude);
                RaycastHit hitInfo;

                if (Physics.Raycast(previousPosition, movementThisStep, out hitInfo, movementMagnitude, layerMask.value))
                {
                    if (!hitInfo.collider)
                        return;

                    if (hitInfo.collider.isTrigger)
                        hitInfo.collider.SendMessage("OnTriggerEnter", myCollider);

                    if (!hitInfo.collider.isTrigger)
                        body.position = hitInfo.point - (movementThisStep / movementMagnitude) * partialExtent;
                }
            }

            previousPosition = body.position;
        }
    }
}