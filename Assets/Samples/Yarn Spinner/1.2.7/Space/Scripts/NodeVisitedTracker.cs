using System.Collections;
using System.Collections.Generic;
using TrainMystery;
using UnityEngine;

// Field ... is never assigned to and will always have its default value null
#pragma warning disable 0649

public class NodeVisitedTracker : MonoBehaviour
{

    // The dialogue runner that we want to attach the 'visited' function to
#pragma warning disable 0649
    [SerializeField] Yarn.Unity.DialogueRunner dialogueRunner;
    [SerializeField] CharacterData characterData;
#pragma warning restore 0649

    private HashSet<string> _visitedNodes = new HashSet<string>();

    void Start()
    {
        // Register a function on startup called "visited" that lets Yarn
        // scripts query to see if a node has been run before.
        dialogueRunner.AddFunction("visited", 1, delegate (Yarn.Value[] parameters)
        {
            var nodeName = parameters[0];
            var index = (int)TrainMysteryGameManager.Instance.yarnVariables.GetValue("$convid").AsNumber;
            var charName = characterData.dialogueStrings[index].name;
            var entryName = nodeName.AsString + "_" + charName;

            return _visitedNodes.Contains(entryName);
        });

    }

    // Called by the Dialogue Runner to notify us that a node finished
    // running. 
    public void NodeComplete(string nodeName) {
        // Log that the node has been run.
        var index = (int)TrainMysteryGameManager.Instance.yarnVariables.GetValue("$convid").AsNumber;
        var charName = characterData.dialogueStrings[index].name;
        var entryName = nodeName + "_" + charName;

        _visitedNodes.Add(entryName);
    }


	// Called by the Dialogue Runner to notify us that a new node 
	// started running. 
	public void NodeStart(string nodeName) {
		// Log that the node has been run.

        var tags = new List<string>(dialogueRunner.GetTagsForNode(nodeName));
        
		//Debug.Log($"Starting the execution of node {nodeName} with {tags.Count} tags.");
	}

}
