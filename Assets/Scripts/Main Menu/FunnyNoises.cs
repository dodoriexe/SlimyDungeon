using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunnyNoises : MonoBehaviour
{

    public float cooldownTime = 0.0f;
    public AudioClip clip;

    private float actualCooldownTime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        actualCooldownTime -= Time.fixedDeltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(actualCooldownTime <= 0)
        {
            AudioManager.PlayClip(clip);
            actualCooldownTime = cooldownTime;
        }
    }
}
