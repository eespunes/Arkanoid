using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pad : MonoBehaviour {

    public float speed;
    public GameObject left, right;
    private SpriteRenderer mySpriteRenderer;
    public bool sticky, giant, slow;
    private GameObject ball;
    private Ball ballSprite;
    public Text powerUp;
    public Sprite glue;
    private Sprite padSprite;

    // Use this for initialization
    void Start()
    {
        speed = 10;
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        ball = GameObject.Find("Ball");
        ballSprite = ball.GetComponent<Ball>();
        padSprite = mySpriteRenderer.sprite;
            }

    // Update is called once per frame
    void Update()
    {
        Bounds b = mySpriteRenderer.bounds;
        Bounds bLeft = left.GetComponent<SpriteRenderer>().bounds;
        Bounds bRight= right.GetComponent<SpriteRenderer>().bounds;
        float direction = 0;
        if (Input.GetKey(KeyCode.RightArrow))
            if (b.max.x < bRight.min.x)
                direction = 1;
        if (Input.GetKey(KeyCode.LeftArrow))
            if (b.min.x > bLeft.max.x)
                direction = -1;

        transform.position += Vector3.right * direction * speed * Time.deltaTime;
    }
    public void Slow()
    {
        NormalState(0);
        ballSprite.SetSpeed(2);
        powerUp.text = "Slow";
        slow = true;
    }
    public void Sticky()
    {
        NormalState(0);
        ballSprite.SetSticky(true);
        mySpriteRenderer.sprite = glue;
        powerUp.text = "Glue";
        sticky = true;
    }
    public void Giant()
    {
        NormalState(0);
        transform.localScale = new Vector3(0.3656249f * 2, 0.2437499f, 1);
        if (transform.childCount > 0)
            transform.GetChild(0).transform.localScale = new Vector3(0.5f, 1, 1);
        powerUp.text = "Giant Pad";
        giant = true;
    }
    public void NormalState(int i)
    {
        if (sticky&&(i==1||i==0))
        {
            ballSprite.SetSticky(false);
            mySpriteRenderer.sprite = padSprite;
            sticky = false;
        }
            if (giant && (i == 2 || i == 0))
            {
                transform.localScale = new Vector3(0.3656249f, 0.2437499f, 1);
                if (transform.childCount > 0)
                    transform.GetChild(0).transform.localScale = new Vector3(1, 1, 1);
            giant = false;
            }
        if (slow && (i == 3 || i == 0))
        {
            ballSprite.SetSpeed(4);
            slow = false;
        }
    }
}
