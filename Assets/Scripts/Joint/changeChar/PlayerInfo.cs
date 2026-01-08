using UnityEngine;
using UnityEngine.InputSystem;

public enum CharacterType
{
    Character1,
    Character2,
    Character3,
}

public class PlayerInfo : MonoBehaviour
{
    public InputDevice PairWithDevice { get; private set; } = default;
    public CharacterType SelectedCharacter { get; private set; } = default;

    public PlayerInfo(InputDevice pairWithDevice, CharacterType selectedCharacter)
    {
        PairWithDevice = pairWithDevice;
        SelectedCharacter = selectedCharacter;
    }
}
