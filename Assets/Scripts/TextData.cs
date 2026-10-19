using UnityEngine;

[CreateAssetMenu(fileName = "NewTextData", menuName = "TextData")]
public class TextData : ScriptableObject
{
    [TextArea(3, 10)]
    public string text;
}
