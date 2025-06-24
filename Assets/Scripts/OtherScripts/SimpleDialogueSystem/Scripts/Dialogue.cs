using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "SO/Dialogue")]
public class Dialogue : ScriptableObject
{
    [SerializeField] private ActorDialogue player;
    [SerializeField] private ActorDialogue npc;
    [SerializeField] private List<DialogueState> states;
    [SerializeField] private string initialStateID;

    public ActorDialogue Player => player;
    public ActorDialogue NPC => npc;
    public List<DialogueState> States => states;
    public string InitialStateID => initialStateID;

    public DialogueState GetState(string stateID)
    {
        return states.Find(state => state.StateID == stateID);
    }
}