
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class SmackCueBall : UdonSharpBehaviour
{
    public Rigidbody cueBall;
    public float force;
    void Interact()
    {
        cueBall.AddForce(Vector3.forward * force, ForceMode.Impulse);

        
    }
}
