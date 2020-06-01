using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarGame : MonoBehaviour
{
    public Menu mainMenu;
    
    private Scene _loadedScene;
    private AsyncOperation _loadSceneOp;

    private bool _isLoading;

    // Update is called once per frame
    void Update()
    {
        if (_isLoading)
        {
            mainMenu.SetLoadProgress(_loadSceneOp.progress);
        }
    }

    public void LoadLevel(string sceneName)
    {
        _loadSceneOp = SceneManager.LoadSceneAsync(sceneName);
        _isLoading = true;
    }
}
