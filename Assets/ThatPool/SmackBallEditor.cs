using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmackBallEditor : MonoBehaviour
{
    public Rigidbody cueBall;
    public Transform balls;

    public Vector3 forcePosition;
    [Range(0, 5f)]
    public float force;
    [Range(0,1f)]
    public float timeScale = 1f;

    [Range(0, 0.1f)]
    public float randomness;

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

    void Reset()
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

    void Update()
    {
        Time.timeScale = timeScale;
    }

    void Interact()
    {
        var f = Vector3.forward * force;
        f.x = Random.Range(-randomness, randomness);
        var p = cueBall.transform.position;
        p.x += Random.Range(-randomness, randomness);
        cueBall.transform.position = p;
        cueBall.AddForceAtPosition(f, cueBall.transform.position + forcePosition, ForceMode.Impulse);
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 100, 50), "Smack ball"))
        {
            Interact();
        }
        if (GUI.Button(new Rect(100, 0, 100, 50), "Reset"))
        {
            Reset();
        }
    }
}
