
    using TMPro;
    using UnityEngine;

    public class HUDGameMenu : MonoBehaviour
    {
       
        [SerializeField] protected GameObject _startPanel;
        [SerializeField] protected GameObject characterPanel;
        [ SerializeField]protected TextMeshProUGUI _playerScoretxt;
        [SerializeField] protected GameObject _continue;
        [SerializeField] protected GameObject _rePlay;
        [SerializeField] protected GameObject _play;
        private void Awake()
        {
            StartSetting();
        }
        private void StartSetting()
        {
            Time.timeScale = 0;
            var highScore =  SaveManager.LoadHighScore();
            GameEvents.OnScoreChanged?.Invoke(highScore);
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
            GameEvents.OnHealthChanged?.Invoke(100);
           // _playerHealthtxt.text =100.ToString();
        }

        public void Quit()
        {
            Application.Quit();
        }

        public void Continue()
        {
            var highScore =  SaveManager.LoadHighScore();
            _playerScoretxt.text = highScore.ToString();
            GameEvents.OnHealthChanged?.Invoke(100);
            GameEvents.OnScoreChanged?.Invoke(highScore);
            _startPanel.SetActive(false);
            Time.timeScale = 1;
        }

        public void RePlay()
        {
            _startPanel.SetActive(false);
            Time.timeScale = 1;
        }
    }
