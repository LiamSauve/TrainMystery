using UnityEngine;
using Yarn.Unity;

namespace TrainMystery
{
    public class MainYarnInit : MonoBehaviour
    {
        public YarnProgram yarnProgram;
        private void Start()
        {
            if (yarnProgram != null)
            {
                this.gameObject.GetComponent<DialogueRunner>().Add(yarnProgram);
            }
        }
    } 
}
