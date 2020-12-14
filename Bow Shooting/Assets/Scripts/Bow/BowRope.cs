using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BowController
{
    public class BowRope : MonoBehaviour
    {
        [SerializeField] private float _returnTime;
        [SerializeField] private Vector3 _startPosition;
        [SerializeField] private Vector3 _finalPosition;

        [SerializeField] private AnimationCurve _ropeCurve;
        private float _stretching;

        public float Stretching
        {
            get => _stretching;
            set => _stretching = value;
        }

        private void Start()
        {
            _startPosition = transform.localPosition;
        }

        public void StartStreachingRope()
        {
            if (_stretching < 1f)
                _stretching += Time.deltaTime;

            transform.localPosition = Vector3.Lerp(_startPosition, _finalPosition, _stretching);
        }

        public IEnumerator StopStreachingRope()
        {
            var startLocalPosition = transform.localPosition;

            for (var i = 0f; i < 1f; i += Time.deltaTime / _returnTime)
            {
                transform.localPosition =
                    Vector3.LerpUnclamped(startLocalPosition, _startPosition, _ropeCurve.Evaluate(i));
                yield return null;
            }
            transform.localPosition = _startPosition;
        }
    }

}
