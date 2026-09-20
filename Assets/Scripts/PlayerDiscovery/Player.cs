using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

// Acts as the main coordinator for player-related systems.
// Each responsibility is delegated to a dedicated component.
public class Player : MonoBehaviour
{
    private PlayerHealth health;
    private PlayerMovement _movement;
    private PlayerStateMachine _stateMachine;

    private void Awake()
    {
        health = GetComponent<PlayerHealth>();
        _stateMachine = GetComponent<PlayerStateMachine>();
        _movement = GetComponent<PlayerMovement>();
    }

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
