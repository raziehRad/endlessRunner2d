using DefaultNamespace;
using UnityEngine;

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
        private static readonly int JumpHash =
            Animator.StringToHash("_jump");

        private static readonly int FlyingHash =
            Animator.StringToHash("flying");

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
            if (!isGround || isFlying)
                return;
            
            Jump(jumpForce);
        }

        public void SetFlyingMode( bool enable,int flyingHeight )
        {
            isFlying = enable;
            _animator.SetBool(FlyingHash,enable);
            wings.SetActive(enable);
            if (enable)
            {
                rb.gravityScale = 0;
                rb.linearVelocity = Vector2.zero;
                var pos = transform.position;
                pos.y = flyingHeight;
                transform.position = pos;
            }
            else rb.gravityScale = normalGravity;
        }
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.transform.CompareTag("Ground"))
            {
                isGround = true;
                _animator.SetBool(JumpHash,false);
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
        private void Jump(float force)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
            AudioManager.instance.Play(SoundType.Jump);
            _animator.SetBool(JumpHash, true);
        }
        public void JumpMode(ItemData itemData)
        {
            if (isFlying) return;
            Jump(itemData.value);
        }
    }
