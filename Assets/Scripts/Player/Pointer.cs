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
    public Transform FireTransform;
    public GameObject Bullet;
    public float BulletVelocity;

    [SerializeField]
    private Camera _MainCamera;
    private Ray _RayFromScreen;
    // The interactable object the pointer is hovering
    private BaseInteractable _InteractableHovering;

    void Start()
    {
        _MainCamera = _MainCamera ? _MainCamera : Camera.main;
        //FirePoint = FireTransform ? FireTransform.position : _MainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, _MainCamera.nearClipPlane));
    }

    void Update()
    {
        _RayFromScreen = _MainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (Physics.Raycast(_RayFromScreen, out RaycastHit hit))
        {

            // Compare layer mask with layer of hit object. Change color if its layer is in the valid mask
            if ((1 << hit.transform.gameObject.layer & ValidLayer.value) != 0)
            {
                CCManager.ColorToValid();
            }
            else
            {
                CCManager.ColorToDefault();
            }

            OnRaycastHit?.Invoke(hit);

            _InteractableHovering = hit.collider.GetComponentInParent<BaseInteractable>();
            if (_InteractableHovering != null)
            {
                CCManager.ShowCross();

                if (Input.GetMouseButtonUp(1))
                {
                    _InteractableHovering.OnRightClick();
                }

                if(Input.GetMouseButtonUp(0))
                {
                    _InteractableHovering.OnLeftClick();
                }
            }
            else
            {
                CCManager.ShowCross(false);
            }
        }
        else
        {
            CCManager.ColorToDefault();
            OnRaycastMiss?.Invoke();
            _InteractableHovering = null;
            CCManager.ShowCross(false);
        }


        if (Input.GetKeyUp("f"))
        {
            GameObject go = Instantiate(Bullet, FireTransform.position, Quaternion.LookRotation(_RayFromScreen.direction));
            go.GetComponent<Rigidbody>().velocity = _RayFromScreen.direction * BulletVelocity;
        }

    }
}
