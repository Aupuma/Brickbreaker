using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    //TODO: COnvert data to scriptable object
    [SerializeField] private float _speed;
    [SerializeField] private float _speedLevelIncrease;
    [SerializeField] private float _ballBounceSpeedInfluence;
    [SerializeField] private string _inputAxisName;

    private float delta;
    private float xPosition;
    private bool _isInputEnabled;

    public bool IsInputEnabled { get => _isInputEnabled; set => _isInputEnabled = value; }

    // Start is called before the first frame update
    void Start()
    {
        xPosition = transform.position.x;
        _isInputEnabled = false;
    }

    public void ResetPosition()
    {
        xPosition = 0f;
        transform.position = new Vector3(xPosition, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if(_isInputEnabled)
            ManageInput();
    }

    private void ManageInput()
    {
        delta = Input.GetAxis(_inputAxisName) * _speed * Time.deltaTime;
        xPosition = Mathf.Clamp(xPosition + delta, -4.5f, 4.5f);

        transform.position = new Vector3(xPosition, transform.position.y, transform.position.z);
    }

    public Vector3 GetSpeedVector()
    {
        Debug.Log(delta * _ballBounceSpeedInfluence);
        return new Vector3(delta * _ballBounceSpeedInfluence, 0f, 0f);
    }

    public void IncreaseSpeed()
    {
        _speed += _speedLevelIncrease;
    }
}
