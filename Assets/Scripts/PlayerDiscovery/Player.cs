using DefaultNamespace;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private PlayerHealth health;
    [SerializeField] private PlayerBoost _boost;
    [SerializeField] private PlayerStateMachine _stateMachine;

    void Update()
    {
        _stateMachine.Tick();
    }

    void FixedUpdate()
    {
        _movement.Tick();
    }

    public void TakeDamage(int damage)
    {
        health.TakeDamage(damage);
    }
    // [SerializeField] private int _playerHealth;
    // [SerializeField] private float jumpForce = 5f;
    // [SerializeField] private bool _isGround = true;
    // [SerializeField] private GroundManager2D _groundManager;
    // [SerializeField] private float boostedTime = 3;
    // [SerializeField] private GameObject _coinFX;
    // [SerializeField] private GameObject _dieFX;
    // [SerializeField] private Animator _animator;
    // private int fullHealth = 100;
    //
    // private float verticalVelocity;
    //
    // private int itemCount;
    //
    // private bool isBoosted;
    //
    // public PlayerState currentState;
    // private float boostedTimeFix;
    //
    // void Start()
    // {
    //     currentState = PlayerState.Ideal;
    //     boostedTimeFix = boostedTime;
    // }
    //
    // void Update()
    // {
    //     FSMController();
    //     CheckDeath();
    //     if (isBoosted)
    //     {
    //         boostedTime -= Time.deltaTime;
    //         if (boostedTime < 0) 
    //         {
    //             currentState = PlayerState.Ideal;
    //             boostedTime = boostedTimeFix;
    //         }
    //     }
    // }
    //
    // private void FixedUpdate()
    // {
    //     Move();
    // }
    //
    // private void FSMController()
    // {
    //     //FSM
    //   
    // }
    //
    // private void JumpAction()
    // {
    //     var rb = transform.GetComponent<Rigidbody2D>();
    //     rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
    //     rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    //     AudioManager.instance.PlayJump();
    //   _animator.SetBool("_jump",true);
    // }
    //
    // public void JumpButton()
    // {
    //     if (_isGround) currentState = PlayerState.Jump;
    // }
    // private void GameOverAction()
    // {
    //     AudioManager.instance.PlayGameOver();
    //     _animator.CrossFade("Die",0.5f);
    //     Instantiate(_dieFX, transform.position, quaternion.identity);
    //     Invoke("ReLoad",0.2f);
    // }
    //
    // private void ReLoad()
    // {
    //     SceneManager.LoadScene(0);
    // }
    //
    // private void CheckDeath()
    // {
    //     if (transform.position.y < -9)
    //     {
    //         currentState = PlayerState.Fall;
    //     }
    // }
    //
    // void Move()
    // {
    //     //_animation.Play("Run");
    //     if (_isGround && (Input.GetButtonDown("Jump")))
    //     {
    //         currentState = PlayerState.Jump;
    //     }
    // }
    // public void TakeDamage(int damage)
    // {
    //     _playerHealth -= damage;
    //     HUDManager.instace.SetPlayerHealth(damage);
    //     HUDManager.instace.SetItemCount(0);
    //     itemCount = 0;
    //     if (isBoosted)
    //     {
    //         isBoosted = false;
    //         HUDManager.instace.SwitchBoosted(isBoosted);
    //         _groundManager.moveSpeed = 5;
    //     }
    //
    //     if (_playerHealth <= 0)
    //     {
    //         currentState = PlayerState.Die;
    //     }
    // }
    //
    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.CompareTag("item"))
    //     {
    //         AudioManager.instance.PlayCoin();
    //        SpawnCoinEffect(other);
    //        HUDManager.instace.SetItemCount(itemCount);
    //     }
    // }
    //
    // private void SpawnCoinEffect(Collider2D other)
    // {
    //     var fx = Instantiate(_coinFX, other.transform.position, quaternion.identity);
    //     fx.GetComponent<ParticleSystem>().Play();
    //     HUDManager.instace.SetPlayerScore(other.GetComponent<Item>().data.value);
    //     other.transform.DOScale(new Vector3(0.5f, 0.8f, 0.5f), 0.3f)
    //         .SetEase(Ease.OutBack).OnComplete((() =>
    //         {
    //             other.transform.DOScale(new Vector3(0.2f, 0.5f, 0.2f), 0.2f)
    //                 .SetEase(Ease.InOutSine).OnComplete(() =>
    //                 {
    //                     other.gameObject.SetActive(false);
    //                     Destroy(fx);
    //                     other.transform.DOScale(new Vector3(0.3f, 0.6f, 0.3f), 0.2f);
    //                 });
    //         }));
    //     itemCount++;
    //     if (itemCount == 10)
    //     {
    //         currentState = PlayerState.Run;
    //         _groundManager.moveSpeed += 2;
    //         itemCount = 0;
    //     }
    // }
    //
    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     if (other.transform.CompareTag("Ground"))
    //     {
    //         _isGround = true;
    //         _animator.SetBool("_jump",false);
    //     }
    // }
    //
    // private void OnCollisionExit2D(Collision2D other)
    // {
    //     if (other.transform.CompareTag("Ground"))
    //     {
    //         _isGround = false;
    //         currentState = PlayerState.Ideal;
    //         //_animator.SetTrigger("Run");
    //     }
    // }
}

