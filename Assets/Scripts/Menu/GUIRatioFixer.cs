using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUIRatioFixer : MonoBehaviour {

    public float m_NativeRatio = 3.5288461538F;

    void Start()
    {
        float currentRatio = (float)Screen.width / (float)Screen.height;
        Vector3 scale = transform.localScale;
        scale.x *= m_NativeRatio / currentRatio;
        transform.localScale = scale;
    }
}
