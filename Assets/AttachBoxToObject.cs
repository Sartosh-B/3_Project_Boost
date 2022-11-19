using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachBoxToObject : MonoBehaviour
{

    public GameObject RocketForTransform;
    public bool isItemToTransportAttached = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isItemToTransportAttached)
        {
            Vector3 rocketVector = RocketForTransform.transform.position;
            Vector3 itemToTransportVector = new Vector3 (rocketVector.x, rocketVector.y -3f, rocketVector.z);
            Quaternion itemToTransportQuaternion = RocketForTransform.transform.rotation;
            transform.SetPositionAndRotation(itemToTransportVector, itemToTransportQuaternion);
        }
    }
    
    

    public void AttachBox()
    {
        isItemToTransportAttached = true;
    }
}
