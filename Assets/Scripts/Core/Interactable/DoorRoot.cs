using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrainMystery
{
    public class DoorRoot : TimeScaleIndependentUpdate
    {
        public GameObject door;

        public bool _isOpened;
        public float _doorOpen = 3f;

        void OnTriggerExit(Collider other)
        {
            Debug.Log("No longer in contact with " + other.transform.name);
            if(other.gameObject.GetComponent<Player>())
            {
                if(_isOpened == false)
                {
                    return;
                }
                AkSoundEngine.PostEvent("Play_sfx_door_open", door.gameObject);
                _isOpened = false;
            }
        }

        protected override void Update()
        {
            var targetX = 0f;
            if(_isOpened)
            {
                targetX = _doorOpen;
            }

            var newPos = new Vector3(targetX, door.transform.position.y, door.transform.position.z);
            door.transform.position = Vector3.Lerp(door.transform.position, newPos, 0.1f);
        }
    } 
}
