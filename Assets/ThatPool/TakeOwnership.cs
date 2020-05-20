
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class TakeOwnership : UdonSharpBehaviour
{
    public Transform balls;
    public GameObject cue;
    void Interact()
    {
        Networking.SetOwner(Networking.LocalPlayer, cue);
        for (int i = 0; i < balls.childCount; ++i)
        {
            Networking.SetOwner(Networking.LocalPlayer, balls.GetChild(i).gameObject);
        }
    }
}
