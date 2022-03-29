using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KochGenerator : MonoBehaviour
{

    protected enum _axis
    {
        XAxis,
        YAxis,
        ZAxis
    };

    protected enum _initiator
    {
        Triangle,
        Square,
        Pentagon,
        Hexagon,
        Heptagon,
        Octagon
    };

    [SerializeField]
    protected _axis axis = new _axis();

    [SerializeField]
    protected _initiator initiator = new _initiator();
    [SerializeField]
    protected AnimationCurve _generator;
    protected Keyframe[] _keys;

    protected int _generationCount;

    protected int _initiatorPointAmount;
    private Vector3[] _initiatorPoint;
    private Vector3 _rotateVector;
    private Vector3 _rotateAxis;
    private float _initialRotation;

    [SerializeField]
    protected float _initiatorSize;

    protected Vector3[] _position;
    protected Vector3[] _targetPosition;

    // TODO extract copied code to another class
    private void Awake()
    {
        GetInitiatiorPoints();
        
        _position = new Vector3[_initiatorPointAmount + 1];
        _targetPosition = new Vector3[_initiatorPointAmount + 1];
        _keys = _generator.keys;

        _rotateVector = Quaternion.AngleAxis(_initialRotation, _rotateAxis) * _rotateVector;

        for (int i = 0; i < _initiatorPointAmount; i++)
        {
            _position[i] = _rotateVector * _initiatorSize;
            _rotateVector = Quaternion.AngleAxis(360 / _initiatorPointAmount, _rotateAxis) * _rotateVector;
        }
        _position[_initiatorPointAmount] = _position[0];
    }

    private void OnDrawGizmos()
    {
        GetInitiatiorPoints();
        _initiatorPoint = new Vector3[_initiatorPointAmount];
        _rotateVector = Quaternion.AngleAxis(_initialRotation, _rotateAxis) * _rotateVector;

        for (int i = 0; i < _initiatorPointAmount; i++)
        {
            _initiatorPoint[i] = _rotateVector * _initiatorSize;
            _rotateVector = Quaternion.AngleAxis(360 / _initiatorPointAmount, _rotateAxis) * _rotateVector;
        }

        for (int i = 0; i < _initiatorPointAmount; i++)
        {
            Gizmos.color = Color.white;
            Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
            Gizmos.matrix = rotationMatrix;
            if (i < _initiatorPointAmount - 1)
            {
                Gizmos.DrawLine(_initiatorPoint[i], _initiatorPoint[i + 1]);
            }
            else
            {
                Gizmos.DrawLine(_initiatorPoint[i], _initiatorPoint[0]); //the last one
            }
        }
    }

// TODO remove hardcoded angles
    private void GetInitiatiorPoints()
    {
        switch(initiator)
        {
            case _initiator.Triangle:
                _initiatorPointAmount = 3;
                _initialRotation = 0;
                break;
            case _initiator.Square:
                _initiatorPointAmount = 4;
                _initialRotation = 45;
                break;
            case _initiator.Pentagon:
                _initiatorPointAmount = 5;
                _initialRotation = 36;
                break;
            case _initiator.Hexagon:
                _initiatorPointAmount = 6;
                _initialRotation = 30;
                break;
            case _initiator.Heptagon:
                _initiatorPointAmount = 7;
                _initialRotation = 25.71428F;
                break;
            case _initiator.Octagon:
                _initiatorPointAmount = 8;
                _initialRotation = 22.5F;
                break;
            default:
                _initiatorPointAmount = 3;
                _initialRotation = 0;
                break;
        }
        switch (axis)
        {
            case _axis.XAxis:
                _rotateVector = new Vector3(1, 0, 0);
                _rotateAxis = new Vector3(0, 0, 1); // on z axis
                break;
            case _axis.YAxis:
                _rotateVector = new Vector3(0, 1, 0);
                _rotateAxis = new Vector3(1, 0, 0); // on x axis
                break;
            case _axis.ZAxis:
                _rotateVector = new Vector3(0, 0, 1);
                _rotateAxis = new Vector3(0, 1, 0); // on y axis
                break;
            default:
                _rotateVector = new Vector3(0, 0, 1);
                _rotateAxis = new Vector3(0, 1, 0); // on y axis
                break;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}