
    using TMPro;
    using UnityEngine;

    public class HUDScore : MonoBehaviour
    {
        [SerializeField]protected TextMeshProUGUI _playerScoretxt;
        [SerializeField] protected TextMeshProUGUI _bonesTXT;
        [SerializeField]protected HUDAnimation _HUDAnimate;
        [SerializeField] protected TextMeshProUGUI _highScoreTxt;
        private int _playerScore;
        public void SetPlayerScore(int score)
        {
            AddScore(score);
            _playerScoretxt.text = _playerScore.ToString();
            CheckPrize(_playerScore);
        }
        public void AddScore(int amount)
        {
            _playerScore += amount;
            var highScore= SaveManager.LoadHighScore();
            if (_playerScore > highScore)
            {
                highScore = _playerScore;
                SaveManager.SaveHighScore(highScore);
                //  Debug.Log("New HighScore Saved: " + highScore);
                //FirebaseAnalytics.Instance.LogLevelComplete(highScore);
            }
        }
        private void CheckPrize(int score)
        {
            if (score%100==0)
            {
                _bonesTXT.text = "go on";
                _bonesTXT.gameObject.SetActive(true);
                _HUDAnimate. BonesScale();
            }

            if (score %1000==0)
            {
                _bonesTXT.text =" you're on fire";
                _bonesTXT.gameObject.SetActive(true);
                _HUDAnimate .BonesScale();
            }

            if (score % 10000 == 0)
            {
                _bonesTXT.text = "Legend";
                _bonesTXT.gameObject.SetActive(true);
                _HUDAnimate.BonesScale();
            }
            _HUDAnimate.BonesScale(_bonesTXT.transform);
        }
        private void SaveScore()
        {
            var highScore= SaveManager.LoadHighScore();
            if (_playerScore>highScore)
            {
                //PlayerPrefs.SetInt("Highscore",_playerScore);
                _highScoreTxt.text = _playerScore.ToString();
                // FirebaseAnalytics.Instance.LogLevelComplete(highScore);
            }
        }
        public void SaveData(int reward)
        {
            var highscore = SaveManager.LoadHighScore();
            highscore += reward;
            // PlayerPrefs.SetInt("Highscore", highscore);
            _highScoreTxt.text = highscore.ToString();
        }
    }
