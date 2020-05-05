
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class TrackedHand : UdonSharpBehaviour
{
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"{other} entered");
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log($"{other} left");
    }
}
