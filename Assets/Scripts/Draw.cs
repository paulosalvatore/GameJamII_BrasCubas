using UnityEngine;
using UnityEngine.EventSystems;

public class Draw : MonoBehaviour,
    IPointerDownHandler,
    IPointerUpHandler,
    IPointerEnterHandler,
    IPointerExitHandler
{
    private bool _isPointerWithinBounds;

    private bool _isPointerDown;

    private Vector3 _lastMousePosition;

    [SerializeField]
    private float minDelta;

    [SerializeField]
    private GameObject spherePrefab;

    [SerializeField]
    private Transform armParent;

    private Transform _currentDrawParent;

    private int _id;

    [SerializeField]
    private Camera drawCamera;

    private void Update()
    {
        if (!_isPointerDown
            || !_isPointerWithinBounds)
        {
            return;
        }

        var mousePosition = Input.mousePosition;
        mousePosition.y *= -1f;
        mousePosition.z = -10;

        var delta = Vector3.Distance(mousePosition, _lastMousePosition);

        if (delta < minDelta)
        {
            return;
        }

        var sphere = Instantiate(spherePrefab, drawCamera.ScreenToWorldPoint(mousePosition), Quaternion.identity);
        sphere.transform.parent = _currentDrawParent;

        _lastMousePosition = mousePosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _currentDrawParent = new GameObject().transform;
        _currentDrawParent.gameObject.name = "Draw " + ++_id;

        var mousePosition = Input.mousePosition;
        mousePosition.y *= -1f;
        mousePosition.z = -10f;

        _currentDrawParent.position = drawCamera.ScreenToWorldPoint(mousePosition);

        _isPointerDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isPointerDown = false;

        // Destroy previous arms
        foreach (Transform child in armParent)
        {
            Destroy(child.gameObject);
        }

        // Calculate positions
        const float zAmount = 0.5f;
        var armPosition = armParent.position;
        var arm1Position = armPosition + Vector3.forward * zAmount;
        var arm2Position = armPosition + Vector3.forward * -zAmount;

        // First Arm Position
        _currentDrawParent.position = arm1Position;

        // Second Arm
        var arm2Rotation = _currentDrawParent.rotation.eulerAngles;
        arm2Rotation.z -= 180f;
        var secondArm = Instantiate(_currentDrawParent.gameObject, arm2Position, Quaternion.Euler(arm2Rotation));

        // Parent
        _currentDrawParent.parent = armParent;
        secondArm.transform.parent = armParent;

        armParent.parent.Translate(Vector3.up * 3f);

        _currentDrawParent = null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isPointerWithinBounds = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isPointerWithinBounds = false;
    }
}
