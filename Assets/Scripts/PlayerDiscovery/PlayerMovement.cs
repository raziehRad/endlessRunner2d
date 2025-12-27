using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace DefaultNamespace
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float jumpForce;
        private Rigidbody2D rb;
        private bool isGround;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public void Tick()
        {
            if (isGround && Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }

        public void Jump()
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            rb.AddForce(Vector2.up*jumpForce,ForceMode2D.Impulse);
            AudioManager.instance.PlayJump();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.transform.CompareTag("Ground"))
                isGround = true;
        }
        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.transform.CompareTag("Ground"))
                isGround = false;
        }
    }
}