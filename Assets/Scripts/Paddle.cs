using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    //TODO: COnvert data to scriptable object
    [SerializeField] private float _initialSpeed;
    [SerializeField] private float _speedLevelIncrease;
    [SerializeField] private float _ballBounceSpeedInfluence;
    [SerializeField] private string _inputAxisName;

    private float _speed;
    private float _delta;
    private float _xPosition;
    private bool _isInputEnabled;

    public bool IsInputEnabled { get => _isInputEnabled; set => _isInputEnabled = value; }

    // Start is called before the first frame update
    void Start()
    {
        _xPosition = transform.position.x;
        _isInputEnabled = false;
    }

    public void ResetPosition()
    {
        _xPosition = 0f;
        transform.position = new Vector3(_xPosition, transform.position.y, transform.position.z);
    }

    public void ResetSpeed()
    {
        _speed = _initialSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(_isInputEnabled)
            ManageInput();
    }

    private void ManageInput()
    {
        _delta = Input.GetAxis(_inputAxisName) * _speed * Time.deltaTime;
        _xPosition = Mathf.Clamp(_xPosition + _delta, -4.5f, 4.5f);

        transform.position = new Vector3(_xPosition, transform.position.y, transform.position.z);
    }

    public Vector3 GetSpeedVector()
    {
        return new Vector3(_delta * _ballBounceSpeedInfluence, 0f, 0f);
    }

    public void IncreaseSpeed()
    {
        _speed += _speedLevelIncrease;
    }
}
