using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour {
    public GameObject[] levels;
    public Sprite[] faces;
    public SpriteRenderer rightBounce;
    private int rotation,speed,life;
    private SpriteRenderer boss;
    private Vector3 direction;

    private void Start()
    {
        rotation = 45;
        speed = 0;
        life = 7;
        direction = new Vector2(1, 0);
        boss = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update () {
        Transform child = transform.GetChild(0);
        child.Rotate(new Vector3(0, 0, rotation * Time.deltaTime));
        if (speed > 0)
        {
            if (Camera.main.WorldToViewportPoint(boss.bounds.max).x >= 0.5)
                direction = new Vector2(-1, 0);
            if(Camera.main.WorldToViewportPoint(boss.bounds.min).x<=0.05)
                direction = new Vector2(1, 0);
            transform.position += speed * direction* Time.deltaTime;
        }
        LoseLife();
        
	}
    public void LoseLife()
    {

        if (levels[7 - life].transform.childCount == 0)
        {
            levels[7 - life].SetActive(false);
            life--;
            if(life==0)
                SceneManager.LoadScene("Win");
            else levels[7 - life].SetActive(true);
        }
        if (life == 5)
        {
            rotation = 90;
            boss.sprite = faces[1];
        }
        else if (life == 3)
        {
            boss.sprite = faces[2];
            speed = 2;
            rotation = 90;
        }
        else if (life == 1)
        {
            speed = 3;
            rotation = 180;
        }
    }
}
