namespace Goo.Characters
{
    public interface ICharacterComponent
    {
        void InjectCharacter(Character character);
    }

    public interface IPublicCharacterComponent : ICharacterComponent
    {

    }
}