using UnityEngine;
using System.Collections;
using UnityEditorInternal;

public class Avatar : MonoBehaviour
{
    public float MovementSpeed = 1.0f;
    public float Temp = 1.0f;

    private enum MovingDir
    {
        UP,
        UPRIGHT,
        UPLEFT,
        DOWN,
        DOWNRIGHT,
        DOWNLEFT,
        LEFT,
        RIGHT,
        IDLE
    }

    private Animator _animator;
    private MovingDir _dir;
    private GameObject _room;
    private Stage _stage;

    private Vector3 _scale;

    public void Awake()
    {
        Debug.Log( "loading settings from settings setting set" );

        _room = GameObject.Find( "Stage" );
        if ( _room == null )
            throw new UnityException( "Could not find required Stage object" );

        _stage = _room.GetComponent<Stage>();
        if ( _stage == null ) {
            throw new UnityException( "Stage object did not have required 'Stage' component" );
        }

        FindMaxY();
    }

    private void FindMaxY()
    {
        BoxCollider2D[] colliders = gameObject.GetComponents<BoxCollider2D>();

        if ( colliders.Length == 0 )
            throw new UnityException( "Avatar has no 2D colliders defined" );

        foreach ( var col in colliders ) {
            //if ()
            Debug.Log( "col.GetInstanceID() = " + col.GetInstanceID() );
            Debug.Log( "col.transform.position.y = " + col.transform.position.y );
        }

    }

    public void Start()
    {
        _animator = GetComponent<Animator>();
        _dir = MovingDir.IDLE;
    }

    public void FixedUpdate()
    {
        DoMovement();
    }

    // Update is called once per frame
    public void Update()
    {
        if ( Input.GetKey( KeyCode.DownArrow ) || Input.GetKey( KeyCode.S ) ) {
            _animator.Play( "Down" );
            if ( Input.GetKey( KeyCode.LeftArrow ) || Input.GetKey( KeyCode.A ) ) {
                _dir = MovingDir.DOWNLEFT;
            }
            else if ( Input.GetKey( KeyCode.RightArrow ) || Input.GetKey( KeyCode.D ) ) {
                _dir = MovingDir.DOWNRIGHT;
            }
            else {
                _dir = MovingDir.DOWN;
            }
        }
        else if ( Input.GetKey( KeyCode.UpArrow ) || Input.GetKey( KeyCode.W ) ) {
            _animator.Play( "Up" );
            if ( Input.GetKey( KeyCode.LeftArrow ) || Input.GetKey( KeyCode.A ) ) {
                _dir = MovingDir.UPLEFT;
            }
            else if ( Input.GetKey( KeyCode.RightArrow ) || Input.GetKey( KeyCode.D ) ) {
                _dir = MovingDir.UPRIGHT;
            }
            else {
                _dir = MovingDir.UP;
            }
        }
        else if ( Input.GetKey( KeyCode.LeftArrow ) || Input.GetKey( KeyCode.A ) ) {
            _animator.Play( "Left" );
            _dir = MovingDir.LEFT;
        }
        else if ( Input.GetKey( KeyCode.RightArrow ) || Input.GetKey( KeyCode.D ) ) {
            _animator.Play( "Right" );
            _dir = MovingDir.RIGHT;
        }
        else {
            _animator.Play( "IdleDown" );
            _dir = MovingDir.IDLE;
            rigidbody2D.velocity = Vector2.zero;
        }
    }

    private void DoMovement()
    {
        ScalePlayer();

        Vector2 speed = new Vector2( 0, 0 );
        float y_scale = - ( ( 0.5f - YNormalized ) * 1.2f );
        switch ( _dir ) {
            case MovingDir.UP:
                speed.y = MovementSpeed;
                break;
            case MovingDir.DOWN:
                speed.y = -MovementSpeed;
                break;
            case MovingDir.LEFT:
                speed.x = -MovementSpeed;
                break;
            case MovingDir.RIGHT:
                speed.x = MovementSpeed;
                break;
            case MovingDir.UPLEFT:
                speed.x = -MovementSpeed;
                speed.y = MovementSpeed;
                break;
            case MovingDir.UPRIGHT:
                speed.x = MovementSpeed;
                speed.y = MovementSpeed;
                break;
            case MovingDir.DOWNLEFT:
                speed.x = -MovementSpeed;
                speed.y = -MovementSpeed;
                break;
            case MovingDir.DOWNRIGHT:
                speed.x = MovementSpeed;
                speed.y = -MovementSpeed;
                break;
        }
        speed = speed * y_scale;
        transform.position = new Vector3( transform.position.x, transform.position.y, transform.position.y );
        rigidbody2D.MovePosition( rigidbody2D.position + speed * Time.deltaTime );
    }

    private float YNormalized
    {
        get { return -( 1 / ( ( Stage.STAGE_HEIGHT * 2 ) / ( transform.position.y - Stage.STAGE_HEIGHT ) ) ); }
    }

    private void ScalePlayer()
    {
        _scale.x = _scale.y = _stage.AvatarMinSize + ( ( _stage.AvatarMaxSize - _stage.AvatarMinSize ) * YNormalized );
        _scale.z = transform.localScale.z;

        transform.localScale = _scale;
    }
}
