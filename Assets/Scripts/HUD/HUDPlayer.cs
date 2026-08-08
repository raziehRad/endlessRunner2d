
    using System;
    using DG.Tweening;
    using TMPro;
    using UnityEngine;

    public class HUDPlayer : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI _playerHealthtxt;
        [SerializeField] protected GameObject _playerItemPic;
        [SerializeField] protected TextMeshProUGUI _playerItemtxt;
        [SerializeField] protected TextMeshProUGUI _bonesTXT;
        private HUDAnimation _HUDAnimate;
        private void Awake()
        {
            _HUDAnimate = GetComponent<HUDAnimation>();
        }

        public void SetPlayerHealth(int damage)
        {
          //  _playerHealth = damage;
            //_playerHealthtxt.text = _playerHealth.ToString();
            _playerHealthtxt.text = damage.ToString();
            _HUDAnimate.ScaleBounce(_playerHealthtxt.transform);
        }
        public void SetItemCount(int count)
        {
            _playerItemPic.gameObject.SetActive(count!=0);
            _playerItemtxt.text = count + "X";
            if (_playerItemPic.gameObject.activeInHierarchy)
            {
                _HUDAnimate.ScaleBounce(_playerItemtxt.transform);
            }
        }
        public void SpeedTxt(float speed)
        {
           // if (speed==_playerSpeed) return;
           _bonesTXT.text =(int) speed+"X";
            _bonesTXT.gameObject.SetActive(true);
            _HUDAnimate.BonesScale(_bonesTXT.transform);
            _HUDAnimate. BonesScale();
        }
    }
