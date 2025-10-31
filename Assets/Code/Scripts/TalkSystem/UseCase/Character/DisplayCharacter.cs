namespace ScriptedTalk.TalkSystem.UseCase.Character
{
    public class DisplayCharacter
    {
        private readonly ICharacterView _view;
        private readonly ICharacterRepository _repository;

        public DisplayCharacter(ICharacterView view, ICharacterRepository repository)
        {
            _view = view;
            _repository = repository;
        }
    }
}