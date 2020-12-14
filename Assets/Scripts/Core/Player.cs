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
        [SerializeField]
        public int notebookPage = 21;


        private bool _hasShot = false;
        private float _scrollTimer = 0f;
        private static float MAX_SCROLL_TIME = 0.4f;
        private bool firstScroll = true;

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

            UpdateMouseWheel();
        }

        private void UpdateMouseWheel()
        {
            _scrollTimer += deltaTime;

            //if (Input.GetKeyDown(KeyCode.P))
            //{
            //    notebookPage++;
            //}
            //if (Input.GetKeyDown(KeyCode.O))
            //{
            //    notebookPage--;
            //}

            if(_scrollTimer < MAX_SCROLL_TIME)
            {
                return;
            }

            var scrollwheel = Input.GetAxis("Mouse ScrollWheel");
            if (scrollwheel > 0)
            {
                _scrollTimer = 0f;
                if (TrainMysteryGameManager.Instance.yarnVariables.GetValue("$NotebookEquipped").AsBool != true)
                {
                    return;
                }

                if (firstScroll)
                {
                    firstScroll = false;
                    notebookPage = 21;
                }
                else
                {
                    notebookPage++;
                    if (notebookPage > 25)
                    {
                        notebookPage = 0;
                    }
                }
                TrainMysteryGameManager.Instance.uiCommands.SetPage(notebookPage);
            }
            else if (scrollwheel < 0)
            {
                _scrollTimer = 0f;
                if (TrainMysteryGameManager.Instance.yarnVariables.GetValue("$NotebookEquipped").AsBool != true)
                {
                    return;
                }

                if (firstScroll)
                {
                    firstScroll = false;
                    notebookPage = 21;
                }
                else
                {
                    notebookPage--;
                    if (notebookPage < 0)
                    {
                        notebookPage = 25;
                    }
                }
                TrainMysteryGameManager.Instance.uiCommands.SetPage(notebookPage);
            }
        }

        public void SetPageIndex(int index)
        {
            firstScroll = false;
            notebookPage = index;
            TrainMysteryGameManager.Instance.uiCommands.SetPage(notebookPage);
        }

        void LookForObjectInFront(float distance = 5, bool updateName = true, bool forced = false)
        {
            if(forced)
            {
                goto force;
            }

            // check timer
            if (_checkFacedObjectTimer > 0)
            {
                _checkFacedObjectTimer -= deltaTime;
                return;
            }
            _checkFacedObjectTimer = _checkFacedObjectMax;

            force: // lol

            // look for object
            LayerMask interactableMask = LayerMask.GetMask("Interactable");
            LayerMask defaultMask      = LayerMask.GetMask("Default");
            Ray ray                    = Camera.main.ScreenPointToRay(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hit;
            if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, distance, interactableMask | defaultMask))
            {
                var interactable = hit.collider.gameObject.GetComponent<Interactable>() != null ? hit.collider.gameObject.GetComponent<Interactable>() : null;
                if (interactable != _facedInteractable)
                {
                    _facedInteractable = interactable;
                    if(updateName)
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
            if(!_playerGun.isEquipped)
            {
                return;
            }

            if(_hasShot)
            {
                return;
            }

            _playerGun.Shoot();
            _hasShot = true;

            LookForObjectInFront(100f, false, true);
            if (_facedInteractable && _facedInteractable is Interactable_NPC)
            {
                TrainMysteryGameManager.Instance.GameOver_Murderer(_facedInteractable.gameObject.name);
            }
            else
            {
                TrainMysteryGameManager.Instance.GameOver_ShotNothing();
            }
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
