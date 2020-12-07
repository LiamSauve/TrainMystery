using Yarn.Unity;

namespace TrainMystery
{
    public class NPC : Interactable
    {
        public string startNode;
        public YarnProgram yarnProgram;

        private void Start()
        {
            if (yarnProgram != null)
            {
                DialogueRunner dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
                dialogueRunner.Add(yarnProgram);
            }
        }

        public override void Interact()
        {
            FindObjectOfType<DialogueRunner>().StartDialogue(startNode);
        }
    } 
}
