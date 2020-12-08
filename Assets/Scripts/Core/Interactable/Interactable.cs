using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrainMystery
{
    public abstract class Interactable : TimeScaleIndependentUpdate
    {
        public abstract void Interact();
    } 
}
