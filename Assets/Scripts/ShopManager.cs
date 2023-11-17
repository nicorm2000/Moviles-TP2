using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private AudioClip clickClip;

    private ICommand _mainMenuCommand = new MainMenuCommand();

    public void ClickedMainMenu()
    {
        _mainMenuCommand.Execute(clickClip);
    }
}