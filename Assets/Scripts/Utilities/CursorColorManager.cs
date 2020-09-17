using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorColorManager : MonoBehaviour
{
    public Image NormalCursor;
    public Color ValidColor;

    [SerializeField]
    private GameObject Cross;

    private Color _DefaultColor;
    private bool ValidStatus;

    public void ColorToValid()
    {
        if (!ValidStatus)
        {
            NormalCursor.color = ValidColor;
            ValidStatus = true;
        }        
    }

    public void ColorToDefault()
    {
        if (ValidStatus)
        {
            NormalCursor.color = _DefaultColor;
            ValidStatus = false;
        }        
    }

    public void ShowCross(bool value = true)
    {
        Cross.SetActive(value);
    }

    void Start()
    {
        NormalCursor = NormalCursor ? NormalCursor : GetComponent<Image>();
        _DefaultColor = NormalCursor.color;
    }
    
}
