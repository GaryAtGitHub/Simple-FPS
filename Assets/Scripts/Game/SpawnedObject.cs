using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnedObject : BaseInteractable
{
    public Action<GameObject> OnDelete;
    private Canvas _UI;
    private Camera _MainCam;
    private bool _IsUIShowwn;

    public override void OnRightClick()
    {
        DeletObject();
    }

    public override void OnLeftClick()
    {
        ToggleUI();
    }

    private void DeletObject()
    {
        OnDelete?.Invoke(gameObject);
        Destroy(gameObject);
    }

    private void Awake()
    {
        _UI = GetComponentInChildren<Canvas>(true);
        _MainCam = Camera.main;
        gameObject.name = "SpawnedObject " + UnityEngine.Random.Range(0, 1000).ToString();
        _UI.GetComponentInChildren<Text>().text += gameObject.name;
    }

    private void ToggleUI()
    {
        _UI.gameObject.SetActive(!_UI.gameObject.activeSelf);
        _IsUIShowwn = _UI.gameObject.activeSelf;
    }

    private void Update()
    {
        if (_IsUIShowwn)
        {
            _UI.transform.rotation = Quaternion.LookRotation(_UI.transform.position - _MainCam.transform.position);
        }       
    }
}
