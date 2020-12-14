using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrainMystery
{
    public class Interactable_Door : Interactable
    {
        public override void Interact()
        {
            transform.GetComponentInParent<DoorRoot>()._isOpened = true;
        }
    }
}