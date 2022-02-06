using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    void Start()
    {
        
    }
    
    void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
        Destroy(this.gameObject, 5);
    }

    private void OnTriggerEnter(Collider c)
    {
        Destroy(this.gameObject);
    }
}
