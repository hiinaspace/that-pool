
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class Reset : UdonSharpBehaviour
{

    public Transform balls;
    private Vector3[] positions;
    private Quaternion[] rotations;

    void Start()
    {
        positions = new Vector3[balls.childCount];
        rotations = new Quaternion[balls.childCount];
        for (int i = 0; i < balls.childCount; ++i)
        {
            positions[i] = balls.GetChild(i).transform.position;
            rotations[i] = balls.GetChild(i).transform.rotation;
        }
    }

    void Interact()
    {
        for (int i = 0; i < balls.childCount; ++i)
        {
            var b = balls.GetChild(i).transform;
            b.SetPositionAndRotation(positions[i], rotations[i]);
            var rb = b.gameObject.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
