using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneManager : MonoBehaviour {

    public GameObject loadingScreen;
    public Slider loadingSlider;

    public Text continueTxt;

    public Text progressText;

    

    private void Start () {

    }

    public void LoadLevel (int sceneIndex) {
        StartCoroutine (LoadAsynchronously (sceneIndex));
    }

    IEnumerator LoadAsynchronously (int sceneIndex) {

        yield return null;

        loadingScreen.SetActive (true);

        // yield return new WaitForSeconds (15);

        AsyncOperation operation = SceneManager.LoadSceneAsync (sceneIndex);

        operation.allowSceneActivation = false;

        while (!operation.isDone) {
            //operation.isDone == false

            float progress = Mathf.Clamp01 (operation.progress / .9f);


            loadingSlider.value = progress;
            progressText.text = progress * 100f + "%";
            Debug.Log (progress);

            continueTxt.text = "Press the E to continue";

            if (Input.GetKeyDown (KeyCode.E)) {
                Debug.Log ("E PRESSED");
                //Activate the Scene
                operation.allowSceneActivation = true;
            }

            yield return null;
        }

    }

    public void QuitGame () {
        Application.Quit ();
    }

}