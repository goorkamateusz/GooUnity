using Assets.Goo.SceneObjects.Characters.UI;
using Assets.Goo.Tools.Patterns;

public class UiReferenceManager : SceneSingleton<UiReferenceManager>
{
    public SceneInteractionsContainer KeyActionView;
    public MessagePopupView MessagePopup;
    public InventoryView Inventory;
}
