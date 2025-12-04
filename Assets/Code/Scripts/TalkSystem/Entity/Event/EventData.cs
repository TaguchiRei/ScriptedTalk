using System;

namespace ScriptedTalk.Code.Scripts.TalkSystem.Entity.Event
{
    /// <summary>
    /// イベントのIDとその対象を保持するクラス
    /// </summary>
    [Serializable]
    public class EventData
    {
        public int EventType { get; private set; }
        public int EventID { get; private set; }
        public int CharacterID { get; private set; }

        public EventData(int eventID, int characterID)
        {
            EventID = eventID;
            CharacterID = characterID;
        }
    }
}