using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrainMystery
{
    public class TreeEmitter : TimeScaleIndependentUpdate
    {
        [SerializeField]
        private GameObject tree;
        [SerializeField]
        private float distance;

        private float start;

        public bool disabled = false;

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            start = tree.transform.localPosition.z;
            MoveTree();
        }

        private void MoveTree()
        {
            Sequence sequence = DOTween.Sequence();

            sequence.PrependInterval(Random.value);
            sequence.Append(DOVirtual.DelayedCall(0,  () =>  {
                tree.transform.localPosition = Vector3.zero; } ));
            sequence.Append(tree.transform.DOMoveZ(distance, 2).SetEase(Ease.Linear));
            sequence.SetLoops(-1, LoopType.Restart);
        }

        public void End()
        {
            AkSoundEngine.StopAll(tree);
            Destroy(tree);
            Destroy(this.gameObject);
        }
    } 
}
