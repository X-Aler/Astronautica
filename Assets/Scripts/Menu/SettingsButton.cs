using UnityEngine;
using UnityEngine.EventSystems;

public class SettingsButton : ButtonBase, IPointerUpHandler
{
    [SerializeField] private GameObject settingsScreen;
    private bool open = false;
    public void OnPointerUp(PointerEventData eventData)
    {
        open = !open;
        settingsScreen.SetActive(open);
    }
}
