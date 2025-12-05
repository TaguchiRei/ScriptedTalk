using ScriptedTalk.Code.Scripts.TalkSystem.Entity.Event;
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

    public EventExecutor(
        IEventRepository eventRepository,
        IBackGroundView backGroundView,
        ISoundSystem soundSystem,
        IBGMSystem bgmSystem)
    {
        _eventRepository = eventRepository;
        _backGroundView = backGroundView;
        _soundSystem = soundSystem;
        _bgmSystem = bgmSystem;
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
        _backGroundView.AnimateBackground(anim);
    }

    private void PlaySound(EventEntity eventEntity)
    {
        var sound = _eventRepository.GetSound(eventEntity.EventID, out var volume);
        _soundSystem.PlaySoundEffect(sound, volume);
    }

    private void ChangeBGM(EventEntity eventEntity)
    {
        var bgm = _eventRepository.GetSound(eventEntity.EventID, out var volume);
        _bgmSystem.PlayBGM(bgm, volume);
    }

    private void PlayEffect(EventEntity eventEntity)
    {
        var effect = _eventRepository.GetEffect(eventEntity.EventID);
        _backGroundView.PlayEffect(effect);
    }

    private void ChangeBackGround(EventEntity eventEntity)
    {
        var backGround = _eventRepository.GetBackGround(eventEntity.EventID);
        _backGroundView.SetBackground(backGround);
    }
}