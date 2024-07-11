using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public ModuleController mc;

    public float moveSpeed;
    [SerializeField] float interactableRange = 3f;
    private bool isMoving;
    private Vector2 input;
    private Animator animator;
    public LayerMask solidObjectsLayer;
    public LayerMask interactableLayer;
    [SerializeField] FloatingJoystick floatingJoystick;
    [SerializeField] GameObject interactOutline;
    private Collider2D solidObjects;
    private Collider2D interactableCollider;
    private bool canwallslide;
    private bool isWallSliding;

    [Header("Collision Info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask whatIsGround;
    private bool IsGrounded;

    [SerializeField] private Transform wallcheck;
    [SerializeField] private float wallCheckDistance;
    private bool isWallDetected;

    LastKnownPosition lastKnownPosition = new LastKnownPosition();
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        OnLoadGameSetup();
    }
    private void OnLoadGameSetup()
    {

        //PlayerPosition();
        Debug.Log("ILoadGame: " + FlyHigh.IsLoadGame);
        if (!FlyHigh.IsLoadGame) return;
        transform.position = FlyHigh.playerLoadPos;
        FlyHigh.IsLoadGame = false;
        

    }
    public void Update()
    {
        HandleMovementInput();
        // HandleInteractionInput();
    }

    private void HandleMovementInput()
    {
        if (!isMoving)
        {

            input.x = floatingJoystick.Horizontal;
            input.y = floatingJoystick.Vertical;
            if (input.magnitude <= 0.1)
            {
                input.x = Input.GetAxisRaw("Horizontal");
                input.y = Input.GetAxisRaw("Vertical");
                input.Normalize();
            }

            if (input.magnitude >= 0.1f)
            {
                //input.y = Mathf.Abs(input.x) > 0 ? 0 : input.y;

                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                var targetPos = transform.position + (Vector3)input.normalized;

                if (IsWalkable(targetPos))
                    StartCoroutine(Move(targetPos));
            }
        }
        animator.SetBool("isMoving", isMoving);
    }

    // private void HandleInteractionInput()
    // {
    //     if (Input.GetKeyDown(KeyCode.Z))
    //         Interact();
    // }
    float detecTimer = 0;
    public float detectDelay = 0.5f;

    private void InteractableDetect()
    {
        detecTimer -= Time.deltaTime;


        if (detecTimer <= 0)
        {
            interactableCollider = Physics2D.OverlapCircle(transform.position, interactableRange, interactableLayer);
            if (interactableCollider != null)
            {
                if (interactableCollider.tag == "NPT")
                {
                   // string scene = SceneManager.GetActiveScene().name;
                    //if (scene == "Forest" || scene == "Cloud Village")
                   // {
                    //    mc.setModuleContent(scene); 
                    //}
                    
                }
                interactOutline.SetActive(true);
            }
            else
            {
                interactOutline.SetActive(false);
            }
        }

    }
    // Collider

    private void OnTriggerEnter2D(Collider2D collision)
    {
        interactableCollider = Physics2D.OverlapCircle(transform.position, interactableRange, interactableLayer);
        if (interactableCollider != null)
        {
            if (interactableCollider.tag == "NPT")
            {
                Debug.Log(interactableCollider.tag + "   ---   Working");

            }
        }
    }

    private void FixedUpdate()
    {
        InteractableDetect();
    }
    public void Interact()
    {
        var facingDir = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        var interactPos = transform.position + facingDir;

        if (interactableCollider != null)
        {
            interactableCollider.GetComponent<Interactable>()?.Interact();
        }
    }

    private IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        isMoving = false;
    }

    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.CompareTag("Player"))
    //         SceneManager.LoadScene("Forest");
    // }

    private bool IsWalkable(Vector3 targetPos)
    {
        /*solidObjects = Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer | interactableLayer);
        return solidObjects == null;*/

        if (Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer | interactableLayer) != null)
        {
            Debug.Log("ME pader");
            return false;
        }
        else
        {
            Debug.Log("Tanga wala");
            return true; 
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, interactableRange);
    }


    //CheckScene
    void PlayerPosition()
    {
        checkScene cs =new checkScene();

        transform.position = cs.checkLastScene();

    }
}
