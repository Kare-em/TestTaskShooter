using System;
using UnityEngine;

namespace _MainAssets.Scripts.Player
{
    public class LegsView : MonoBehaviour
    {
        private float _angularSpeed = 90f;
        private Vector3 _targetVector3;
        private Vector3 _deltaVector3;
        private Quaternion _targetRotation;

        private void Update()
        {
            if(!TouchHandler.Instance.Pressed)
                return;
            _targetVector3 = transform.position + new Vector3(TouchHandler.Instance.TempDelta.x, 0f,
                TouchHandler.Instance.TempDelta.y);
            
            _deltaVector3 = _targetVector3 - transform.position;
            if(_deltaVector3==Vector3.zero)
                return;
            _targetRotation = Quaternion.LookRotation(_deltaVector3);

            transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, _angularSpeed * Time.deltaTime);
        }
    }
}