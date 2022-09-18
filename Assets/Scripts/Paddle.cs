using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    //TODO: COnvert data to scriptable object
    [SerializeField] private float _speedLimit;
    [SerializeField] private float _speedIncreaseMultiplier;
    [SerializeField] private string _inputAxisName;

    private float xPosition;
    private bool _isInputEnabled;
    private float _speed;
    private Rigidbody _rigidbody;

    public bool IsInputEnabled { get => _isInputEnabled; set => _isInputEnabled = value; }
    public float Speed => _speed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _isInputEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(_isInputEnabled)
            ManageInput();
    }

    private void ManageInput()
    {
        float delta = Input.GetAxis(_inputAxisName) * _speedIncreaseMultiplier * Time.deltaTime;
        xPosition = Mathf.Clamp(xPosition + delta, -4.5f, 4.5f);

        transform.position = new Vector3(xPosition, transform.position.y, transform.position.z);
    }
}
