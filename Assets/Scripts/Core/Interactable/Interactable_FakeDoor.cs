using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrainMystery
{
    public class Interactable_FakeDoor : Interactable
    {
        public override void Interact()
        {
            AkSoundEngine.PostEvent("Play_sfx_door_jiggle", this.gameObject);
        }
    } 
}
