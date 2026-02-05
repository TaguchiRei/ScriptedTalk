using System.Collections.Generic;
using ScriptedTalk;
using UnityEngine;

public class TalkRunner
{
    private TalkRuntimeModel _trm;
    private ITextView _textView;
    private ISelectionView _selectionView;

    private IBackgroundView _backgroundView;
    private ICharacterView _characterView;
    private IEffectView _effectView;
    private ISoundSystem _soundSystem;

    private List<IEvent> _playingEvents;

    private TalkState _talkState;

    public TalkRunner(
        TalkRuntimeModel trm,
        ITextView textView,
        ISelectionView selectionView,
        IBackgroundView backgroundView,
        ICharacterView characterView,
        IEffectView effectView,
        ISoundSystem soundSystem)
    {
        _trm = trm;
        _textView = textView;
        _selectionView = selectionView;
        _backgroundView = backgroundView;
        _characterView = characterView;
        _effectView = effectView;
        _soundSystem = soundSystem;
        _talkState = TalkState.Talking;
        _playingEvents = new List<IEvent>();
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
        }
        else if (_talkState != TalkState.DisableOperation)
        {
            Debug.Log("GetLine");
            var next = _trm.TryGetNextLine(out var textData);

            var name = textData.HighLightCharacterName;
            if (name == "None") name = string.Empty;
            Debug.Log("AnimationText");
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

                if (talkEvent is IRequireSoundSystem needSoundSystem)
                {
                    needSoundSystem.SetSoundView(_soundSystem);
                }

                if (talkEvent is IRequireCharacterView requireCharacterView)
                {
                    requireCharacterView.SetCharacterView(_characterView);
                }

                if (talkEvent is IRequireSelectView requireSelectView)
                {
                    requireSelectView.SetSelectView(_selectionView);
                }

                #endregion

                _playingEvents.Add(talkEvent);
                talkEvent.EndAction = () => _playingEvents.Remove(talkEvent);
                talkEvent.Execute();
            }

            if (!next)
            {
                if (_trm.IsBranch())
                {
                    _talkState = TalkState.DisableOperation;
                    return TalkState.Question;
                }
                else
                {
                    _talkState = TalkState.EndTalk;
                }
            }
        }

        return _talkState;
    }

    public void AnsweredQuestion(int selectionNumber)
    {
        _trm.SelectNextGroup(selectionNumber);
        _talkState = TalkState.Talking;
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

        /// <summary> 操作を無効化する </summary>
        DisableOperation
    }
}