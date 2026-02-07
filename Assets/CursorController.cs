using UnityEngine;
using UnityEngine.InputSystem;

public class CursorController : MonoBehaviour
{
    private float moveSpeed = 1000f;
    private Vector2 inputVer;
    private RectTransform trans;

    private PlayerInput input;

    private void Awake()
    {
        trans = GetComponent<RectTransform>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        inputVer = context.ReadValue<Vector2>();
    }

    public void OnEnter(InputAction.CallbackContext context)
    {

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector2 move = inputVer * moveSpeed * Time.deltaTime;

        trans.anchoredPosition += move;
    }
}
