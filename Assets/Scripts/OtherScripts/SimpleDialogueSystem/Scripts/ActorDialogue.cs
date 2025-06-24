using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Actor", menuName = "SO/DataActor")]
public class ActorDialogue : ScriptableObject
{
    [SerializeField] private int id;
    [SerializeField] private string name;
    [SerializeField] private Sprite avatar;

    public int ID => id;
    public string Name => name;
    public Sprite Avatar => avatar;
}

[System.Serializable]
public class DialogueState
{
    [SerializeField] private string stateID;
    [SerializeField] private string nextStateID;

    [SerializeField] private List<DialogueLine> linesInState;

    public string StateID => stateID;
    public string NextStateID => nextStateID;
    public List<DialogueLine> LinesInState => linesInState;
}

[System.Serializable]
public class DialogueLine
{
    [Tooltip("True nếu là lời người chơi, False nếu là NPC")]
    public bool isPlayer;

    [TextArea(2, 4)]
    public List<string> lines = new List<string>();
}
