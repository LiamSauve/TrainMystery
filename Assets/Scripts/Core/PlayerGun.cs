using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrainMystery
{
    public class PlayerGun : TimeScaleIndependentUpdate
    {
        [SerializeField] private float _bobAmountX;
        [SerializeField] private float _bobAmountY;
        [SerializeField] private float _bobAmountZ;
        [SerializeField] private float _bobFrequencyX;
        [SerializeField] private float _bobFrequencyY;
        [SerializeField] private float _bobFrequencyZ;

        [SerializeField] private float equipSpeed = 0.2f;

        private float _startX;
        private float _startY;
        private float _startZ;

        private float timer = 0f;

        private float equipOffsetYPosition = -0.5f;
        private float equipOffsetZRotate = 60f;

        public bool isEquipped { get; private set; }
        private bool isShooting = false;

        protected override void Awake()
        {
            base.Awake();

            _startX = transform.localPosition.x;
            _startY = transform.localPosition.y;
            _startZ = transform.localPosition.z;
        }

        private void Start()
        {
            this.transform.localPosition = new Vector3(_startX, _startY + equipOffsetYPosition, _startZ);
            this.transform.localRotation = Quaternion.Euler(0, 90, equipOffsetZRotate);
        }

        protected override void Update()
        {
            base.Update();

            if (isShooting) return;

            timer += deltaTime;
            var newX = Oscillate(timer, _bobFrequencyX, _bobAmountX) + _startX;
            var newY = Oscillate(timer, _bobFrequencyY, _bobAmountY) + _startY;
            var newZ = Oscillate(timer, _bobFrequencyZ, _bobAmountZ) + _startZ;
            this.transform.localPosition = new Vector3(newX, newY + equipOffsetYPosition, newZ);
            this.transform.localRotation = Quaternion.Euler(0, 90, equipOffsetZRotate);

            // set weapon down
            if(isEquipped)
            {
                equipOffsetYPosition = Mathf.Lerp(equipOffsetYPosition, 0, equipSpeed);
                equipOffsetZRotate = Mathf.Lerp(equipOffsetZRotate, 0, equipSpeed);
            }
            else
            {
                equipOffsetYPosition = Mathf.Lerp(equipOffsetYPosition, -0.5f, equipSpeed);
                equipOffsetZRotate = Mathf.Lerp(equipOffsetZRotate, 60, equipSpeed);
            }
        }

        private float Oscillate(float time, float speed, float scale)
        {
            return Mathf.Cos(time * speed / Mathf.PI) * scale;
        }

        public void Equip(bool equip)
        {
            isEquipped = equip;
            if (isEquipped)
            {
                AkSoundEngine.PostEvent("Play_sfx_gun_equip", Camera.main.gameObject);
            }
            TrainMysteryGameManager.Instance.yarnVariables.SetValue("$GunEquipped", isEquipped);
            TrainMysteryGameManager.Instance.uiCommands.ShowShootInput(isEquipped);
        }

        public void Shoot()
        {
            AkSoundEngine.PostEvent("Play_sfx_gun_shoot", Camera.main.gameObject);
            PlayShootAnim();
        }

        private void PlayShootAnim()
        {
            isShooting = true;
            transform.localPosition = new Vector3(_startX, _startY, _startZ);

            var startZ = transform.localPosition.z;
            Sequence sequence = DOTween.Sequence();
            sequence.Append(transform.DOLocalMoveZ(transform.localPosition.z - 0.05f, 0.075f).SetEase(Ease.Linear));
            sequence.Append(transform.DOLocalMoveZ(startZ, 0.075f).SetEase(Ease.Linear));
            sequence.AppendCallback(EndShootAnim);
        }

        private void EndShootAnim()
        {
            isShooting = false;
        }
    } 
}
