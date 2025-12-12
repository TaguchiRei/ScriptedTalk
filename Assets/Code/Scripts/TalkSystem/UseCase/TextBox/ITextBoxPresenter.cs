using System.Collections.Generic;
using ScriptedTalk.TalkSystem.Entity.TalkData;

namespace ScriptedTalk.TalkSystem.UseCase.TextBox
{
    /// <summary>
    /// 表示処理を上位層に委譲
    /// </summary>
    public interface ITextBoxPresenter
    {
        /// <summary> テキストを新規のものに置き換えする </summary>
        void DisplayText(TalkLine fullText);
        
        void DisplaySelection(List<TalkGroup.Selection> choices);
        void OnTalkEnd();
    }
}