﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrainMystery
{
    public class TrainMysterySoundbankLoader : MonoBehaviour
    {
        private uint initSoundbankID;
        private uint mainSoundbankID;
        void Awake()
        {
            AkSoundEngine.LoadBank("Init", out initSoundbankID);
            AkSoundEngine.LoadBank("Main", out mainSoundbankID);
        }
    } 
}
