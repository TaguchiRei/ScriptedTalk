using System;
using System.Collections.Generic;
using ScriptedTalk.TalkSystem.Entity.Character;
using UnityEngine;

[CreateAssetMenu(fileName = "ContextData", menuName = "ScriptableObjects/ContextData")]
public class ContextData : ScriptableObject
{
    public CharacterEntity[] AllCharacters;
    public List<TalkGroupData> Context;
    public string StartGroupGuid;

    public int GetGroupIndex(string readingGroupGuid)
    {
        return Context.FindIndex(item => item.Guid == readingGroupGuid);
    }

    /// <summary>
    /// 指定した行のテキストを取得する。
    /// </summary>
    /// <param name="readingGroup"></param>
    /// <param name="readingLine"></param>
    /// <param name="talkLine"></param>
    /// <returns>falseの場合は最後の行を読み込んだ時</returns>
    public bool TryGetLine(int readingGroup, int readingLine, out TalkLineData talkLine)
    {
        return Context[readingGroup].TryGetLine(readingLine, out talkLine);
    }

    public bool TryGetQuestion(int readingGroup, out List<SelectionData> selections)
    {
        if (Context[readingGroup].IsBranch())
        {
            selections = Context[readingGroup].Selections;
            return true;
        }
        else
        {
            selections = null;
            return false;
        }
    }

    private void OnEnable()
    {
        hideFlags = HideFlags.None;
    }
}

[Serializable]
public class TalkGroupData
{
    public string Guid = System.Guid.NewGuid().ToString();
    public TalkLineData[] TalkLines;

    public List<SelectionData> Selections;

    public bool IsBranch() => Selections != null && TalkLines.Length > 0;

    public bool TryGetLine(int readingLine, out TalkLineData talkLine)
    {
        if (readingLine >= TalkLines.Length)
        {
            talkLine = null;
            return false;
        }

        talkLine = TalkLines[readingLine];
        return readingLine < TalkLines.Length - 1;
    }

#if UNITY_EDITOR
    /// <summary> エディタ専用　触るな </summary>
    public Vector2 Position;
#endif
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
    public string NextGroupGuid;
}