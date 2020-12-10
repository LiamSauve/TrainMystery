using Yarn.Unity;

namespace TrainMystery
{
    public class Interactable_NPC : Interactable
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
            TrainMysteryGameManager.Instance.uiCommands.SetFacedObjectLabel(string.Empty);
            FindObjectOfType<DialogueRunner>().StartDialogue(startNode);
        }
    } 
}
