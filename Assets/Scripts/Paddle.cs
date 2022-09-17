using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] private float _speedLimit;
    [SerializeField] private float _speedIncreaseMultiplier;
    [SerializeField] private string _inputAxisName;


    private float _speed;
    public float Speed => _speed;

    private float xPosition;

    private Rigidbody _rigidbody;
    

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //_speed += Input.GetAxis(_inputAxisName) * _speedIncreaseMultiplier * Time.deltaTime;
        //_speed = Mathf.Clamp(_speed, -_speedLimit, _speedLimit);

        //_rigidbody.velocity = new Vector3(_speed, 0f, 0f);

        float delta = Input.GetAxis(_inputAxisName) * _speedIncreaseMultiplier * Time.deltaTime;
        xPosition = Mathf.Clamp(xPosition + delta, -4.5f, 4.5f);

        transform.position = new Vector3(xPosition, transform.position.y, transform.position.z);
    }

    private void FixedUpdate()
    {
        
    }
}
