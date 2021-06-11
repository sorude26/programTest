using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CellState
{
    None = 0,
    One = 1,
    Two = 2,
    Three = 3,
    Four = 4,
    Five = 5,
    Six = 6,
    Seven =7,
    Eight = 8,
    Mine = -1,
}

public class Cell : EventSubscriber
{
    [SerializeField] private Text _cellText = null;
    [SerializeField] private CellState _cellState = CellState.None;
    [SerializeField] GameObject _cellButton;
    [SerializeField] GameObject _cellGuard;
    [SerializeField] Image[] _images;
    private bool _guard = false;
    public int SellID { get; set; }
    public bool Check { get; private set; }
    public CellState CellState
    {
        get => _cellState;
        set
        {
            _cellState = value;
            CellStateChange();
        }
    }
    private void OnValidate()
    {
        CellStateChange();
    }

    private void CellStateChange()
    {
        if (!_cellText)
        {
            return;
        }
        _images[0].color = Color.clear;
        switch (_cellState)
        {
            case CellState.None:
                _cellText.text = "";
                break;
            case CellState.One:
                _cellText.text = ((int)_cellState).ToString();
                _cellText.color = Color.blue;
                break;
            case CellState.Two:
                _cellText.text = ((int)_cellState).ToString();
                _cellText.color = new Color(0, 0.8f, 0);
                break;
            case CellState.Three:
                _cellText.text = ((int)_cellState).ToString();
                _cellText.color = Color.red;
                break;
            case CellState.Four:
                _cellText.text = ((int)_cellState).ToString();
                _cellText.color = new Color(1, 0, 1);
                break;
            case CellState.Five:
                _cellText.text = ((int)_cellState).ToString();
                _cellText.color = new Color(1, 0.5f, 0);
                break;
            case CellState.Six:
                _cellText.text = ((int)_cellState).ToString();
                _cellText.color = new Color(0, 0.5f, 0.8f);
                break;
            case CellState.Seven:
                _cellText.text = ((int)_cellState).ToString();
                _cellText.color = new Color(0.7f, 0, 0.7f);
                break;
            case CellState.Eight:
                _cellText.text = ((int)_cellState).ToString();
                _cellText.color = new Color(0.5f, 0.5f, 0.5f);
                break;
            case CellState.Mine:
                _cellText.text = "X";
                _images[0].color = Color.white;
                _cellText.color = Color.black;
                break;
            default:
                break;
        }
    }
    public void OpenThis()
    {
        if (BomTest.Instance.ExplosionBom)
        {
            return;
        }
        _cellButton.SetActive(false);
        if (_cellState == CellState.None && !Check)
        {
            Check = true;
            BomTest.Instance.AroundCheck(SellID);
        }
        Check = true;
        BomTest.Instance.ClearCheck();
    }
    public void OnClickThis()
    {
        if (BomTest.Instance.ExplosionBom || _guard)
        {
            return;
        }
        _cellButton.SetActive(false);
        if (_cellState == CellState.None && !Check)
        {
            Check = true;
            BomTest.Instance.AroundCheck(SellID);
        }
        else if (_cellState == CellState.Mine)
        {
            BomTest.Instance.Explosion();
            _images[1].color = Color.white;
        }
        Check = true;
        BomTest.Instance.ClearCheck();
    }
    public void OnClickFlag()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (_guard)
            {
                _guard = false;
                _cellGuard.SetActive(false);
            }
            else
            {
                _guard = true;
                _cellGuard.SetActive(true);
            }
        }
    }
    public override void OnGameEnd()
    {
        _cellButton.SetActive(false);
    }
    public override void OnGameClear()
    {
        if (_cellState == CellState.Mine)
        {
            _cellGuard.SetActive(true);
        }
    }
    public override void OnRestart()
    {
        Check = false;
        _guard = false;
        _cellGuard.SetActive(false);
        _cellButton.SetActive(true);
        _images[1].color = Color.clear;
    }
    public override void OnDataSet()
    {
        Destroy(this.gameObject);
    }
}
