using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace ScriptedTalk.TalkSystem.UseCase.TextBox
{
    /// <summary>
    /// テキストボックスを管理する
    /// </summary>
    public class TextBox
    {
        ITalkRepository _talkRepository;

        public TextBox(ITalkRepository talkRepository)
        {
            _talkRepository = talkRepository;
        } 
    }
}