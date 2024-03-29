﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClickLoadScene : MonoBehaviour {

    public void LoadByIndex(int sceneIndex) {
        SceneManager.LoadScene(sceneIndex);
    }

    public void LoadByName(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}
