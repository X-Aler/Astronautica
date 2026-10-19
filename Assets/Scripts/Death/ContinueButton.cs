using UnityEngine;

public class ContinueButton : MonoBehaviour
{
    public void OnClick()
    {
        LoadingManager.LoadScene("MainMenu");
    }
}
