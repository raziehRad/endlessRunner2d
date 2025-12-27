
    using UnityEngine;

    public class DifficultyManager : MonoBehaviour
    {
        [SerializeField] private DifficultyCurve difficulty;
        [SerializeField] private int currentLevel;

        public float GetHealthMultiplier()
        {
            return difficulty.enemyHealthMultiplier.Evaluate(currentLevel);
        }
    }
