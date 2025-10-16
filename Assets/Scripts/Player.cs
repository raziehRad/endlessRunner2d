using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private int _playerHealth;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private bool _isGround = true;
    [SerializeField] private GroundManager2D _groundManager;
    [SerializeField] private float boostedTime = 3;
    private int fullHealth = 100;

    private float verticalVelocity;

    private int itemCount;

    private bool isBoosted;

    public PlayerState currentState;

    void Start()
    {
        currentState = PlayerState.Ideal;
    }

    void Update()
    {
        FSMController();
        Move();
        CheckDeath();
        if (isBoosted)
        {
            boostedTime -= Time.deltaTime;
            if (boostedTime < 0)
            {
                currentState = PlayerState.Ideal;
                boostedTime = 3;
            }
        }
    }

    private void FSMController()
    {
        //FSM
        switch (currentState)
        {
            case PlayerState.Jump:
                var rb = transform.GetComponent<Rigidbody2D>();
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                break;
            case PlayerState.Run:
                isBoosted = true;
                HUDManager.instace.SwitchBoosted(isBoosted);
                break;
            case PlayerState.Die:
                SceneManager.LoadScene(0);
                break;
            case PlayerState.Fall:
                SceneManager.LoadScene(0);
                break;
            case PlayerState.Ideal:
                isBoosted = false;
                _groundManager.moveSpeed = 5;
                HUDManager.instace.SwitchBoosted(isBoosted);
                break;
        }
    }

    private void CheckDeath()
    {
        if (transform.position.y < -9)
        {
            currentState = PlayerState.Fall;
        }
    }

    void Move()
    {
        if (_isGround && Input.GetButtonDown("Jump"))
        {
            currentState = PlayerState.Jump;
        }
    }

    public void TakeDamage(int damage)
    {
        _playerHealth -= damage;
        HUDManager.instace.SetPlayerHealth(damage);
        HUDManager.instace.SetItemCount(0);
        itemCount = 0;
        if (isBoosted)
        {
            isBoosted = false;
            HUDManager.instace.SwitchBoosted(isBoosted);
            _groundManager.moveSpeed = 5;
        }

        if (_playerHealth <= 0)
        {
            currentState = PlayerState.Die;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("item"))
        {
            other.gameObject.SetActive(false);
            HUDManager.instace.SetPlayerScore(10);
            itemCount++;
            if (itemCount == 10)
            {
                currentState = PlayerState.Run;
                _groundManager.moveSpeed += 2;
                itemCount = 0;
            }

            HUDManager.instace.SetItemCount(itemCount);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Ground"))
        {
            _isGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.CompareTag("Ground"))
        {
            _isGround = false;
            currentState = PlayerState.Ideal;
        }
    }
}

public interface IDamageable
{
    void TakeDamage(int damage);
}

public enum PlayerState
{
    Jump,
    Run,
    Fall,
    Die,
    Ideal
}