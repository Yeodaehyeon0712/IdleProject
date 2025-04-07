using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : BaseCamera
{
    //메인 카메라(메인 캐릭터를 따라가는) 클래스
    GameObject _followTarget;
    bool _isFollowTargetPosition;
    public bool IsFollowTargetPosition
    {
        get { return _isFollowTargetPosition; }

        set
        {
            _isFollowTargetPosition = value;
        }
    }

    public GameObject SetActor
    {
        set
        {
            _followTarget = value;
            IsFollowTargetPosition = true;
            transform.position = _followTarget.transform.position + GameConst.ViewOffset + Vector3.forward * -10f;
            _targetPosition = transform.position;
        }
    }
    public GameObject SetTagActor
    {
        set => _followTarget = value;
    }
    Vector3 _targetPosition;
    [Range(0f, 1f)] [SerializeField] float _lerpCoefficient = 0.05f;
    float _currentLerpAmount;
    private void FixedUpdate()
    {
        if (_followTarget != null)
        {
            if (IsFollowTargetPosition)
            {
                Vector3 actorPosition = _followTarget.transform.position;
               // _currentLerpAmount = Player.GetMoveSpeed() * _lerpCoefficient;
                if (actorPosition != _targetPosition)
                {
                    if ((actorPosition - _targetPosition).sqrMagnitude > float.Epsilon)
                        _targetPosition = Vector3.Lerp(_targetPosition, actorPosition, _currentLerpAmount);
                    else
                        _targetPosition = actorPosition;
                }
            }

        }
    }
}
