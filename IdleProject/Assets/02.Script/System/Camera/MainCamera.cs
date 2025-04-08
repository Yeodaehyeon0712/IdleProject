using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : BaseCamera
{
    #region Fields
    Actor _followTarget;
    bool _isFollowTargetPosition;
    public bool IsFollowTargetPosition
    {
        get { return _isFollowTargetPosition; }

        set
        {
            _isFollowTargetPosition = value;
        }
    }
    Vector3 _targetPosition;
    [Range(0f, 1f)] [SerializeField] float _lerpCoefficient = 0.05f;
    float _currentLerpAmount;
    #endregion

    #region SetTarget Method
    public Actor SetActor
    {
        set
        {
            _followTarget = value;
            IsFollowTargetPosition = true;
            transform.position = _followTarget.transform.position + GameConst.ViewOffset /*+ Vector3.forward * -10f*/ ;
            _targetPosition = transform.position;
        }
    }
    public void Clear()
    {
        _followTarget = null;
        IsFollowTargetPosition = fadeEffectProcess;
        _targetPosition = Vector3.zero;
        transform.position = Vector3.zero;
    }
    
    private void FixedUpdate()
    {
        if (_followTarget != null)
        {
            if (IsFollowTargetPosition)
            {
                Vector3 actorPosition = _followTarget.transform.position;
                _currentLerpAmount = Player.PlayerCharacter.Status.GetStatus(eStatusType.MoveSpeed) * _lerpCoefficient;
                if (actorPosition != _targetPosition)
                {
                    if ((actorPosition - _targetPosition).sqrMagnitude > float.Epsilon)
                        _targetPosition = Vector3.Lerp(_targetPosition, actorPosition, _currentLerpAmount);
                    else
                        _targetPosition = actorPosition;
                }
                
                transform.position = _targetPosition + GameConst.ViewOffset /*+ Vector3.forward * -10f*/;
            }

        }
    }
    #endregion  
}
