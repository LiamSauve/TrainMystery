using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrainMystery
{
    public class Interactable_Door : Interactable
    {
        public override void Interact()
        {
            if (transform.GetComponentInParent<DoorRoot>()._isOpened)
                return;

            AkSoundEngine.PostEvent("Play_sfx_door_open", this.gameObject);
            transform.GetComponentInParent<DoorRoot>()._isOpened = true;
        }
    }
}