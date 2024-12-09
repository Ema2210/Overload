using Cinemachine.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static event Action OnCrash;

    private const float WeightRate = 40.0f;

    [Header("Movement")]

    [SerializeField] float horizontalMovementSpeed = 1;
    [SerializeField] float breakSpeed = 1;
    [SerializeField] float verticalMovementSpeed = 1;
    [SerializeField] float maxHorizontalSpeed = 5f;
    [SerializeField] float maxVerticalSpeed = 5f;
    [SerializeField] float verticalSmoothingFactor = 1;

    [Header("Fall Damage")]
    [SerializeField] float thresholdVelocityForFallDamage = 10;

    [Header("Earthquakes")]
    [SerializeField] Vector3 EathquakeUpVector;
    [SerializeField] float earthquakeThrustLenght = 0.6f;
    [SerializeField] float earthquakeThrustUpStrenght = 11;

    [Header("Player Components")]
    [SerializeField] PlayerEquipment playerEquipment;
    [SerializeField] Animator myAnimator;



    Rigidbody2D myRigidbody;
    InputReader inputReader;
    FuelManager fuelManager;
    HealthManager healthManager;

    bool wasGrounded;
    Vector2 playerVelocity;
    float normalDrag;
    Vector2 previousVelocity;
    Vector2 scale;

    bool playerIsMovingHorizontally;
    bool playerIsMovingVertically;
    bool mustCheckFallDamage = false;
    float breakForceDirection;
    float breakingForce;
    float engineSpeed;

    float finalHorizontalVelocity;
    float finalVerticalVelocity;

    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        inputReader = GetComponent<InputReader>();
        fuelManager = GetComponent<FuelManager>();
        healthManager = GetComponent<HealthManager>();



        normalDrag = myRigidbody.drag;
        scale = transform.localScale;
    }

    private void OnEnable()
    {
        Earthquakes.OnEarthquake += EarthquakesJumpUp;
        SmallExplosive.SmallExplosiveOnUse += Explosion;
        PlasticExplosive.PlasticExplosiveOnUse += Explosion;
    }

    private void OnDisable()
    {
        Earthquakes.OnEarthquake -= EarthquakesJumpUp;
        SmallExplosive.SmallExplosiveOnUse -= Explosion;
        PlasticExplosive.PlasticExplosiveOnUse -= Explosion;

    }


    void Start()
    {
        if (!Pause.IsPaused) return;
        Pause.TogglePause();
    }

    void Update()
    {
        if (Pause.IsPaused || HealthManager.IsDead) return;

        // Input Handling
        if (!Drill.IsDigging)
        {
            float inputX = inputReader.MovementValue.x;
            float inputY = inputReader.MovementValue.y;

            float engineSpeed = playerEquipment.equippedEngine.MovementSpeed;
            float weighFactor = (WeightRate - Inventory.GetCargoWeight()) / WeightRate;

            float horizontalVelocity = inputX * engineSpeed * horizontalMovementSpeed;
            float verticalVelocity = inputY * engineSpeed * verticalMovementSpeed * weighFactor;

            playerVelocity = new Vector2(horizontalVelocity, verticalVelocity);
        }
        else
        {
            playerVelocity = Vector2.zero;
        }

        playerIsMovingHorizontally = Mathf.Abs(myRigidbody.velocity.x) > 0.1f;
        playerIsMovingVertically = Mathf.Abs(myRigidbody.velocity.y) > 0.1f;

        breakForceDirection = Mathf.Sign(myRigidbody.velocity.x);
        breakingForce = breakSpeed * breakForceDirection;

        EngineSpeed();



        if (Drill.IsDigging)
        {

            if (Drill.IsDiggingDown)
            {
                myAnimator.SetBool("IsDiggingDown", true);
                Debug.Log("I am digging");
            }
            else
            {
                myAnimator.SetBool("IsDigging", true);
            }

            return;
        }
        else if (!Grounded.Instance.IsGrounded())
        {
            //SoundManager.Instance.PlaySound(Sounds.Flying);
            myAnimator.SetBool("IsDigging", false);
            myAnimator.SetBool("IsDiggingDown", false);
        }
        else
        {
            //SoundManager.Instance.StopSounds();
            myAnimator.SetBool("IsDigging", false);
            myAnimator.SetBool("IsDiggingDown", false);
        }

        if (playerIsMovingVertically && myRigidbody.velocity.y > 1.0f)
        {
            fuelManager.ChangeDepletionRateScheme(TypeOfFuelDepletionRate.VeryHigh);

        }
        else if (playerIsMovingHorizontally)
        {
            fuelManager.ChangeDepletionRateScheme(TypeOfFuelDepletionRate.High);

        }
        else
        {
            fuelManager.ChangeDepletionRateScheme(TypeOfFuelDepletionRate.Standard);
        }

        if (!Drill.IsDigging)
        {

            myAnimator.SetBool("IsFlying", !Grounded.Instance.IsGrounded());
            HandleGroundedState();
        }


        if (!Grounded.Instance.IsGrounded()) return;
        myAnimator.SetBool("IsRunning", playerIsMovingHorizontally);
        myAnimator.SetBool("IsIdling", !playerIsMovingHorizontally);



    }
    private void EarthquakesJumpUp()
    {
        StartCoroutine(IncrementalEarthquakeForce());
    }

    IEnumerator IncrementalEarthquakeForce()
    {

        yield return new WaitForSeconds(earthquakeThrustLenght);


        float earthquakeTime = 0;


        while (earthquakeTime < earthquakeThrustLenght)
        {
            earthquakeTime += Time.deltaTime;
            myRigidbody.velocity = Vector2.up * earthquakeThrustUpStrenght;
            yield return null;
        }


    }
    void FlipSprite()
    {
        if (Drill.IsDigging) return;
        if (Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon)  //myRigidbody.velocity.x
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x) * scale.x, scale.y);
        }
    }

    void FixedUpdate()
    {
        if (Pause.IsPaused || HealthManager.IsDead) return;
        if (Drill.IsDigging) return;

        if (!Drill.IsDigging)
        {
            previousVelocity = myRigidbody.velocity; //should be last I think
        }
        finalVerticalVelocity = myRigidbody.velocity.y;//N

        // Physics-related calculations
        Move();
        HandleDrag();
        if (Mathf.Abs(inputReader.MovementValue.x - previousVelocity.x) > 0.1f)
        {
            FlipSprite();
        }
        if (playerIsMovingVertically && myRigidbody.velocity.y > 3.0f)
        {


        myRigidbody.velocity = new Vector2(myRigidbody.velocity.x * verticalSmoothingFactor, myRigidbody.velocity.y);
        }

    }
    void HandleGroundedState()
    {
        wasGrounded = Grounded.Instance.IsGrounded();

    }

    void HandleDrag()
    {
        myRigidbody.drag = Grounded.Instance.IsGrounded() ? normalDrag : 0;
    }

    void EngineSpeed()
    {
        engineSpeed = playerEquipment.equippedEngine.MovementSpeed;
    }
    void Move()
    {
        if (Pause.IsPaused) return;
        myRigidbody.velocity += playerVelocity;

        if (inputReader.IsDownBreaking)
        {
            myRigidbody.velocity -= new Vector2(breakingForce, 0);
            if (Mathf.Sign(myRigidbody.velocity.x) != breakForceDirection)
            {
                myRigidbody.velocity = new Vector2(0, myRigidbody.velocity.y);
            }
        }

        myRigidbody.velocity = new Vector2(
            Mathf.Clamp(myRigidbody.velocity.x, -maxHorizontalSpeed * engineSpeed, maxHorizontalSpeed * engineSpeed),
            Mathf.Clamp(myRigidbody.velocity.y, -maxVerticalSpeed * engineSpeed, maxVerticalSpeed * engineSpeed)
        );


    }



    void FallDamage(float crashSpeed)
    {
        if (MathF.Abs(myRigidbody.velocity.y) > 0.2f) return;

        float fallDamage = (crashSpeed - thresholdVelocityForFallDamage) * 5f - 10; //it's a negative value

        Debug.Log("fall damage confirmed: " + fallDamage);
        if (fallDamage < 0)
        {
            healthManager.ChangeHealth(fallDamage);
            Explosion();

        }

    }

    private static void Explosion()
    {
        OnCrash?.Invoke();
    }

    public Vector2Int GetGridPosition()
    {
        return new Vector2Int
            (
            Mathf.RoundToInt(transform.position.x),
            Mathf.RoundToInt(transform.position.z)

            );
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (finalVerticalVelocity < thresholdVelocityForFallDamage)
        {
            FallDamage(finalVerticalVelocity);
        }
    }

    public void StopMovement()
    {
        myRigidbody.velocity = new Vector3(myRigidbody.velocity.x/2, myRigidbody.velocity.y / 2, 0);
    }
}
