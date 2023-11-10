using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Singleton
    public static Player Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Debug.LogError("There is more than one Player instance");
        }
        Instance = this;
    }

    // Event
    public event EventHandler<OnSelectedCounterChangedEventArgs> onSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs: EventArgs
    {
        public ClearCounter selectedCounter;
    }


    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterLayerMask;

    private bool isWalking = false;
    private Vector3 lastInteractDir;
    private ClearCounter selectedCounter;

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if(selectedCounter != null)
        {
            selectedCounter.Interact();
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void HandleInteractions()
    {
        Vector2 input = gameInput.GetMovementVector();

        Vector3 dirVector = new Vector3(input.x, 0, input.y);

        if(dirVector != Vector3.zero)
        {
            lastInteractDir = dirVector;
        }

        float interactDistance = 2f;
        if(Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, counterLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                if (clearCounter != selectedCounter)
                {
                    SetSelectedCounter(clearCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }


    }

    private void HandleMovement()
    {
        Vector2 input = gameInput.GetMovementVector();

        Vector3 dirVector = new Vector3(input.x, 0, input.y);

        float moveDistance = Time.deltaTime * moveSpeed;
        float playerRadius = 0.7f;
        float playerHeight = 2f;

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, dirVector, moveDistance);

        if (!canMove)
        {
            // Try X axis
            Vector3 dirVectorX = new Vector3(dirVector.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, dirVectorX, moveDistance);

            if (canMove)
            {
                dirVector = dirVectorX;
            }
            else
            {
                // try Z axis
                Vector3 dirVectorZ = new Vector3(0, 0, dirVector.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, dirVectorZ, moveDistance);

                if (canMove)
                {
                    dirVector = dirVectorZ;
                }
            }
        }

        if (canMove)
        {
            transform.position += dirVector * moveDistance;
        }
        isWalking = dirVector != Vector3.zero;
        transform.forward = Vector3.Slerp(transform.forward, dirVector, Time.deltaTime * rotateSpeed);
    }

    private void SetSelectedCounter(ClearCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        onSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }
}
