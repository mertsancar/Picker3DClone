using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI.Screens
{
    public class StageSuccessScreen : BaseScreen
    {
        [SerializeField] private Transform popup;
        [SerializeField] private TMP_Text popupText;
        private List<string> contents = new List<string> { "Awesome", "Perfect", "Nice" };
        public override void Prepare(object param)
        {
            popupText.text = contents[Random.Range(0, contents.Count)];
            
            var seq = DOTween.Sequence();
            seq.AppendCallback(() => popup.DOScale(new Vector3(100, 100, 100), 1f).SetEase(Ease.InBounce));
            seq.AppendInterval(2f);
            seq.AppendCallback(() => popup.DOScale(new Vector3(0, 0, 0), 1.25f).SetEase(Ease.InOutBounce));
            seq.AppendInterval(1f);
            seq.OnComplete(HideScreen);
        }
    }
    
}
