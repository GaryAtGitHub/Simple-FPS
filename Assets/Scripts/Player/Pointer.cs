using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pointer : MonoBehaviour
{
    public Action<RaycastHit> OnRaycastHit;
    public Action OnRaycastMiss;
    public Image Cursor;
    public CursorColorManager CCManager;
    // A layermask for changing cursor color
    public LayerMask ValidLayer;

    [SerializeField]
    private Camera _MainCamera;
    private Ray _RayFromScreen;

    void Start()
    {
        _MainCamera = _MainCamera ? _MainCamera : Camera.main;
    }

    void Update()
    {
        _RayFromScreen = _MainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (Physics.Raycast(_RayFromScreen, out RaycastHit hit))
        {

            // Compare layer mask with layer of hit object
            if ((1 << hit.transform.gameObject.layer & ValidLayer.value) != 0)
            {
                CCManager.ColorToValid();
            }
            else
            {
                CCManager.ColorToDefault();
            }

            OnRaycastHit?.Invoke(hit);
        }
        else
        {
            CCManager.ColorToDefault();
            OnRaycastMiss?.Invoke();
        }


    }
}
