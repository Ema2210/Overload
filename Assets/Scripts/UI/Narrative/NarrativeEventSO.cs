using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewNarrativeEvent", menuName = "Narrative")]
public class NarrativeEventSO : ScriptableObject
{
    [field: SerializeField] public string EventName { get; private set; }

    [SerializeField, TextArea(3, 10)]
    string text;

    public string Text { get => text; } 


[field: SerializeField] public Sprite Art { get; private set; }

}
