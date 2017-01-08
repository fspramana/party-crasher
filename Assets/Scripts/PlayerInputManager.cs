using UnityEngine;
using System.Collections;
using InControl;

public class PlayerInputManager : MonoBehaviour {

    public static PlayerInputManager singleton;

    public InputDevice[] playersInput = new InputDevice[4];

    private int count = 0;

    void Awake() {
        if ( singleton == null ) {
            singleton = this;
        } else {
            Destroy(gameObject);
            Debug.LogWarning("PlayerInputManager duplicate deleted!");
        }

        InputManager.OnDeviceAttached += OnDeviceAttached;
    }

    public void ListenSetupPlayerInput() {
        if ( count >= InputManager.Devices.Count ) return;

        if ( InputManager.ActiveDevice.RightTrigger ) {
            playersInput[count] = InputManager.Devices[InputManager.ActiveDevice.SortOrder];
            count++;
        }
    }

    public void ResetPlayerInput() {
        for ( int i = 0; i < playersInput.Length; i++ ) {
            playersInput[i] = null;
        }
    }

    private void OnDeviceAttached(InputDevice obj) {

    }
}
