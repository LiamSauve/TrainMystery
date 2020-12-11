using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TrainMystery
{
    public class Quit : TimeScaleIndependentUpdate
    {
        private bool isQuitting = false;
        private float quitValue = 0;
        public float quitSpeed = 0;

        public Image quitBar;
        public TMPro.TMP_Text quitText;

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Update()
        {
            base.Update();
            if (isQuitting)
                return;

            if(Input.GetKey(KeyCode.Q))
            {
                quitValue += Time.deltaTime * quitSpeed;
                if(quitValue > 1)
                {
                    quitValue = 1;
                    isQuitting = true;
                    quitText.text = "Thanks for playing!";
                    TrainMysteryGameManager.Instance.GameOver_User();
                    quitBar.gameObject.SetActive(false);
                    //DOVirtual.DelayedCall(2, () => Application.Quit());
                }
            }
            else
            {
                quitValue -= Time.deltaTime * quitSpeed;
                if (quitValue < 0) quitValue = 0;
            }
            quitBar.transform.localScale = new Vector3(quitValue, 1f, 1f);
        }
    } 
}
