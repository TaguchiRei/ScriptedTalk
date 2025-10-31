using ScriptedTalk.TalkSystem.Entity.TalkData;

namespace ScriptedTalk.TalkSystem.UseCase.TextBox
{
    /// <summary>
    /// 表示処理を上位層に委譲
    /// </summary>
    public interface ITextBoxView
    {
        /// <summary> テキストを新規のものに置き換えする </summary>
        void DisplayText(string fullText);

        /// <summary> テキストの表示文字数を変更する </summary>
        void DisplayTextUpdate(int charCount);
        void DisplaySelection(TalkGroup.Selection[] choices);
        void OnTalkEnd();
    }
}