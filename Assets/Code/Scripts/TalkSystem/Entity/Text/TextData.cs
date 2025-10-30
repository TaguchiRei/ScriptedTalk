namespace ScriptedTalk.Code.Scripts.TalkSystem.Entity.Text
{
    /// <summary>
    /// 会話文のデータ
    /// </summary>
    public class TextData 
    {
        public string Text { get; private set; }
        public int CharacterID { get; private set; }

        public TextData(string text, int characterID)
        {
            Text = text;
            CharacterID = characterID;
        }
    }
}
