using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour {

    public string scene;
    private Immortal immortal;
    private void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("Win"))
        {
            immortal = GameObject.Find("Immortal").GetComponent<Immortal>();
            immortal.PrintScore(GameObject.Find("ScoreText").GetComponent<Text>());
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
    public void LoadScene()
    {
        SceneManager.LoadScene(scene);

    }
}
