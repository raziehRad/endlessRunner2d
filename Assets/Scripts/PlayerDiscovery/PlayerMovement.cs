using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace DefaultNamespace
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float jumpForce;
        [SerializeField] private GameObject wings;
        private Rigidbody2D rb;
        private bool isGround;

        private Animator _animator;
        private PlayerStateMachine _playerStateMachine;
        
        private float normalGravity;
        private bool isFlying;

        private void Awake()
        {
           
            rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            normalGravity = rb.gravityScale;
            _playerStateMachine = GetComponent<PlayerStateMachine>();
        }
        
        public void Tick()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
            }
        }

        public void Jump()
        {
            if (!isGround) return;
            if (isFlying) return;
            
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            rb.AddForce(Vector2.up*jumpForce,ForceMode2D.Impulse);
            AudioManager.instance.PlayJump();
            _animator.SetBool("_jump",true);
        }

        public void FlyingMode( bool enable,int flyingHeight )
        {
            isFlying = enable;
            _animator.SetBool("flying",enable);
            wings.SetActive(enable);
            if (enable)
            {
                rb.gravityScale = 0;
                rb.linearVelocity = Vector2.zero;

                var pos = transform.position;
                pos.y = flyingHeight;
                transform.position = new Vector2(0,flyingHeight);
            }
            else rb.gravityScale = normalGravity;
        }
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.transform.CompareTag("Ground"))
            {
                isGround = true;
                _animator.SetBool("_jump",false);
            }
        }
        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.transform.CompareTag("Ground"))
            {
                isGround = false;
                _playerStateMachine.ChangeState(PlayerState.Ideal);
            }
        }

        public void JumpMode(ItemData itemData)
        {
            if (isFlying) return;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            rb.AddForce(Vector2.up*itemData.value,ForceMode2D.Impulse);
            AudioManager.instance.PlayJump();
            _animator.SetBool("_jump",true);
        }
    }
}