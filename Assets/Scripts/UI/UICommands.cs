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

        public void SetFacedObjectLabel(string facedObjectName)
        {
            _facedObjectLabel.text = facedObjectName;
        }

        public void AddNote(string note)
        {
            if (TrainMysteryGameManager.Instance.yarnVariables.GetValue("$NotebookEquipped").AsBool) // otherwise we don't add the note :P
            {
                _notebookLabel.text += "\n - " + note;
            }
        }
    }
}