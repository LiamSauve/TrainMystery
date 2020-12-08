using UnityEngine;
using System.Collections;

namespace TrainMystery
{
    public class Interactable_Radio : Interactable
    {
        [SerializeField]
        private bool _isRadioOn = false;
        [SerializeField]
        private float _currentVolume = 0.0f;
        [SerializeField]
        private float _targetVolume = 0.0f;

        // wwise fields
        private uint radioMusicID;
        private uint radioClickID;

        protected override void Awake()
        {
            _isRadioOn = true;
            radioMusicID = AkSoundEngine.PostEvent("Play_mus_radio", this.gameObject);
            _targetVolume = 1f;
        }

        protected override void Update()
        {
            base.Update();

            if (Input.GetKeyDown(KeyCode.P))
            {
                Interact();
            }

            _currentVolume = Mathf.Lerp(_currentVolume, _targetVolume, 0.1f);
            AkSoundEngine.SetRTPCValue("rtpc_radio_volume", _currentVolume);
        }

        public override void Interact()
        {
            radioClickID = AkSoundEngine.PostEvent("Play_sfx_radio_click", this.gameObject);

            _isRadioOn = !_isRadioOn;
            if (_isRadioOn)
            {
                _targetVolume = 1f;
                return;
            }

            _targetVolume = 0f;
        }
    } 
}
