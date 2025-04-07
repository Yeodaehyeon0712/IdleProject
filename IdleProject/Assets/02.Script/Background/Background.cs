using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    SpriteRenderer _renderer;
    public Background Initialize()
    {
        _renderer = GetComponent<SpriteRenderer>();
        Disable();
        return this;
    }
    public void Enable()
    {
        gameObject.SetActive(true);
    }
    public void Disable()
    {
        gameObject.SetActive(false);
    }
    public void SetupBackground(Sprite sprite)
    {
        _renderer.sprite = sprite;
        Enable();
    }
    public void CleanBackground()
    {
        _renderer.sprite = null;
    }
}
