using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RainbowColor : MonoBehaviour
{
    [SerializeField] float _colorSpeed = 1f;
    [SerializeField] Image _image;
    [SerializeField] Text _text;
    [SerializeField] Renderer _renderer;
    [SerializeField] Light _light;
    [SerializeField] float _a = 0.5f;
    [SerializeField] Vector3Int _startColor = new Vector3Int(1, 0, 0);
    float _r = 1;
    float _g = 0;
    float _b = 0;
    private void Start()
    {
        _r = _startColor.x;
        _g = _startColor.y;
        _b = _startColor.z;
    }
    void Update()
    {
        if (_r == 1 && _g < 1 && _b == 0)
        {
            _g += _colorSpeed * Time.deltaTime;
            if (_g >= 1)
            {
                _g = 1;
            }
        }
        if (_g == 1 && _r > 0 && _b == 0)
        {
            _r -= _colorSpeed * Time.deltaTime;
            if (_r <= 0)
            {
                _r = 0;
            }
        }
        if (_g == 1 && _r == 0 && _b < 1)
        {
            _b += _colorSpeed * Time.deltaTime;
            if (_b >= 1)
            {
                _b = 1;
            }
        }
        if (_g > 0 && _r == 0 && _b == 1)
        {
            _g -= _colorSpeed * Time.deltaTime;
            if (_g <= 0)
            {
                _g = 0;
            }
        }
        if (_b == 1 && _g == 0 && _r < 1)
        {
            _r += _colorSpeed * Time.deltaTime;
            if (_r >= 1)
            {
                _r = 1;
            }
        }
        if (_b > 0 && _g == 0 && _r == 1)
        {
            _b -= _colorSpeed * Time.deltaTime;
            if (_b <= 0)
            {
                _b = 0;
            }
        }
        if (_image)
        {
            _image.color = new Color(_r, _g, _b, _a);
        }
        if (_text)
        {
            _text.color = new Color(_r, _g, _b, _a);
        }
        if (_renderer)
        {
            _renderer.material.color = new Color(_r, _g, _b, _a);
        }
        if (_light)
        {
            _light.color = new Color(_r, _g, _b, _a);
        }
    }
}
