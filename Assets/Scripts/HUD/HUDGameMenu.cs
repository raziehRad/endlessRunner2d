
    using System;
    using System.Collections;
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
        [SerializeField] protected TextMeshProUGUI _highScoreTxt;
        [SerializeField] protected TextMeshProUGUI _ErorTxt;
        
        private bool canStart=false;
        private void Awake()
        {
            StartSetting();
        }

        private void OnEnable()
        {
            GameEvents.OnStartGame += StartGame;
        }


        private void StartGame()
        {
            canStart = true;
        }

        // Set the initial menu state and load saved data
        private void StartSetting()
        {
            Time.timeScale = 0;
            var highScore =  SaveManager.LoadHighScore();
            _highScoreTxt.text = highScore.ToString();
            GameEvents.OnSetScore?.Invoke(highScore);
            _continue.SetActive(highScore != 0);
            GameEvents.OnSetCharacter?.Invoke();
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
            if (!canStart)
            {
                StartCoroutine(SetErrorPanel());
                return;
            }

            _startPanel.SetActive(false);
            Time.timeScale = 1;
            SaveManager.SaveHighScore(0);
            GameEvents.OnSetScore?.Invoke(0);
            GameEvents.OnHealthChanged?.Invoke(100);
           // _playerHealthtxt.text =100.ToString();
        }

        private IEnumerator SetErrorPanel()
        {
            _ErorTxt.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _ErorTxt.gameObject.SetActive(false);
        }

        public void Quit()
        {
            Application.Quit();
        }
        public void Continue()
        {
            if (!canStart)
            {
                StartCoroutine(SetErrorPanel());
                return;
            }

            var highScore =  SaveManager.LoadHighScore();
            _playerScoretxt.text = highScore.ToString();
            GameEvents.OnHealthChanged?.Invoke(100);
            GameEvents.OnSetScore?.Invoke(highScore);
            _startPanel.SetActive(false);
            Time.timeScale = 1;
        }

        public void RePlay()
        {
            _startPanel.SetActive(false);
            Time.timeScale = 1;
        }
    }
