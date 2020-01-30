using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Immortal : MonoBehaviour {

    int life, score;
    public GameObject[] hearts;
    // Use this for initialization
    void Awake () {
        DontDestroyOnLoad(gameObject);
        life = 3;
        score = 0;
	}

    public void SetScore(int a)
    {
        score = a;
    }

    public int GetScore()
    {
        return score;
    }
    public void PrintScore(Text t)
    {
        print(score);
        print(life);
        int total = score * life;
        t.text = total.ToString();
        Replay();
    }
    public void Replay()
    {
        life = 3;
        score = 0;
    }
    public void LoseLife()
    {
        if (life > 1)
        {
            hearts[3 - life].SetActive(false);
            life--;
        }
        else
        {
            Replay();
            SceneManager.LoadScene("GameOver");
        }

    }
}
