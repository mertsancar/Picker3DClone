using Game.Character;
using UnityEngine;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        private Character _character;
        private Vector3? lastMousePos;
        private Vector2 diff;

        private void Start()
        {
            _character = GameController.Instance.character;
        }

        private void Update()
        {
            if (GameController.Instance.isPlaying)
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
                lastMousePos = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                ResetMouseInput();
            }

            if (lastMousePos != null)
            {
                UpdateMouseDirection();
            }
        }

        private void ResetMouseInput()
        {
            lastMousePos = null;
            _character.direction = Vector3.zero;
        }
        
        private void UpdateMouseDirection()
        {
            diff = (Vector2)Input.mousePosition - (Vector2)lastMousePos;

            lastMousePos = Input.mousePosition;
            _character.direction = Vector3.Lerp(_character.direction,  Vector3.right * diff.x, Time.deltaTime * 5);
            _character.transform.position = new Vector3(Mathf.Clamp(_character.transform.position.x, -1.25f, 1.25f),
                _character.transform.position.y, _character.transform.position.z);

        }
        
    }
}
