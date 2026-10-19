using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private static string targetScene;
    public static void LoadScene(string SceneName)
    {
        targetScene = SceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    private void Start()
    {
        if (targetScene != null)
        {
            StartCoroutine(LoadCoroutine());
        }
    }
    public IEnumerator LoadCoroutine()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(targetScene);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            text.text = (progress * 100).ToString("F0") + "%";

            if (operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
            }
            yield return new WaitForSeconds(1.5f);
        }
    }
}
