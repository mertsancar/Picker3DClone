using Game.Character;
using UnityEngine;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        private Character _character;

        private void Start()
        {
            _character = GameController.instance.character;
        }

        private void Update()
        {
            if (GameController.instance.isPlaying)
            {
                HandleGameplayInput();
            }
        }

        private void HandleGameplayInput()
        {
            HandleMouseInput();
        }

        private void HandleMouseInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _character.lastMousePos = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                ResetMouseInput();
            }

            if (_character.lastMousePos != null)
            {
                UpdateMouseDirection();
            }
        }

        private void ResetMouseInput()
        {
            _character.lastMousePos = null;
            _character.direction = Vector3.zero;
        }
        
        private void UpdateMouseDirection()
        {
            _character.diff = (Vector2)Input.mousePosition - (Vector2)_character.lastMousePos;

            _character.lastMousePos = Input.mousePosition;
            _character.direction = Vector3.Lerp(_character.direction,  Vector3.right * _character.diff.x, Time.deltaTime * 5);
            _character.transform.position = new Vector3(Mathf.Clamp(_character.transform.position.x, -1.25f, 1.25f),
                _character.transform.position.y, _character.transform.position.z);

        }
        
    }
}
