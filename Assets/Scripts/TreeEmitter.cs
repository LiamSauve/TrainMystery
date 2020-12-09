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

        protected override void Awake()
        {
            base.Awake();

            MoveTree();
        }

        private void Start()
        {
            start = tree.transform.localPosition.z;
        }

        private void MoveTree()
        {
            tree.transform.localPosition = Vector3.zero;

            Sequence sequence = DOTween.Sequence();
            sequence.PrependInterval(Random.value);
            sequence.Append(tree.transform.DOMoveZ(distance, 2).SetEase(Ease.Linear));
            sequence.AppendCallback(MoveTree);
        }
    } 
}
