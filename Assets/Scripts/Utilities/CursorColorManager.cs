using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorColorManager : MonoBehaviour
{
    public Image Cursor;
    public Color ValidColor;

    private Color _DefaultColor;
    private bool ValidStatus;

    public void ColorToValid()
    {
        if (!ValidStatus)
        {
            Cursor.color = ValidColor;
            ValidStatus = true;
        }        
    }

    public void ColorToDefault()
    {
        if (ValidStatus)
        {
            Cursor.color = _DefaultColor;
            ValidStatus = false;
        }        
    }

    void Start()
    {
        Cursor = Cursor ? Cursor : GetComponent<Image>();
        _DefaultColor = Cursor.color;
    }
    
}
