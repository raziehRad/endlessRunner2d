
    using DG.Tweening;
    using TMPro;
    using UnityEngine;

    public class HUDPlayer : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI _playerHealthtxt;
        [SerializeField]protected HUDAnimation _HUDAnimate;
        [SerializeField] protected GameObject _playerItemPic;
        [SerializeField] protected TextMeshProUGUI _playerItemtxt;
        [SerializeField] protected TextMeshProUGUI _bonesTXT;
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
        public void SpeedTxt(int speed)
        {
           // if (speed==_playerSpeed) return;
            
           
            _bonesTXT.text = speed+"X";
            _bonesTXT.gameObject.SetActive(true);
            _HUDAnimate.BonesScale(_bonesTXT.transform);
            _HUDAnimate. BonesScale();
        }
      
        

    }
