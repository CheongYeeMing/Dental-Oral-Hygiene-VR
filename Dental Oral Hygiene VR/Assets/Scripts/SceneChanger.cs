using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger Instance;

    private void Awake() 
    {
        Instance = this;
    }

    public enum Scene 
    {
        Bathroom,
        JawModel
    }

    public void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

    public void LoadBathroom() 
    {
        SceneManager.LoadScene(Scene.Bathroom.ToString());
    }

    public void LoadJawModel() 
    {
        SceneManager.LoadScene(Scene.JawModel.ToString());
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public string GetScene() {
        return SceneManager.GetActiveScene().name;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
