using UnityEngine;

namespace TrainMystery
{
    public class Interactable_Notebook : Interactable
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
            TrainMysteryGameManager.Instance.GotNotebook();
            GameObject.Destroy(this.gameObject);
        }
    }
}
