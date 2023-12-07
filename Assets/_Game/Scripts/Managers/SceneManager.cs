using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : Singleton<SceneManager>
{
    public async void LoadMenu()
    {
        AsyncOperation loadOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Scene_Menu", LoadSceneMode.Single);
        loadOperation.allowSceneActivation = false;

        while (Time.timeSinceLevelLoad < 4)
        {
            await Task.Yield();
        }

        loadOperation.allowSceneActivation = true;
    }
    public async void LoadGame()
    {
        AsyncOperation loadOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Scene_Game", LoadSceneMode.Single);
        while (!loadOperation.isDone)
        {
            await Task.Yield();
        }

    }
}
