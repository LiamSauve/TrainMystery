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
        //[SerializeField]
        //private UICommands _uiCommands;

        [SerializeField]
        private float _checkFacedObjectTimer = 1f;
        [SerializeField]
        private float _checkFacedObjectMax = 1f;
        [SerializeField]
        private PlayerGun _playerGun;
        [SerializeField]
        private PlayerNotebook _playerNotebook;

        void Start()
        {
            AkSoundEngine.PostEvent("Play_sfx_train_interior", Camera.main.gameObject);
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
                AttemptInteract();
            }

            if(Input.GetKeyDown(KeyCode.R))
            {
                EquipGun();
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                EquipNotebook();
            }

            if (Input.GetMouseButtonDown(0))
            {
                ShootGun();
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
            LayerMask defaultMask      = LayerMask.GetMask("Default");
            Ray ray                    = Camera.main.ScreenPointToRay(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hit;
            if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, 5, interactableMask | defaultMask))
            {
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

        void AttemptInteract()
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
                TrainMysteryGameManager.Instance.uiCommands.SetFacedObjectLabel(string.Empty);
            }
            else
            {
                TrainMysteryGameManager.Instance.uiCommands.SetFacedObjectLabel(_facedInteractable.transform.name + " <sprite=0>");
            }

        }

        public void Footstep()
        {
            AkSoundEngine.PostEvent("Play_sfx_footstep", Camera.main.gameObject);
        }

        public void ShootGun()
        {
            _playerGun.Shoot();
        }

        public void EquipGun()
        {
            if(_playerNotebook.isEquipped)
            {
                _playerNotebook.Equip(false);
            }

            // gun already equipped
            if (_playerGun.isEquipped)
            {
                _playerGun.Equip(false);
                return;
            }

            if (TrainMysteryGameManager.Instance.yarnVariables.GetValue("$HasGun").AsBool)
            {
                _playerGun.Equip(true);
            }
        }

        public void EquipNotebook()
        {
            if (_playerGun.isEquipped)
            {
                _playerGun.Equip(false);
            }

            // notebook already equipped
            if (_playerNotebook.isEquipped)
            {
                _playerNotebook.Equip(false);
                return;
            }

            if (TrainMysteryGameManager.Instance.yarnVariables.GetValue("$HasNotebook").AsBool)
            {
                _playerNotebook.Equip(true);
            }
        }
    } 
}
