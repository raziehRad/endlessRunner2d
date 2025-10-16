
    using System;
    using DG.Tweening;
    using TMPro;
    using UnityEngine;

    public class HUDManager : MonoBehaviour
    {
        public static HUDManager instace;

        [SerializeField] private TextMeshProUGUI _playerHealthtxt;
        [SerializeField] private TextMeshProUGUI _playerScoretxt;
        [SerializeField] private TextMeshProUGUI _playerItemtxt;
        [SerializeField] private TextMeshProUGUI _boostedItem;
        [SerializeField] private TextMeshProUGUI _bonesTXT;
        
        [SerializeField] private GameObject _startPanel;
        [SerializeField] private GameObject _continue;
        [SerializeField] private GameObject _rePlay;
        [SerializeField] private GameObject _play;
        private int _playerHealth=100;
        private int _playerScore;

        private void Awake()
        {
            instace = this;
            StartSetting();
        }

        private void StartSetting()
        {
            Time.timeScale = 0;
            var highScore = PlayerPrefs.GetInt("Highscore");
            _continue.SetActive(highScore != 0);
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

        public void Play()
        {
            _startPanel.SetActive(false);
            Time.timeScale = 1;
        }

        public void Quit()
        {
            Application.Quit();
        }

        public void Continue()
        {
            var highScore = PlayerPrefs.GetInt("Highscore");
            _playerScoretxt.text ="Score: "+ highScore.ToString();
            _playerScore = highScore;
            Play();
        }

        public void RePlay()
        {
            _startPanel.SetActive(false);
            Time.timeScale = 1;
        }
        
        public  void SetPlayerHealth(int damage)
        {
            _playerHealth -= damage;
            _playerHealthtxt.text = "Health: "+_playerHealth.ToString();
            ScaleBounce(_playerHealthtxt.transform);
        }

        public void SetPlayerScore(int score)
        {
            _playerScore += score;
            _playerScoretxt.text ="Score: "+ _playerScore.ToString();
            SaveScore();
            CheckPrize(_playerScore);
        }

        private void SaveScore()
        {
            var highScore=PlayerPrefs.GetInt("Highscore");
            if (_playerScore>highScore)
            {
                PlayerPrefs.SetInt("Highscore",_playerScore);
            }
        }

        private void CheckPrize(int score)
        {
            if (score%100==0)
            {
                Debug.Log("go On");
                
                _bonesTXT.text = "go on";
                _bonesTXT.gameObject.SetActive(true);
                BonesScale();
            }

            if (score %1000==0)
            {
                Debug.Log("you're on fire");
                _bonesTXT.text =" you're on fire";
                _bonesTXT.gameObject.SetActive(true);
                BonesScale();
            }

            if (score % 10000 == 0)
            {
                Debug.Log("Legend");
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
            _playerItemtxt.gameObject.SetActive(count!=0);
            _playerItemtxt.text = count + "X";
            if (_playerItemtxt.gameObject.activeInHierarchy)
            {
                ScaleBounce(_playerItemtxt.transform);
            }
        }

        void ScaleBounce(Transform _transform)
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
    }
