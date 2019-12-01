using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private ParticleSystem ps;
    void Start(){
        tag = "explosion";
        ps = GetComponent<ParticleSystem>();
    }
    void Update()
    {
        if(ps.isStopped)
            Destroy(gameObject);
    }
}
