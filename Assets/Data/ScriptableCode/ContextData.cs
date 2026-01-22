using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ContextData", menuName = "ScriptableObjects/ContextData")]
public class ContextData : ScriptableObject
{
    public List<TalkGroupData> Context;
}

[Serializable]
public class TalkGroupData
{
    public TalkLineData[] TalkLines;

    public List<SelectionData> Selections;
}

[Serializable]
public class TalkLineData
{
    public string Text;
    public int HighLightCharacterID;
    public int TextShowDuration;
    [SerializeReference, SubclassSelector] public IEvent[] Events;
}

[Serializable]
public class SelectionData
{
    public string SelectionTitle;
    public int NextGroupID;
}