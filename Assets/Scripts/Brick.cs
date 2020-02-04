using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Brick : MonoBehaviour {

    private GameObject ball;
    private Ball ballScript;
    private SpriteRenderer ballRenderer, brickRenderer;
    private bool inside;
    private int maxLife;
    public int life;
    public Sprite[] theBricks;
    public string nextLevel;
    public GameObject[] thePowerups;

	// Use this for initialization
	void Start () {
        ball = GameObject.Find("Ball");
        ballScript = ball.GetComponent<Ball>();
        ballRenderer = ball.GetComponent<SpriteRenderer>();
        brickRenderer = GetComponent<SpriteRenderer>();
        inside = false;
        maxLife = life;
;	}
	
	// Update is called once per frame
	void Update () {
       
        if (brickRenderer.bounds.ClosestPoint(ballRenderer.bounds.min) == ballRenderer.bounds.min || brickRenderer.bounds.ClosestPoint(ballRenderer.bounds.max) == ballRenderer.bounds.max|| brickRenderer.bounds.ClosestPoint(ballRenderer.bounds.center) == ballRenderer.bounds.center)
        {
            if (!inside)
            {
                ballScript.SetDirection(BrickCollision());
                LoseLife();
                ballScript.AddScore(10);
            }
            inside = true;
        }
        else
            inside = false;
    }
    private Vector2 BrickCollision()
    {
        Bounds brickBound = brickRenderer.bounds, ballBound = ballRenderer.bounds;
        Vector2 direction = ballScript.GetDirection();
        float sizeY =brickBound.max.y;
        float sizeX = brickBound.max.x;
        float left = ballBound.max.x - brickBound.min.x;
        float right = brickBound.max.x - ballBound.min.x;
        float down= ballBound.max.y - brickBound.min.y;
        float up = brickBound.max.y - ballBound.min.y;

        if (up<=down&&up<=left&&up<=right)
        {
            if(ballBound.min.x>=(sizeX-sizeX/5))
                return new Vector2(1, 1);
            if(ballBound.min.x >= (sizeX - sizeX*4 / 5))
                return new Vector2(-1, 1);
            return new Vector2(direction.x, -direction.y);
        }
        if (down<=up&&down<=left&&down<=right)
        {
            if (ballBound.max.x >= (sizeX - sizeX / 5))
                return new Vector2(1, -1);
            if (ballBound.max.x >= (sizeX - sizeX*4 / 5))
                return new Vector2(-1, -1);
            return new Vector2(direction.x, -direction.y);
        }
        if (left<=down&&left<=up&&left<=right)
        {
            if (ballBound.max.y <= (sizeY - sizeY / 5))
                return new Vector2(-1, 1);
            if (ballBound.max.y <= (sizeY - sizeY * 4 / 5))
                return new Vector2(-1, -1);
            return new Vector2(-direction.x, direction.y);
        }
        if (right <= down && right <= up && right <= left)
        {
            if (ballBound.min.y <= (sizeY - sizeY / 5))
                return new Vector2(1, 1);
            if (ballBound.min.y <= (sizeY - sizeY * 4 / 5))
                return new Vector2(1, -1);
            return new Vector2(-direction.x, direction.y);
        }
        return direction;
    }
    private void LoseLife()
    {
        if (life > 1)
        {
            life--;
            brickRenderer.sprite = theBricks[maxLife - life];
        }
        else
        {
            if (transform.parent.childCount == 1)
            {
                ballScript.Save();
                if (!SceneManager.GetActiveScene().name.Equals("Boss"))
                    SceneManager.LoadScene(nextLevel);
            }

            int random= Random.Range(0, thePowerups.Length + (14 - life));
            if (random < 3)
            {
                GameObject go=Instantiate(thePowerups[random], transform);
                go.transform.parent = GameObject.Find("PowerUps").transform;
            }
            ballScript.AddScore(100*maxLife);
            Destroy(gameObject);
        }
    }
}
