using TMPro;
using UI;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using Utilities.FSM;

namespace Player.PlayerStates
{
    public class PlayerLockedState : State
    {
        private readonly ChequeTable _chequeTable;
        
        private readonly Button _openButton;

        private readonly TMP_Text _tooltipField;
        
        private readonly CinemachineCamera _overviewCamera;
        
        public PlayerLockedState(StateType stateType, CinemachineCamera overviewCamera, ChequeTable chequeTable, Button openButton, TMP_Text tooltipText)
        {
            StateType = stateType;
            
            _chequeTable = chequeTable;
            
            _openButton = openButton;
            
            _tooltipField = tooltipText;
            _tooltipField.text = "Нажмите ПКМ, что бы взаимодействовать с UI";
            
            _overviewCamera = overviewCamera;
        }   
        
        public override void Enter()
        {
            _overviewCamera.gameObject.SetActive(true);
            
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            
            _tooltipField.text = "Нажмите ПКМ, чтобы выйти из режима взаимодействия";

            _openButton.interactable = true;
        }

        public override void Update()
        {
            
        }

        public override void Exit()
        {
            _overviewCamera.gameObject.SetActive(false);
            
            _openButton.interactable = false;
            
            _chequeTable.Close();
            
            _tooltipField.text = "Нажмите ПКМ, что бы взаимодействовать с UI";
        }
    }
}