using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus2_plusHP : Bonus1_antiTrap {

	protected override void Start () {
        base.Start();
        tag = "Bonus2";
	}
	protected override void Update () {
        base.Update();
	}
    protected override void OnCollisionEnter2D(Collision2D other)
    {
        base.OnCollisionEnter2D(other);
    }
}
