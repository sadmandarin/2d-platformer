using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool _isOnGround = true;
    private bool _isTouchedWall;
    private bool _isOnStair;
    private bool _isInStelsMode;

    public bool IsOnGround {  get { return _isOnGround; } private set {  _isOnGround = value; } }
    public bool IsTouchedWall { get { return _isTouchedWall; } private set { _isTouchedWall = value; } }
    public bool IsOnStair { get {  return _isOnStair; } private set { _isOnStair = value; } }
    public bool IsInStelsMode { get {  return _isInStelsMode; } private set { _isInStelsMode = value; } }

    public void GroundChecker(bool isOnGround)
    {
        _isOnGround = isOnGround;
    }

    public void IsTochedWall(bool isTouchedWall)
    {
        _isTouchedWall = isTouchedWall;
    }

    public void IsTouchedStairs(bool isOnStair)
    {
        _isOnStair = isOnStair;
    }

    public void ChangeStelsMode(bool stelsMode)
    {
        _isInStelsMode = stelsMode;
    }
}
