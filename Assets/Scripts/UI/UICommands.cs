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

        public void SetFacedObjectLabel(string facedObjectName)
        {
            _facedObjectLabel.text = facedObjectName;
        }
    }
}