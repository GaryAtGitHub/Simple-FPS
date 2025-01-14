﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleporter : MonoBehaviour
{
    public Action<Vector3,Vector3> OnTeleportFinish;

    // distance of Player transform to the feet
    public float PlayerHeightOffSet;
    public PlayerFPSController FpsController;
    public Pointer PlayerPointer;
    public string TargetLayerName;
    public GameObject TeleportIndicator;

    [SerializeField]
    private float TeletportSpeed = 20;
    
    void Start()
    {
        TeleportIndicator = Instantiate(TeleportIndicator);
        TeleportIndicator.SetActive(false);
        FpsController = FpsController ? FpsController : GetComponentInChildren<PlayerFPSController>();
        PlayerPointer = PlayerPointer ? PlayerPointer : GetComponentInChildren<Pointer>();
        PlayerPointer.OnRaycastHit += CheckTelePort;
        PlayerPointer.OnRaycastMiss += ClearIndicator;
    }

    private void OnDestroy()
    {
        PlayerPointer.OnRaycastHit -= CheckTelePort;
        PlayerPointer.OnRaycastMiss -= ClearIndicator;
    }

    private void CheckTelePort(RaycastHit hit)
    {
        if (hit.transform.gameObject.layer == LayerMask.NameToLayer(TargetLayerName))
        {
            // Show indicator if angle is satisfied.
            if (Vector3.Angle(hit.normal, Vector3.up) < 60)
            {
                if (!TeleportIndicator.activeInHierarchy)
                {
                    TeleportIndicator.SetActive(true);
                }
                TeleportIndicator.transform.position = hit.point;

                TeleportIndicator.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                if (Input.GetMouseButton(0))
                {
                    // Teleport to the cusor poinint position with height offset
                    StopAllCoroutines();
                    StartCoroutine(StartTeleporting(hit.point, hit.normal));
                }
            }
            else
            {
                TeleportIndicator.SetActive(false);
            }
        }
        else
        {
            TeleportIndicator.SetActive(false);
        }
    }

    private void ClearIndicator()
    {
        TeleportIndicator.SetActive(false);
    }

    IEnumerator StartTeleporting(Vector3 destination, Vector3 normal)
    {
        Vector3 offSetDestination = new Vector3(destination.x, destination.y + PlayerHeightOffSet, destination.z);
        Vector3 direction = offSetDestination - transform.position;
        while (direction.magnitude > 0.05)
        {
            Vector3 teleportDelta = Vector3.ClampMagnitude(Vector3.Normalize(direction) * TeletportSpeed * Time.deltaTime, direction.magnitude);
            transform.Translate(teleportDelta, Space.World);
            direction = offSetDestination - transform.position;
            yield return null;
        }
        OnTeleportFinish?.Invoke(destination, normal);
    }
}
