using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus4_2x : Bonus1_antiTrap {

    protected override void Start(){
        base.Start();
        tag = "Bonus3";
    }
    protected override void Update(){
        base.Update();
    }
    protected override void OnCollisionEnter2D(Collision2D other){
        base.OnCollisionEnter2D(other);
    }
}
