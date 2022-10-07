using System;
using UnityEngine;

public class CircleController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _start;
    [SerializeField] private Transform _end;
    
    private float _currentTime;
    private Vector2 _currentPosition;
    
    private float _originalSpeed;
    private float _totalDistance;
    private Vector2 _originalStart;
    private Vector2 _originalEnd;

    private void Awake()
    {
        _originalSpeed = _speed;
        _originalStart = _start.position;
        _originalEnd = _end.position;
        _totalDistance = Vector2.Distance(_start.position, _end.position);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _start.position = _originalStart;
            _speed = _originalSpeed;
            ChangeDirection();
            
            var distance = Vector2.Distance(_currentPosition, _end.position);
            var travelTime = distance / _speed;
            _speed = travelTime / _totalDistance;
            
            _start.position = _currentPosition;
            _currentTime = 0f;
        }
        
        _currentTime += Time.deltaTime;

        var progress = _currentTime / _speed;
        _currentPosition = Vector2.Lerp(_start.position, _end.position, progress);
        transform.position = _currentPosition;

        if (_currentTime >= _speed)
        {
            _currentTime = 0f;
            _speed = _originalSpeed;
            _start.position = _originalStart;

            ChangeDirection();
        }
    }

    private void ChangeDirection()
    {
        (_start, _end) = (_end, _start);
        (_originalStart, _originalEnd) = (_originalEnd, _originalStart);
    }
}
