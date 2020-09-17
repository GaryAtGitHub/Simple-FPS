using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestorySelf : MonoBehaviour
{
    public float DestroyTime = 6;

    void Start()
    {
        Destroy(gameObject, DestroyTime);
    }

}
