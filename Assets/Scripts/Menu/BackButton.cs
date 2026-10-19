using UnityEngine;
using UnityEngine.EventSystems;

public class BackButton : ButtonBase, IPointerUpHandler
{
    private GameObject Screen;
    private bool open = false;

    private void Start()
    {
        Screen = transform.parent.gameObject;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        open = !open;
        Screen.SetActive(open);
    }
}
