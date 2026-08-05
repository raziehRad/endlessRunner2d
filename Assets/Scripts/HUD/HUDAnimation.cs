using DG.Tweening;
using TMPro;
using UnityEngine;
    public class HUDAnimation : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI _bonesTXT;
        public void ScaleBounce(Transform _transform)
        {
            _transform.DOScale(new Vector3(1.2f, 1.2f, 1f), 0.3f)
                .SetEase(Ease.OutBack).OnComplete((() => _transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f)
                    .SetEase(Ease.InOutSine)));
        }
        public void BonesScale(Transform target)
        {
            Sequence s = DOTween.Sequence();

            target.localScale = Vector3.zero;
            target.gameObject.SetActive(true);

            s.Append(target.DOScale(1.2f,0.6f).SetEase(Ease.OutBack))
                .Append(target.DOScale(1f,0.2f))
                .AppendInterval(0.5f)
                .Append(target.DOScale(0,0.3f).SetEase(Ease.InBack))
                .OnComplete(()=> target.gameObject.SetActive(false));
        }
        public void BonesScale()
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
    }
