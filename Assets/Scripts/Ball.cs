using UnityEngine;

public class Ball : MonoBehaviour
{
    // config parameters

    [SerializeField] Paddle paddle1;
    [SerializeField] float xPush = 2f;
    [SerializeField] float yPush = 15f;
    [SerializeField] float minBounceAngle = 15;
    [SerializeField] AudioClip[] BallSounds;
    //[SerializeField] float randomFactorMin = 0.2f;
    //[SerializeField] float randomFactorMax = 0.2f;

    //state
    Vector2 paddleToBallVector;
    bool hasStarted = false;

    // Cached component references
    AudioSource myAudioSource;
    Rigidbody2D myRigidBody2D;

    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = transform.position - paddle1.transform.position;
        myAudioSource = GetComponent<AudioSource>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!hasStarted)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }

    }

    private void LaunchOnMouseClick()
    {

        if (Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            myRigidBody2D.velocity = new Vector2(xPush, yPush);
        }

    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        //Vector2 velocityTweak = new Vector2
        //    (Random.Range(randomFactorMin, randomFactorMax),
        //    Random.Range(randomFactorMin, randomFactorMax));

        if (hasStarted)
        {
            AudioClip clip = BallSounds[UnityEngine.Random.Range(0, BallSounds.Length)];
            myAudioSource.PlayOneShot(clip);
            AdjustBallVector();
          //  myRigidBody2D.velocity += velocityTweak;
        }

    }

    private void AdjustBallVector() //function to adjust ball's bounce vector on collision when it is lower than min bounce angle
    {

        float bounceAngle = Mathf.Atan2(myRigidBody2D.velocity.y, myRigidBody2D.velocity.x) * Mathf.Rad2Deg;
        float magnitude = myRigidBody2D.velocity.magnitude;


        float newAngle = 0;
        if (IsNumInRange(bounceAngle, 0, 90))
        {
            newAngle = Mathf.Clamp(bounceAngle, minBounceAngle, 90 - minBounceAngle);
        }
        else if (IsNumInRange(bounceAngle, 90, 180))
        {
            newAngle = Mathf.Clamp(bounceAngle, 90 + minBounceAngle, 180 - minBounceAngle);
        }
        else if (IsNumInRange(bounceAngle, -90, 0))
        {
            newAngle = Mathf.Clamp(bounceAngle, -90 + minBounceAngle, -minBounceAngle);
        }
        else if (IsNumInRange(bounceAngle, -180, -90))
        {
            newAngle = Mathf.Clamp(bounceAngle, -180 + minBounceAngle, -90 - minBounceAngle);
        }

        Vector2 newVelocity = new Vector2(Mathf.Cos(newAngle * Mathf.Deg2Rad), Mathf.Sin(newAngle * Mathf.Deg2Rad)) * magnitude;
        myRigidBody2D.velocity = newVelocity;

    }

    private bool IsNumInRange(float num, float min, float max) // function to check if current bounce angle is in range 
    {
        return num >= min && num <= max;
    }
}
