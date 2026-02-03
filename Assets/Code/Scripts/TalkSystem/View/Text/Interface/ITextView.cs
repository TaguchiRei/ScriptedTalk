public interface ITextView
{
    public bool TextAnimation { get; }

    public void ShowFullText(string characterName, string text);

    public void AnimationText(string characterName, string text, int textShowSpeed);

    public void SkipAnimation();

    public void ShowTextBox();

    public void HideTextBox();
}