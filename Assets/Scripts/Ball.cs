using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ball : MonoBehaviour {
    private Vector3 direction;
    private float speed;
    private GameObject pad,game;
    private bool play,sticky,insideL,insideR,insideU,inThePad;
    public SpriteRenderer rightBounce, leftBounce, upperBounce;
    private SpriteRenderer ball,padRenderer;
    private GameObject theBricks;
    private int score;
    public Text scoreText;
    private Immortal immortal;

    void Start()
    {
        pad = GameObject.Find("Pad");
        game = GameObject.Find("Game");
        transform.parent = pad.transform;
        transform.position = pad.transform.position+new Vector3(0,0.25f);
        speed = 4;
        sticky = false;
        play = false;
        insideL = false;
        insideR = false;
        insideU = false;
        inThePad = true;
        ball = GetComponent<SpriteRenderer>();
        padRenderer = pad.GetComponent<SpriteRenderer>();
        immortal = GameObject.Find("Immortal").GetComponent<Immortal>();
        score = immortal.GetScore();
        scoreText.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (play)
        {
            direction = BounceCollision();
            direction = PadCollision();
            transform.position += direction * speed * Time.deltaTime;
        }
        else
            if (Input.GetKey(KeyCode.Space))
            {
                direction = new Vector2(1, 1);
                transform.parent = game.transform;
                play = true;
            }
    }
    private Vector2 BounceCollision()
    {
        Bounds b = ball.bounds;
        if (rightBounce.bounds.ClosestPoint(b.max) == b.max)
        {
            if (!insideR)
            {
                insideR = true;
                return new Vector2(-direction.x, direction.y);
            }
        }
        else
            insideR = false;
        if (leftBounce.bounds.ClosestPoint(b.min)==b.min)
        {
            if (!insideL)
            {
                insideL = true;
                return new Vector2(-direction.x, direction.y);
            }
        }
        else
            insideL = false;

        if (upperBounce.bounds.ClosestPoint(b.max)==b.max)
        {
            if (!insideU)
            {
                insideU = true;
                return new Vector2(direction.x, -direction.y);
            }
        }
        else
            insideU = false;
        Vector3 v = Camera.main.WorldToViewportPoint(transform.position);
        if (v.y <= 0)
        LoseLife();
        if (v.y >= 1|| v.x <= 0|| v.x >= 0.6)
            initialState();

        return direction;
    }

    private void initialState()
    {
        transform.parent = pad.transform;
        transform.position = pad.transform.position + new Vector3(0, 0.1f);
        play = false;
    }

    private Vector2 PadCollision()
    {
        Bounds b = ball.bounds;
        if (padRenderer.bounds.ClosestPoint(b.min) == b.min)
        {
            if (!inThePad)
            {
                inThePad = true;
                if (!sticky)
                    return BallDirection();
                else
                    initialState();
                
            }
        }
        else
            inThePad = false;

        return direction;
    }
    private Vector2 BallDirection()
    {
        float j = padRenderer.bounds.max.x - padRenderer.bounds.min.x;
        Bounds b = ball.bounds;

        if (b.min.x >= (padRenderer.bounds.max.x - j / 5) && b.min.x <= padRenderer.bounds.max.x)
            return new Vector2(1, 1);
        if (b.min.x < (padRenderer.bounds.max.x - j / 5) && b.min.x >= (padRenderer.bounds.max.x - j * 2 / 5))
            return new Vector2(0.5f, 1);
        if (b.min.x < (padRenderer.bounds.max.x - j*2 / 5) && b.min.x >= (padRenderer.bounds.max.x - j * 3 / 5))
            return new Vector2(0, 1);
        if (b.min.x < (padRenderer.bounds.max.x - j*3 / 5) && b.min.x >= (padRenderer.bounds.max.x - j * 4 / 5))
            return new Vector2(-0.5f, 1);
        if (b.min.x < (padRenderer.bounds.max.x - j * 4 / 5) && b.min.x >= padRenderer.bounds.min.x)
            return new Vector2(-1, 1);
        return -direction;
    }
    public void LoseLife()
    {
            immortal.LoseLife();
            initialState();
            score -= 200;
            if (score < 0)
                score = 0;
            scoreText.text = score.ToString();
    }
    public void SetDirection(Vector2 v2)
    {
        direction = v2;
    }
    public Vector2 GetDirection()
    {
        return direction;
    }
    public void AddScore(int s)
    {
        score += s;
        scoreText.text = score.ToString();
    }
    public void SetSpeed(int s)
    {
        speed = s;
    }
    public void SetSticky(bool b)
    {
        sticky=b;
    }
    public void Save()
    {
        immortal.SetScore(score);
    }
}
