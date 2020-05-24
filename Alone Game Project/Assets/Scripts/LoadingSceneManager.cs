using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneManager : MonoBehaviour {

    public GameObject loadingScreen;
    public Slider loadingSlider;

    public Text progressText;


    private void Start () {

    } 

    public void LoadLevel (int sceneIndex) {
        StartCoroutine (LoadAsynchronously (sceneIndex));
    }

    IEnumerator LoadAsynchronously (int sceneIndex) {

        AsyncOperation operation = SceneManager.LoadSceneAsync (sceneIndex);

        loadingScreen.SetActive(true);

        while (!operation.isDone) {
            //operation.isDone == false

            float progress = Mathf.Clamp01(operation.progress / .9f);

            progressText.text = progress * 100f + "%";
            Debug.Log (progress);

            yield return null;
        }

    }

}