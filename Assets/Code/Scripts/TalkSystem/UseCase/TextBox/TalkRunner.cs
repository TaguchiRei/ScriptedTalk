using ScriptedTalk;

public class TalkRunner
{
    private TalkRuntimeModel _trm;
    private ITextView _view;

    public TalkRunner(TalkRuntimeModel trm, ITextView view)
    {
        _trm = trm;
        _view = view;
    }

    public void StartTalk()
    {
        _view.ShowTextBox();
    }

    /// <summary>
    /// 次に進むボタンを押したときの処理
    /// </summary>
    public void OnNextButtonInput()
    {
        if (_view.TextAnimation)
        {
            _view.SkipAnimation();
        }
        else
        {
            _trm.TryGetNextLine(out var textData);
            var name = textData.HighLightCharacterName;
            if (name == "None") name = string.Empty;
            _view.AnimationText(name, textData.Text, textData.TextShowSpeed);
        }
    }
}