using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour {

    protected SpriteRenderer pad;
    protected Pad padScript;
    protected bool activated;
    protected Vector3 direction;
    protected Text t;
    // Use this for initialization
    protected void Start()
    {
        Invoke("Delete", 30);
        pad = GameObject.Find("Pad").GetComponent<SpriteRenderer>();
        padScript = GameObject.Find("Pad").GetComponent<Pad>();
        activated = false;
        direction = new Vector2(0, -1);
        t = GameObject.Find("PowerUp").GetComponent<Text>();
        Invoke("DeleteText", 4);
    }

    protected virtual void Delete() { }
    protected virtual void Power() { }
    protected void Move()
    {
        transform.position += direction * 3 * Time.deltaTime;
    }
    protected bool IsThePad()
    {
        return pad.bounds.ClosestPoint(transform.position) == transform.position && !activated;
    }
    protected void InPad(){
        activated = true;
        direction = Vector3.zero;
        transform.position = new Vector3(transform.position.x, transform.position.y, 400);
    }
    protected void HasToBeDestroyed()
    {
        if (Camera.main.WorldToViewportPoint(transform.position).y <= 0)
            if (!activated)
                Destroy(gameObject);
    }
    protected void DeleteText()
    {
        t.text = "";
    }
}
