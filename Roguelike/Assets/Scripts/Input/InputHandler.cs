using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField]
    private InputSettings _inputSettings;

    public Mouse CurrentMouse       => Mouse.current;
    public Gamepad CurrentGamepad   => Gamepad.current;

    public float ScrollDelta        => CurrentMouse.scroll.ReadValue().normalized.y;
    public Vector3 MousePosition    => CurrentMouse.position.ReadValue();
    public Vector3 MouseDelta       => CurrentMouse.delta.ReadValue() * _inputSettings.MouseScaling;
    public Vector3 GamepadRS        => CurrentGamepad != null ? CurrentGamepad.rightStick.ReadValue() * _inputSettings.GamepadScaling : Vector3.zero;
    public Vector3 GamepadLS        => CurrentGamepad != null ? CurrentGamepad.leftStick.ReadValue() * _inputSettings.GamepadScaling : Vector3.zero;
    
    public InputBindings InputBindings { get; private set; }
    public Vector2 WASDAxis { get; private set; }

    private void Awake()
    {
        InputBindings = new InputBindings();

        InputBindings.Keyboard.WASDMovement.performed += context => WASDAxis = InputBindings.Keyboard.WASDMovement.ReadValue<Vector2>().normalized;
        InputBindings.Keyboard.WASDMovement.canceled  += context => WASDAxis = Vector2.zero;

    }

    private void OnEnable()
    {
        InputBindings.Keyboard.Enable();

    }

    private void OnDisable()
    {
        InputBindings.Keyboard.Disable();

    }

}
