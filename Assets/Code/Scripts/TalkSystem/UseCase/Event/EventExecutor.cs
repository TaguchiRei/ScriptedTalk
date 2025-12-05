using ScriptedTalk.Code.Scripts.TalkSystem.Entity.Event;
using ScriptedTalk.TalkSystem.UseCase.Character;
using ScriptedTalk.TalkSystem.UseCase.Event;
using Xenosite.System.GlobalService.Interface;

/// <summary>
/// 
/// </summary>
public class EventExecutor
{
    private IEventRepository _eventRepository;
    private IBackGroundView _backGroundView;
    private ISoundSystem _soundSystem;
    private IBGMSystem _bgmSystem;
    

    public EventExecutor(IEventRepository eventRepository)
    {
    }

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
        var anim = _eventRepository.GetAnimation(eventEntity.EventID);
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