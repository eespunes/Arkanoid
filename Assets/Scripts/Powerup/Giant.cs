using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giant : PowerUp
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
        padScript.Giant();
    }
    public new void Delete()
    {
        padScript.NormalState(2);
        Destroy(gameObject);
    }
}
