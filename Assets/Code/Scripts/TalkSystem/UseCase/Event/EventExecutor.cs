using ScriptedTalk.Code.Scripts.TalkSystem.Entity.Event;
using ScriptedTalk.TalkSystem.UseCase.Character;

/// <summary>
/// 
/// </summary>
public class EventExecutor
{
    public void ExecuteEvent(EventData eventData)
    {
        switch (eventData.EventType)
        {
            case EventType.AnimateBackground:
                AnimateBackground(eventData);
                break;
            case EventType.PlaySound:
                PlaySound(eventData);
                break;
            case EventType.PlayEffect:
                PlayEffect(eventData);
                break;
            case EventType.ChangeBackground:
                ChangeBackGround(eventData);
                break;
        }
    }

    private void AnimateBackground(EventData eventData)
    {
    }

    private void PlaySound(EventData eventData)
    {
        
    }
    private void PlayEffect(EventData eventData)
    {
    }

    private void ChangeBackGround(EventData eventData)
    {
    }
}