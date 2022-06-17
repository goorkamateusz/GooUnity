namespace Goo.Characters.Interactions
{
    public class SingleUseKeyHandler : PersistantKeyHandler
    {
        internal override bool CancelAfterUp => false;
    }
}