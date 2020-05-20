
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class BallColors : UdonSharpBehaviour
{
    Color[] colors = new Color[]
    {
        Color.white,
        Color.yellow,
        Color.blue,
        Color.red,
        new Color(0.6f, 0.3f, 0.6f),
        new Color(1f, 0.7f, 0.3f),
        Color.green,
        new Color(0.9f, 0.5f, 0.6f),
        Color.black,
        Color.yellow,
        Color.blue,
        Color.red,
        new Color(0.6f, 0.3f, 0.6f),
        new Color(1f, 0.7f, 0.3f),
        Color.green,
        new Color(0.9f, 0.5f, 0.6f),
    };

    void Start()
    {
        var props = new MaterialPropertyBlock();
        for (int i = 1; i < transform.childCount; ++i)
        {
            var b = transform.GetChild(i).GetChild(0).GetComponent<MeshRenderer>();
            props.SetColor("_Color", colors[i]);
            if (i > 8)
            {
                props.SetInt("_IsStripe", 1);
            }
            b.SetPropertyBlock(props);
        }
    }
}
