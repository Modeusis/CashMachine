using DG.Tweening;
using Interactables;
using Player.Camera;
using Sounds;
using UnityEngine;
using Utilities.EventBus;
using Zenject;

namespace Animations
{
    public class CardAnimationHandler : MonoBehaviour
    {
        [Inject] private SoundService _soundService;
        [Inject] private EventBus _eventBus;

        [SerializeField] private GameObject player;
        [SerializeField] private GameObject cardHolder;
        
        [Header("Positions")]
        [SerializeField] private Vector3 cardInsertPosition = new Vector3(0.361560315f, 1.48735881f, -0.481290013f);
        [SerializeField] private Vector3 cardInsertedPosition = new Vector3(0.361999989f, 1.48735881f, -0.312999994f);
        [SerializeField] private Vector3 cardInvisiblePosition = new Vector3(0.120999999f, -0.316000015f, 0.442000002f);
        [SerializeField] private Vector3 cardShowedPosition = new Vector3(0.120999999f, -0.158000007f, 0.442000002f);
        
        [Header("Rotations")]
        [SerializeField] private Vector3 idleRotation = new Vector3(0f, 180f, 90f);
        [SerializeField] private Vector3 insertRotation = new Vector3(270f , 90f, 0);
        
        [Header("Settings")]
        [SerializeField] private float rotationDuration = 0.5f;
        [SerializeField] private float showDuration = 0.5f;
        [SerializeField] private float insertDuration = 0.6f;
        [SerializeField] private float removeDuration = 0.5f;
        [SerializeField] private float getDuration = 0.5f;
        [SerializeField] private float toInsertDuration = 0.65f;
        [SerializeField] private float insertDelay = 0.1f;
        
        
        private void Awake()
        {
            _eventBus.Subscribe<InteractionType>(HandleCardInsert);
        }

        private void Start()
        {
            transform.SetParent(player.transform);
            
            transform.localPosition = cardInvisiblePosition;
            transform.localRotation = Quaternion.Euler(idleRotation);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                InsertCard();
            }
            
            else if (Input.GetKeyDown(KeyCode.B))
            {
                GetCard();
            }
            
            else if (Input.GetKeyDown(KeyCode.N))
            {
                RemoveCard();
            }
        }

        private void InsertCard()
        {
            transform.DOKill();

            var delay = toInsertDuration + insertDelay + Time.deltaTime;
            
            transform.DOLocalMove(cardShowedPosition, showDuration).OnComplete(() =>
            {
                transform.SetParent(cardHolder.transform);
            });
            
            transform.DOLocalMove(cardInsertPosition, toInsertDuration).SetDelay(showDuration);
            transform.DOLocalRotate(insertRotation, rotationDuration).SetDelay(showDuration).OnComplete(() =>
            {
                transform.DOLocalMove(cardInsertedPosition, insertDuration).SetDelay(insertDelay).OnComplete(
                    () =>
                    {
                        _eventBus.Publish(new CameraUnblocker());
                    });
            });
        }

        private void RemoveCard()
        {
            transform.DOKill();
            
            transform.DOLocalMove(cardInsertPosition, removeDuration);
        }

        private void GetCard()
        {
            transform.DOKill();
            
            transform.SetParent(player.transform);
            
            transform.DOLocalMove(cardInvisiblePosition, getDuration);
            transform.DOLocalRotate(idleRotation, getDuration, RotateMode.FastBeyond360);
        }
        
        private void HandleCardInsert(InteractionType interaction)
        {
            
        }
    }
}