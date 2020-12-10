using UnityEngine;

namespace TrainMystery
{
    public class Interactable_Gun : Interactable
    {

        //protected override void Awake()
        //{
        //}
        //
        //protected override void Update()
        //{
        //}

        public override void Interact()
        {
            // play equip sound, destroy self
            TrainMysteryGameManager.Instance.GotGun();
            GameObject.Destroy(this.gameObject);
        }
    }
}
