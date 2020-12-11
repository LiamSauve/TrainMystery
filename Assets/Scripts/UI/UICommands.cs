using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TrainMystery
{
    public class UICommands : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _facedObjectLabel;
        [SerializeField]
        private TMP_Text _notebookLabel;
        [SerializeField]
        private GameObject _notebookEquipInput;
        [SerializeField]
        private GameObject _gunEquipInput;
        [SerializeField]
        private GameObject _gunShootInput;
        [SerializeField]
        private TMP_Text _timerLabel;

        public void SetFacedObjectLabel(string facedObjectName)
        {
            _facedObjectLabel.text = facedObjectName;
        }

        public void AddNote(string note)
        {
            if (TrainMysteryGameManager.Instance.yarnVariables.GetValue("$NotebookEquipped").AsBool) // otherwise we don't add the note :P
            {
                AkSoundEngine.PostEvent("Play_sfx_notebook_write", Camera.main.gameObject);
                _notebookLabel.text += "\n - " + note;
            }
        }

        public void ShowNotebookInput(bool active)
        {
            _notebookEquipInput.gameObject.SetActive(active);
        }

        public void ShowGunInput(bool active)
        {
            _gunEquipInput.gameObject.SetActive(active);
        }

        public void ShowShootInput(bool active)
        {
            _gunShootInput.gameObject.SetActive(active);
        }

        public void ShowTimer(bool active)
        {
            _timerLabel.gameObject.SetActive(active);
        }

        public void UpdateTimer(float time)
        {
            int minutes = Mathf.FloorToInt(time / 60F);
            int seconds = Mathf.FloorToInt(time - minutes * 60);
            string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);
            if(time < 59.9)
            {
                _timerLabel.color = Color.red;
            }
            _timerLabel.text = niceTime;
        }
    }
}