using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TrainMystery
{
    public class Introduction : TimeScaleIndependentUpdate
    {
        //public GameObject introductionGameObject;
        public Image bg;
        public TMP_Text title;
        public TMP_Text credits;
        public TMP_Text introText;

        protected override void Update()
        {
            base.Update();
        }

        public void Begin()
        {
            ShowTitle();
        }

        private void ShowTitle()
        {
            title.DOColor(Color.white, 2f)
                .OnComplete(HideTitle);
        }

        private void HideTitle()
        {
            title.DOColor(Color.black, 2f)
                .SetDelay(3)
                .OnComplete(ShowMainText);
        }

        private void ShowMainText()
        {
            // replace this with text roll in
            // then call HideMainText()
            introText.DOColor(Color.white, 2f)
                    .SetDelay(1)
                    .OnComplete(HideMainText);
        }
        
        private void HideMainText()
        {
            introText.DOColor(Color.black, 2f)
                    .SetDelay(5)
                    .OnComplete(FadeFromBlack);
        }

        private void FadeFromBlack()
        {
            title.gameObject.SetActive(false);
            credits.gameObject.SetActive(false);
            introText.gameObject.SetActive(false);
            bg.DOColor(Color.clear, 2f)
                .SetDelay(1)
                .OnComplete(EndAll);
        }

        private void EndAll()
        {
            TrainMysteryGameManager.Instance.EndIntroduction();
        }

        //private void ShowCredits()
        //{
        //
        //}
        //
        //private void HideCredits()
        //{
        //
        //}
        //
        //private void GunShotSound()
        //{
        //
        //}
        //
        //private void ShowExclaimations()
        //{
        //
        //}
    }
}
