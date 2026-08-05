using DefaultNamespace;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private int coins;

    public int Score => score;
    public int Coins => coins;

    public void AddScore(int value)
    {
        score += value;
        // Update Score UI
    }

    public void AddCoin(int value, Collider2D other)
    {
        coins += value;
        AudioManager.instance.PlayCoin(coins);
        GetComponent<PlayerPickHandler>().SpawnCoinEffect(other,score);
        GameEvents.OnCoinChanged?.Invoke(coins);
        //HUDManager.Instance.SetItemCount(score);
        GameEvents.OnScoreChanged?.Invoke(score);
        // Update Coin UI
    }

    public void Reset()
    {
        score = 0;
        coins = 0;
    }
}