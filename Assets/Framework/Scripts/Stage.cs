using UnityEditor;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public float AvatarMinSize = 1.0f;
    public float AvatarMaxSize = 5.0f;
    public float AvatarZScaling = 1.0f;

    public const float STAGE_WIDTH = 19.2f / 2.0f;
    public const float STAGE_HEIGHT = 10.8f / 2.0f;

    private void OnDrawGizmos()
    {
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color( 255, 0, 0, 0.25f );

        Gizmos.DrawCube( transform.position, new Vector3( STAGE_WIDTH * 2, STAGE_HEIGHT * 2, 0 ) );
        Gizmos.DrawLine( new Vector3( transform.position.x - STAGE_WIDTH, 0, 0 ), new Vector3( transform.position.x + STAGE_WIDTH, 0, 0 ) );

        //TextGizmo.Draw( transform.position, "ASSMASTER" );

        //float foo = Random.Range( -STAGE_HEIGHT, STAGE_HEIGHT );
        //Gizmos.color = new Color( Random.Range( 0, 255 ), Random.Range( 0, 255 ), Random.Range( 0.0f, 1.0f ), 1.0f );
        //Gizmos.DrawLine( new Vector3( transform.position.x - STAGE_WIDTH, foo, 0 ), new Vector3( transform.position.x + STAGE_WIDTH, foo, 0 ) );

        //Gizmos.DrawSphere( transform.position, 1 );
    }

}
