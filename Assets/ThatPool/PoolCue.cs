
using UdonSharp;
using UnityEngine;

public class PoolCue : UdonSharpBehaviour
{
    public Collider leftHand;
    public Collider rightHand;

    public bool simulateLeftTrigger;
    public bool simulateLeftGrip;
    public bool simulateRightTrigger;
    public bool simulateRightGrip;

    private bool leftHandEntered = false;
    private bool rightHandEntered = false;
    
    private Vector3 noGrip = Vector3.one * -1;

    Vector3 cueInHandLocalPositionOnGrip;
    Quaternion cueInHandRotationOnGrip;
    Vector3 handInCueLocalPositionOnGrip;

    void Start()
    {
    }

    void LateUpdate()
    {
        var leftGrip = GetLeftGrip() > 0.5;
        var rightGrip = GetRightGrip() > 0.5;
        if (leftHandEntered && leftGrip)
        {
            if (rightHandEntered && rightGrip)
            {
                BothHands();
            } else
            {
                OneHand(leftHand);
            }
        } else if (rightHandEntered && rightGrip)
        {
            OneHand(rightHand);
        } else
        {
            cueInHandLocalPositionOnGrip = noGrip;
        }

        // if onTriggerEnter back end of the queue, then set initial position along cue Y
        // while grip is held, set cue transform according to hand on the back.
        // if other hand grip on front, then ignore rotation of dominant hand and
        // use rotation going through non-dominant hand.
        
        // later: if non-dominant hand trigger, then freeze the transform of
        // non-dominant hand in world space.

    }
    void BothHands()
    {
        var leftLocal = transform.InverseTransformPoint(leftHand.transform.position);
        var rightLocal = transform.InverseTransformPoint(leftHand.transform.position);

        // dominant is the one to the back (-z) of the cue
        var dominantHand = rightHand;
        var nonDominant = leftHand;
        if (leftLocal.z < rightLocal.z)
        {
            dominantHand = leftHand;
            nonDominant = rightHand;
        }

        var cueDirection = (nonDominant.transform.position - dominantHand.transform.position).normalized;
        // in world space, the rotation to make cue face from dominant to non-dominant
        var newCueRotation = Quaternion.FromToRotation(Vector3.forward, cueDirection);

        // in that newly rotated local space, the original grip location back to a world position
        var gripInNewRotatedCueLocalSpace = newCueRotation * handInCueLocalPositionOnGrip;

        // world space dominant hand position minus the offset (in world rotation, but local to cue origin)
        var newCueWorldPosition = dominantHand.transform.position - gripInNewRotatedCueLocalSpace;

        transform.SetPositionAndRotation(newCueWorldPosition, newCueRotation);

    }

    void OneHand(Collider hand)
    {

        // get cue's origin position in hand's local space on initial grip.
        if (cueInHandLocalPositionOnGrip == noGrip)
        {
            cueInHandLocalPositionOnGrip = hand.transform.InverseTransformPoint(transform.position);
            cueInHandRotationOnGrip = Quaternion.Inverse(hand.transform.rotation) * transform.rotation;
            handInCueLocalPositionOnGrip = transform.InverseTransformPoint(hand.transform.position);
        }

        var newCueWorldPosition = hand.transform.TransformPoint(cueInHandLocalPositionOnGrip);
        var newCueRotation = hand.transform.rotation * cueInHandRotationOnGrip;
        transform.SetPositionAndRotation(newCueWorldPosition, newCueRotation);
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{other} entered");
        if (other == leftHand)
        {
            Debug.Log($"left hand entered");
            leftHandEntered = true;
        } else if (other == rightHand)
        {
            Debug.Log($"right hand entered");
            rightHandEntered = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        Debug.Log($"{other} left");
        if (other == leftHand)
        {
            Debug.Log($"left hand exit");
            leftHandEntered = false;
        } else if (other == rightHand)
        {
            Debug.Log($"right hand exit");
            rightHandEntered = false;
        }
    }

    float GetLeftTrigger()
    {
        return simulateLeftTrigger ? 1 : 0;
        //return Input.GetAxisRaw("Oculus_CrossPlatform_PrimaryIndexTrigger");
    }
    float GetLeftGrip()
    {
        //return simulateLeftGrip ? 1 : 0;
        return Input.GetAxisRaw("Oculus_CrossPlatform_PrimaryHandTrigger");
    }
    float GetRightTrigger()
    {
        return simulateRightTrigger ? 1 : 0;
        //return Input.GetAxisRaw("Oculus_CrossPlatform_PrimaryIndexTrigger");
    }
    float GetRightGrip()
    {
        //return simulateRightGrip ? 1 : 0;
        return Input.GetAxisRaw("Oculus_CrossPlatform_SecondaryHandTrigger");
    }
}
