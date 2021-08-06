using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BecomeDark : MonoBehaviour
{
    [SerializeField]
    private Material m_Material;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, m_Material);
    }

}
