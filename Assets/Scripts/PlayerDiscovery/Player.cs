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

}

