using TMPro;
using UnityEngine;

namespace UI
{
    public class OperationTableElement : MonoBehaviour
    {
        [SerializeField] private TMP_Text operationDateText;
        [SerializeField] private TMP_Text operationText;

        public void Initialize(string operationDate, string operation)
        {
            operationDateText.text = operationDate;
            
            operationText.text = operation;
        }
    }
}