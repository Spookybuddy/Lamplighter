using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManager : MonoBehaviour
{
    public void Scene1()
    {
        SceneManager.LoadScene("Main");
    }
    public void Scene2()
    {
        SceneManager.LoadScene("Intro");
    }
    public void Scene3()
    {
        SceneManager.LoadScene("2Dupdate");
    }
    public void Scene4()
    {
        SceneManager.LoadScene("Ending");
    }
}