using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string name;
    [TextArea(5, 20)]
    public string[] sentences;
}
