using System.Collections.Generic;
using ScriptedTalk;

public class TalkRunner
{
    private TalkRuntimeModel _trm;
    private ITextView _textView;
    private ISelectionView _selectionView;

    private IBackgroundView _backgroundView;
    private ICharacterView _characterView;
    private IEffectView _effectView;

    private List<IEvent> _playingEvents;

    public TalkRunner(TalkRuntimeModel trm, ITextView textView)
    {
        _trm = trm;
        _textView = textView;
    }

    public void StartTalk()
    {
        _textView.ShowTextBox();
        OnNextButtonInput();
    }

    /// <summary>
    /// 次に進むボタンを押したときの処理
    /// </summary>
    public TalkState OnNextButtonInput()
    {
        if (_textView.TextAnimation)
        {
            _textView.SkipAnimation();
            foreach (var talkEvent in _playingEvents)
            {
                if (talkEvent == null) continue;
                talkEvent.Skip();
            }

            _playingEvents.Clear();

            return TalkState.Talking;
        }
        else
        {
            var next = _trm.TryGetNextLine(out var textData);
            var name = textData.HighLightCharacterName;
            if (name == "None") name = string.Empty;
            _textView.AnimationText(name, textData.Text, textData.TextShowSpeed);

            foreach (var talkEvent in textData.Events)
            {
                #region 必要なインターフェースをセット

                if (talkEvent is IRequireTextView needTextView)
                {
                    needTextView.SetTextView(_textView);
                }

                if (talkEvent is IRequireBackgroundView needBackgroundView)
                {
                    needBackgroundView.SetBackgroundView(_backgroundView);
                }

                if (talkEvent is IRequireCharacterView needCharacterView)
                {
                    needCharacterView.SetCharacterView(_characterView);
                }

                if (talkEvent is IRequireEffectView needEffectView)
                {
                    needEffectView.SetEffectView(_effectView);
                }

                #endregion

                _playingEvents.Add(talkEvent);
                talkEvent.EndAction = () => _playingEvents.Remove(talkEvent);
                talkEvent.Execute();
            }

            if (next) return TalkState.Talking;
            if (_trm.IsBranch())
            {
                return TalkState.Question;
            }
            else
            {
                return TalkState.EndTalk;
            }
        }
    }

    public void AnsweredQuestion(int selectionNumber)
    {
        _trm.SelectNextGroup(selectionNumber);
        OnNextButtonInput();
    }

    public enum TalkState
    {
        /// <summary> 会話中。次も会話が続く </summary>
        Talking,

        /// <summary> 一つのグループが終わり、質問をする必要があるとき </summary>
        Question,

        /// <summary> 会話終了 </summary>
        EndTalk,
    }
}