using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeControl : MonoBehaviour
{
    [SerializeField] GameObject _thisCube;
    LifeControl control;
    int _posX;
    int _posY;
    bool _life;
    public void SetPoint(int x, int y, LifeControl lifeControl)
    {
        control = lifeControl;
        _posX = x;
        _posY = y;
    }
    public void Life()
    {
        _thisCube.SetActive(true);
        _life = true;
    }
    public void Dead()
    {
        _thisCube.SetActive(false); ;
        _life = false;
    }
    private void OnMouseDown()
    {
        if (control)
        {
            control.PointAction(_posX, _posY);            
        }
    }
}
