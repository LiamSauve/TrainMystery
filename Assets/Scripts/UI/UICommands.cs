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
        private GameObject[] _notebookLabels;
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
        [SerializeField]
        private CharacterData _characterData;

        public void SetFacedObjectLabel(string facedObjectName)
        {
            _facedObjectLabel.text = facedObjectName;
        }

        public void AddNote(int index, string note)
        {
            if (TrainMysteryGameManager.Instance.yarnVariables.GetValue("$NotebookEquipped").AsBool) // otherwise we don't add the note :P
            {
                // get the right string,
                var oldString = _characterData.notebookPages[index];

                // add the note
                var newNote = oldString + "\n - " + note;
                _characterData.notebookPages[index] = newNote;

                // display the new one
                _notebookLabel.text = newNote;
                AkSoundEngine.PostEvent("Play_sfx_notebook_write", Camera.main.gameObject);
                if(TrainMysteryGameManager.Instance.GetPlayer().notebookPage != index)
                {
                    // will call set page
                    TrainMysteryGameManager.Instance.GetPlayer().SetPageIndex(index);
                }
            }
        }

        public void SetPage(int index)
        {
            _notebookLabel.text = _characterData.notebookPages[index];
            AkSoundEngine.PostEvent("Play_sfx_notebook_equip", Camera.main.gameObject);
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