
    using System;
    using System.Collections;
    using DG.Tweening;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class HUDManager : MonoBehaviour
    {
        public static HUDManager Instance;

        [SerializeField] private TextMeshProUGUI _playerHealthtxt;
        [SerializeField] private TextMeshProUGUI _playerScoretxt;
        [SerializeField] private TextMeshProUGUI _playerItemtxt;
        [SerializeField] private GameObject _playerItemPic;
        [SerializeField] private TextMeshProUGUI _boostedItem;
        [SerializeField] private TextMeshProUGUI _bonesTXT;
        [SerializeField] private TextMeshProUGUI _highScoreTxt;
        [SerializeField] private Image shieldTimerImage;
        [SerializeField] private Image powerUpTimerImage;
        [SerializeField] private Image flyingTimerImage;
        
        [SerializeField] private GameObject _startPanel;
        [SerializeField] private GameObject characterPanel;
        [SerializeField] private GameObject _continue;
        [SerializeField] private GameObject _rePlay;
        [SerializeField] private GameObject _play;
        private int _playerHealth=100;
        private int _playerScore;
        private int _playerSpeed;
        private CharacterData characterData;
        


        private void Awake()
        {
            Instance = this;
            StartSetting();
        }

        private void StartSetting()
        {
            Time.timeScale = 0;
            var highScore =  SaveManager.LoadHighScore();
            _highScoreTxt.text = highScore.ToString();
            _continue.SetActive(highScore != 0);
            GameManager.Instance.Player.SetCharacter();
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _startPanel.SetActive(!_startPanel.activeInHierarchy);
                Time.timeScale = _startPanel.activeInHierarchy ? 0 : 1;
                if (_startPanel.activeInHierarchy)
                {
                    _rePlay.SetActive(true);
                    _play.SetActive(false);
                    _continue.SetActive(false);
                }
            }
        }

        public void OpenSelectPanel()
        {
            characterPanel.SetActive(true);
        }
        public void Play()
        {
            _startPanel.SetActive(false);
            Time.timeScale = 1;
            SaveManager.SaveHighScore(0);
            _playerHealthtxt.text = _playerHealth.ToString();
        }

        public void Quit()
        {
            Application.Quit();
        }

        public void Continue()
        {
            var highScore =  SaveManager.LoadHighScore();
            _playerScoretxt.text = highScore.ToString();
            _playerHealthtxt.text = _playerHealth.ToString();
            _playerScore = highScore;
            _startPanel.SetActive(false);
            Time.timeScale = 1;
        }

        public void RePlay()
        {
            _startPanel.SetActive(false);
            Time.timeScale = 1;
        }
        
        public  void SetPlayerHealth(int damage)
        {
            _playerHealth = damage;
            _playerHealthtxt.text = _playerHealth.ToString();
            ScaleBounce(_playerHealthtxt.transform);
        }

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

        public void SpeedTxt(int speed)
        {
            if (speed==_playerSpeed) return;
            
            _bonesTXT.text = speed+"X";
            _bonesTXT.gameObject.SetActive(true);
            BonesScale();
        }
        private void CheckPrize(int score)
        {
            if (score%100==0)
            {
                _bonesTXT.text = "go on";
                _bonesTXT.gameObject.SetActive(true);
                BonesScale();
            }

            if (score %1000==0)
            {
                _bonesTXT.text =" you're on fire";
                _bonesTXT.gameObject.SetActive(true);
                BonesScale();
            }

            if (score % 10000 == 0)
            {
                _bonesTXT.text = "Legend";
                _bonesTXT.gameObject.SetActive(true);
                BonesScale();
            }
        }

        private void BonesScale()
        {
            Sequence s = DOTween.Sequence();

            _bonesTXT.transform.localScale = Vector3.zero;
            _bonesTXT.gameObject.SetActive(true);

            s.Append(_bonesTXT.transform.DOScale(new Vector3(1.2f, 1.2f, 1f), 0.6f)
                    .SetEase(Ease.OutBack)) // بونس نرم و جذاب برای بزرگ شدن
                .Append(_bonesTXT.transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f)
                    .SetEase(Ease.InOutSine)) // کمی برگشت ملایم برای حس زنده بودن
                .AppendInterval(0.5f) // یه مکث کوچیک
                .Append(_bonesTXT.transform.DOScale(Vector3.zero, 0.3f)
                    .SetEase(Ease.InBack)) // جمع شدن با حالت bounce معکوس
                .OnComplete(() =>
                {
                    _bonesTXT.gameObject.SetActive(false);
                });
        }

        public void SetItemCount(int count)
        {
            _playerItemPic.gameObject.SetActive(count!=0);
            _playerItemtxt.text = count + "X";
            if (_playerItemPic.gameObject.activeInHierarchy)
            {
                ScaleBounce(_playerItemtxt.transform);
            }
        }

       public void ScaleBounce(Transform _transform)
        {
            _transform.DOScale(new Vector3(1.2f, 1.2f, 1f), 0.3f)
                .SetEase(Ease.OutBack).OnComplete((() => _transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f)
                    .SetEase(Ease.InOutSine)));
        }
        public void SwitchBoosted(bool isBoosted)
        {
            _boostedItem.gameObject.SetActive(isBoosted);
            if (isBoosted)
                ScaleBounce(_boostedItem.transform);
        }

        public void SaveData(int reward)
        {
            var highscore = SaveManager.LoadHighScore();
            highscore += reward;
           // PlayerPrefs.SetInt("Highscore", highscore);
            _highScoreTxt.text = highscore.ToString();
        }

        public void ShieldBoosted(float itemDuration)
        {
            StartCoroutine(ShieldCoroutine(itemDuration));

        }

        private IEnumerator ShieldCoroutine(float itemDuration)
        {
            shieldTimerImage.gameObject.SetActive(true);
            float timer = itemDuration;

            while (timer > 0)
            {
                timer -= Time.deltaTime;

                shieldTimerImage.fillAmount = timer / itemDuration;

                yield return null;
            }
            shieldTimerImage.gameObject.SetActive(false);
        }

        public void PowerUpBoosted(float itemDuration)
        {
            StartCoroutine(PowerUpCoroutine(itemDuration));
        }

        private IEnumerator PowerUpCoroutine(float itemDuration)
        {
            powerUpTimerImage.gameObject.SetActive(true);
            float timer = itemDuration;

            while (timer > 0)
            {
                timer -= Time.deltaTime;

                powerUpTimerImage.fillAmount = timer / itemDuration;

                yield return null;
            }
            powerUpTimerImage.gameObject.SetActive(false);
        }

        public void FlyingBoosted(float itemDuration)
        {
            StartCoroutine(FlyingCoroutine(itemDuration));
        }

        private IEnumerator FlyingCoroutine(float itemDuration)
        {
           flyingTimerImage.gameObject.SetActive(true);
            float timer = itemDuration;

            while (timer > 0)
            {
                timer -= Time.deltaTime;

                flyingTimerImage.fillAmount = timer / itemDuration;

                yield return null;
            }
            flyingTimerImage.gameObject.SetActive(false);
        }
    }
    [System.Serializable]
    public class SaveData
    {
        public int highScore;
        public int characterIndex;
    }