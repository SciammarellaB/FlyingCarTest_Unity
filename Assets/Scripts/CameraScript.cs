using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform target;
    public Vector3 targetOffSet;

    public Transform looktarget;
    //public Vector3 lookTargetOffSet;

    public float followSpeed;
    //public float lookSpeed;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 final = new Vector3(target.position.x + targetOffSet.x, target.position.y + targetOffSet.y, target.position.z + targetOffSet.z);
        this.gameObject.transform.position = UnityEngine.Vector3.Lerp(transform.position,final,followSpeed * Time.deltaTime);

        this.gameObject.transform.LookAt(looktarget);

        
        
    }
}
