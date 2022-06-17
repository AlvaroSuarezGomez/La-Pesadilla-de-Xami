using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialOffsetChanger : MonoBehaviour
{
    [SerializeField] private List<Material> materials = new List<Material>();
    [SerializeField] private List<Vector2> offsetsSpeeds = new List<Vector2>();

    private void Update()
    {
        for (int i = 0; i < materials.Count; i++)
        {
            materials[i].mainTextureOffset += offsetsSpeeds[i] * Time.deltaTime;
        }
    }
}
