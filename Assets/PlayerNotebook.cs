using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrainMystery
{
    public class PlayerNotebook : TimeScaleIndependentUpdate
    {
        [SerializeField] private float _bobAmountX;
        [SerializeField] private float _bobAmountY;
        [SerializeField] private float _bobAmountZ;
        [SerializeField] private float _bobFrequencyX;
        [SerializeField] private float _bobFrequencyY;
        [SerializeField] private float _bobFrequencyZ;

        private float _startX;
        private float _startY;
        private float _startZ;

        private float timer = 0f;

        private float equipOffsetYPosition = -0.5f;
        private float equipSpeed = 0.05f;

        public bool isEquipped { get; private set; }

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
        }

        protected override void Update()
        {
            base.Update();

            timer += deltaTime;
            var newX = Oscillate(timer, _bobFrequencyX, _bobAmountX) + _startX;
            var newY = Oscillate(timer, _bobFrequencyY, _bobAmountY) + _startY;
            var newZ = Oscillate(timer, _bobFrequencyZ, _bobAmountZ) + _startZ;
            this.transform.localPosition = new Vector3(newX, newY + equipOffsetYPosition, newZ);

            // set notebook down
            if (isEquipped)
            {
                equipOffsetYPosition = Mathf.Lerp(equipOffsetYPosition, 0, 0.05f);
            }
            else
            {
                equipOffsetYPosition = Mathf.Lerp(equipOffsetYPosition, -0.5f, 0.05f);
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
                //AkSoundEngine.PostEvent("Play_sfx_gun_equip", Camera.main.gameObject); // make book equip sound
            }
        }
    }
}
