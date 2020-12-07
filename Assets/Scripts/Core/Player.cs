using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrainMystery
{
    public class Player : TimeScaleIndependentUpdate
    {
        private Interactable _facedInteractable;

        [SerializeField]
        private Camera _camera;
        [SerializeField]
        private UICommands _uiCommands;

        [SerializeField]
        private float _checkFacedObjectTimer = 1f;
        [SerializeField]
        private float _checkFacedObjectMax = 1f;

        void Start()
        {

        }

        protected override void Update()
        {
            base.Update();

            UpdateInput();
            LookForObjectInFront();
        }

        void UpdateInput()
        {
            if(TrainMysteryGameManager.Instance.GetGameState() != GameState.running)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact();
            }
        }

        void LookForObjectInFront()
        {
            // check timer
            if (_checkFacedObjectTimer > 0)
            {
                _checkFacedObjectTimer -= deltaTime;
                return;
            }
            _checkFacedObjectTimer = _checkFacedObjectMax;

            // look for object
            LayerMask interactableMask = LayerMask.GetMask("Interactable");
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hit;
            if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, 5, interactableMask))
            {
                //Debug.Log(hit.collider.gameObject);

                // set inter
                var interactable = hit.collider.gameObject.GetComponent<Interactable>() != null ? hit.collider.gameObject.GetComponent<Interactable>() : null;
                if (interactable != _facedInteractable)
                {
                    _facedInteractable = interactable;
                    DisplayObjectName();
                }
            }
            else
            {
                _facedInteractable = null;
                DisplayObjectName();
            }
        }

        void Interact()
        {
            var interactable = this.GetInteractable();
            if (!interactable)
            {
                Debug.Log("nothing to interact with");
                return;
            }

            interactable.Interact();
        }

        Interactable GetInteractable()
        {
            return _facedInteractable;
        }

        void DisplayObjectName()
        {
            if (_facedInteractable == null)
            {
                _uiCommands.SetFacedObjectLabel(string.Empty);
            }
            else
            {
                _uiCommands.SetFacedObjectLabel(_facedInteractable.transform.name);
            }

        }
    } 
}
