using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using UnityEngine;

public class FlyingScript : MonoBehaviour
{
    public float alturaDesejada;                         //nivel desejado para subir o veículo
    public float amortecimento;                          //suavidade para chegar na altura desejada
    public float[] forcaPropulsores;                     //força para subir o veículo

    public float vertical, horizontal;
    public float verticalInclination;

    public float speed, acceleration;

    public float gDistance1, gDistance2, gDistance3, gDistance4;

    public float fireTime;

    float tempoPouso;

    public UnityEngine.Vector3 centroDeMassa;            //centro de massa do veículo
    public UnityEngine.Transform[] pontoDePropulsao;     //posição dos propulsores !!! CONVERTER PARA VECTOR3 !!!

    public UnityEngine.Vector3[] upLift;                 //DESCOBRIR PRA QUE SERVE

    public Rigidbody rb;

    public bool land;
    public bool fire;

    public Transform shootPos1;
    public GameObject[] projectile1;

    void Start()
    {
        rb.centerOfMass = centroDeMassa;
    }  
    void Update()
    {
        UserInputs();
        PousoDecolagem();
        gunFire();
    }
    void FixedUpdate()
    {
        propulsorUm();
        propulsorDois();
        propulsorTres();
        propulsorQuatro();
        Movements();
    }
    void UserInputs()
    {
        vertical = Input.GetAxis("Vertical");
        verticalInclination = vertical / 5;

        horizontal = Input.GetAxis("Horizontal");

        fire = Input.GetButton("Fire1");
    }
    void Movements()
    {
        rb.AddRelativeForce(UnityEngine.Vector3.right * vertical * acceleration);

        rb.AddRelativeTorque(UnityEngine.Vector3.up * horizontal * acceleration);
    }
    void PousoDecolagem()
    {
        if(land)
        {
            tempoPouso += 0.05f * Time.deltaTime;
            alturaDesejada = Mathf.Lerp(alturaDesejada, 0, tempoPouso);
        }

        else
        {
            tempoPouso = 0;
        }
    }
    void propulsorUm()
    {
        RaycastHit hit;
        
        if(Physics.Raycast(pontoDePropulsao[0].position,transform.TransformDirection(-UnityEngine.Vector3.up),out hit,Mathf.Infinity))
        {
            UnityEngine.Debug.DrawRay(pontoDePropulsao[0].position, transform.TransformDirection(-UnityEngine.Vector3.up) * hit.distance, Color.red);
            gDistance1 = hit.distance;
        }

        UnityEngine.Debug.Log(hit.distance);

        forcaPropulsores[0] = 1 - (gDistance1 / alturaDesejada);

        if (forcaPropulsores[0] > 0)
        {
            upLift[0] = -Physics.gravity * (forcaPropulsores[0] - rb.velocity.y * amortecimento);
            rb.AddForceAtPosition(upLift[0], pontoDePropulsao[0].position);
        }
    }
    void propulsorDois()
    {
        RaycastHit hit;
       
        if (Physics.Raycast(pontoDePropulsao[1].position, transform.TransformDirection(-UnityEngine.Vector3.up), out hit, Mathf.Infinity))
        {
            UnityEngine.Debug.DrawRay(pontoDePropulsao[1].position, transform.TransformDirection(-UnityEngine.Vector3.up) * hit.distance, Color.red);
            gDistance2 = hit.distance;
        }
        //Mathf.Lerp(1, 0, ((pontoDePropulsao[1].position.y - (alturaDesejada - verticalInclination))));
        forcaPropulsores[1] = 1 - (gDistance2 / alturaDesejada);

        if (forcaPropulsores[1] > 0)
        {
            upLift[1] = -Physics.gravity * (forcaPropulsores[1] - rb.velocity.y * amortecimento);
            rb.AddForceAtPosition(upLift[1], pontoDePropulsao[1].position);
        }
    }
    void propulsorTres()
    {
        RaycastHit hit;
        
        if (Physics.Raycast(pontoDePropulsao[2].position, transform.TransformDirection(-UnityEngine.Vector3.up), out hit, Mathf.Infinity))
        {
            UnityEngine.Debug.DrawRay(pontoDePropulsao[2].position, transform.TransformDirection(-UnityEngine.Vector3.up) * hit.distance, Color.red);
            gDistance3 = hit.distance;
        }
        forcaPropulsores[2] = 1 - (gDistance3 / alturaDesejada);

        if (forcaPropulsores[2] > 0)
        {
            upLift[2] = -Physics.gravity * (forcaPropulsores[2] - rb.velocity.y * amortecimento);
            rb.AddForceAtPosition(upLift[2], pontoDePropulsao[2].position);
        }
    }
    void propulsorQuatro()
    {
        RaycastHit hit;
        
        if (Physics.Raycast(pontoDePropulsao[3].position, transform.TransformDirection(-UnityEngine.Vector3.up), out hit, Mathf.Infinity))
        {
            UnityEngine.Debug.DrawRay(pontoDePropulsao[3].position, transform.TransformDirection(-UnityEngine.Vector3.up) * hit.distance, Color.red);
            gDistance4 = hit.distance;
        }

        forcaPropulsores[3] = 1 - (gDistance4/alturaDesejada);

        if (forcaPropulsores[3] > 0)
        {
            upLift[3] = -Physics.gravity * (forcaPropulsores[3] - rb.velocity.y * amortecimento);
            rb.AddForceAtPosition(upLift[3], pontoDePropulsao[3].position);
        }
    }
    void gunFire()
    {
        if(fire)
        {
            fireTime += Time.deltaTime;

            if(fireTime > 0.2f)
            {
                Instantiate(projectile1[0], shootPos1.position, this.gameObject.transform.rotation);
                fireTime = 0;
            }
            
        }
    }
}
