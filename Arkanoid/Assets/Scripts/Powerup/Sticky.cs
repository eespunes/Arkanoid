using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sticky : PowerUp {

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
        padScript.Sticky();
    }
    public new void Delete()
    {
        padScript.NormalState(1);
        Destroy(gameObject);
    }
}
