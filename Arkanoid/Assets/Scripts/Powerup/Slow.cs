using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : PowerUp
{

    private void Update()
    {
        Move();
        if (IsThePad())
        {
            Power();
            InPad();
        }
        HasToBeDestroyed();
    }
    public new void Power()
    {
        padScript.Slow();
    }
    public new void Delete()
    {
        padScript.NormalState(3);
        Destroy(gameObject);
    }
}
