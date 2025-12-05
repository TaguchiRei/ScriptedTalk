using ScriptedTalk.Code.Scripts.TalkSystem.Entity.Event;
using ScriptedTalk.TalkSystem.UseCase.Character;

/// <summary>
/// 
/// </summary>
public class EventExecutor
{
    public void ExecuteEvent(EventEntity eventEntity)
    {
        switch (eventEntity.EventType)
        {
            case EventType.AnimateBackground:
                AnimateBackground(eventEntity);
                break;
            case EventType.PlaySound:
                PlaySound(eventEntity);
                break;
            case EventType.PlayEffect:
                PlayEffect(eventEntity);
                break;
            case EventType.ChangeBackground:
                ChangeBackGround(eventEntity);
                break;
        }
    }

    private void AnimateBackground(EventEntity eventEntity)
    {
    }

    private void PlaySound(EventEntity eventEntity)
    {
        
    }
    private void PlayEffect(EventEntity eventEntity)
    {
    }

    private void ChangeBackGround(EventEntity eventEntity)
    {
    }
}