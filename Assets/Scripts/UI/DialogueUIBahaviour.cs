using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TrainMystery
{
    public class DialogueUIBahaviour : MonoBehaviour
    {
        private void OnGUI()
        {
            EventSystem.current.SetSelectedGameObject(this.gameObject);
        }
    } 
}
