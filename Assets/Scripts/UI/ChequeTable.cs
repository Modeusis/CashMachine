using Sounds;
using UnityEngine;
using UnityEngine.UI;
using Utilities.EventBus;
using Zenject;

namespace UI
{
    public class ChequeTable : MonoBehaviour
    {
        [Inject] private EventBus _eventBus;
        [Inject] private SoundService _soundService;
        
        [SerializeField] private string writeDownSoundId = "WriteDown";
        
        [SerializeField] private GameObject chequeTable;
        [SerializeField] private OperationTableElement chequeElementPrefab;

        [SerializeField] private Button closeButton;
        [SerializeField] private Button openButton;
        [SerializeField] private Button clearButton;
        
        private void Awake()
        {
            _eventBus.Subscribe<ChequeData>(HandleOperation);
            
            closeButton.onClick.AddListener(Close);
            openButton.onClick.AddListener(Open);
            clearButton.onClick.AddListener(Clear);
            
            Close();
        }

        private void HandleOperation(ChequeData chequeData)
        {
            var chequeElement = Instantiate(chequeElementPrefab, chequeTable.transform);
            
            _soundService.Play(SoundType.Sound, writeDownSoundId);
            
            chequeElement.Initialize(chequeData.Date.ToString("dd/MM/yyyy HH:mm:ss"), chequeData.Operation);
        }

        public void Open()
        {
            Debug.Log("Open");
            
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
        
        private void Clear()
        {
            if (chequeTable.transform.childCount == 0)
                return;
            
            for (int i = 0; i < chequeTable.transform.childCount; i++)
            {
                Destroy(chequeTable.transform.GetChild(i).gameObject);
            }
        }
    }
}