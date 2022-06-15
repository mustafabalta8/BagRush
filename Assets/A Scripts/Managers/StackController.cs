using System.Collections.Generic;
using UnityEngine;

public class StackController : MonoBehaviour
{
    public static StackController instance;

    [SerializeField] private Transform _pickUpParent;
    [SerializeField] private float _spaceBetweenNodes;
    [SerializeField] private Vector3 localCollectionStartPosition;
    [SerializeField] private float _lerpDuration;

    private Transform _stackParent;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private List<Transform> _stack = new List<Transform>();

    private void Awake()
    {
        _stackParent = transform;
        //_playerTransform = FindObjectOfType<Player>().transform;
        _stack.Add(_playerTransform);

        Bag.OnInteract += UpdateStack;

        Singelton();
    }

    private void Update()
    {
        // Simple fix for the child-parent-parent position relationship.
        transform.localPosition = new Vector3(_playerTransform.position.x * -1, 0, 0);
    }

    private void FixedUpdate()
    {
        WaveNodes();
    }
    private void Singelton()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void WaveNodes()
    {
        for (int i = 1; i < _stack.Count; i++)
        {
            Vector3 nodePosition = _stack[i].localPosition;
            Vector3 previousNodePosition = _stack[i - 1].localPosition;
            nodePosition = localCollectionStartPosition + new Vector3(
                Mathf.Lerp(nodePosition.x, previousNodePosition.x, Time.deltaTime * _lerpDuration),
                nodePosition.y,
                i * _spaceBetweenNodes);// i * _spaceBetweenNodes

            _stack[i].localPosition = nodePosition;
        }
    }

    public void UpdateStack(bool isPickedUp, Transform node)
    {
        if (isPickedUp)
        {
            print("node added to the stack");
            _stack.Add(node);
            node.SetParent(_stackParent);
        }
        else
        {
            _stack.Remove(node);
            node.SetParent(_pickUpParent);
            print("node removed from the stack");
        }
    }
    public void ClearStack()
    {
        int stackLenght = _stack.Count;
        for (int i = 1; i < stackLenght; i++)
        {
            _stack[i].gameObject.GetComponent<Bag>().ReturnToObjectPool();
            _stack[i].SetParent(_pickUpParent);      
            print("clear stack:"+i);
        }
        _stack.Clear();
        _stack.Add(_playerTransform);
    }
}

