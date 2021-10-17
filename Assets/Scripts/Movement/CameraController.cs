using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform targetTransform;
    [Range(-100f, 100f)] public float yOffset;
    [Range(-100f, 100f)] public float zOffset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FollowTransform();
    }

    private void FollowTransform()
    {
        //this.transform.LookAt(targetTransform);
        this.transform.position = new Vector3(targetTransform.position.x, yOffset, targetTransform.position.z + zOffset);
        //this.transform.rotation = this.transform.rotation;
    }
}
