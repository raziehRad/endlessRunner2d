using System;
using DefaultNamespace;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private int coins;
    private PlayerPickHandler _pickHandler;

    private void Awake()
    {
        _pickHandler = GetComponent<PlayerPickHandler>();
    }

    public void AddScore(int value)
    {
        score += value;
        // Update Score UI
    }

    public void AddCoin(int value, Collider2D other)
    {
        coins += value;
        _pickHandler.SpawnCoinEffect(other,score);
        GameEvents.OnCoinChanged?.Invoke(coins);
        GameEvents.OnScoreChanged?.Invoke(score);
        // Update Coin UI
    }

    public void Reset()
    {
        score = 0;
        coins = 0;
    }
}