using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharChange : MonoBehaviour
{
    [SerializeField] private InputAction changeNextAction = default;
    [SerializeField] private InputAction changeBackAction = default;

    [SerializeField] private GameObject[] characters;

    public int Index = 0;
    public GameObject curentCharacter;

    private void OnEnable()
    {
        if(curentCharacter == null)
        {
            return;
        }
        changeNextAction.Enable();
        changeNextAction.performed += OnNextChange;

        changeBackAction.Enable();
        changeBackAction.performed += OnBackChange;
    }

    private void OnDisable()
    {
        changeNextAction.performed -= OnNextChange;
        changeNextAction.Disable();

        changeBackAction.performed -= OnBackChange;
        changeBackAction.Disable();
    }

    private void OnNextChange(InputAction.CallbackContext context)
    {
        if (curentCharacter == null) return;

        Debug.Log("1");
        Index++;
        if (Index >= characters.Length)
        {
            Index = 0;
        }
        ChangeCharacter();
    }

    void OnBackChange(InputAction.CallbackContext context)
    {
        if (curentCharacter == null) return;

        Debug.Log("2");
        Index--;
        if (Index < 0)
        {
            Index = characters.Length - 1;
        }
        ChangeCharacter();
    }

    void ChangeCharacter()
    {
        Vector3 spawnPos = curentCharacter.transform.position;
        Destroy(curentCharacter);

        curentCharacter = Instantiate(
            characters[Index],
            spawnPos,
            Quaternion.identity
        );
    }

    public void Set(GameObject p)
    {
        curentCharacter = p;
    }
}
