using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Scene Management /Scene Loader")]
public class SyncLoader : ScriptableObject
{



    public string sceneName;
    public LoadSceneMode loadMode;

    public void Loadscene()
    {
        SceneManager.LoadScene(sceneName, loadMode);
    }

}
