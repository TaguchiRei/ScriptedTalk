namespace ScriptedTalk.Code.Scripts.TalkSystem.Entity.Event
{
    public class EventData
    {
        public TalkEvent TalkEvents{ get; private set; }
    }

    public class TalkEvent
    {
        public int EventID { get; private set; }
        public int CharacterID { get; private set; }

        public TalkEvent(int eventID, int characterID)
        {
            EventID = eventID;
            CharacterID = characterID;
        }
    }
}