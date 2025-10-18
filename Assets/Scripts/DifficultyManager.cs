
    using UnityEngine;

    public class DifficultyManager : MonoBehaviour
    {
        public DifficultyCurve difficulty;
        public int currentLevel;

        public float GetHealthMultiplier()
        {
            return difficulty.enemyHealthMultiplier.Evaluate(currentLevel);
        }
    }
