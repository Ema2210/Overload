using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypesOfChecks
{
    Right,
    Left,
    Down
}
public class DiggingCheck : MonoBehaviour
{

    public bool PositiveCheckRight { get; private set; }
    public bool PositiveCheckLeft { get; private set; }
    public bool PositiveCheckDown { get; private set; }

    [SerializeField] TypesOfChecks type;

    private void Update()
    {
        Collider2D hitCollider = Physics2D.OverlapBox(transform.position, new Vector2(0.2f, 0.2f), 0, LayerMask.GetMask("Ground"));


        if (hitCollider == null)
        {
            switch (type)
            {
                case TypesOfChecks.Right:
                    PositiveCheckRight = false;
                    break;
                case TypesOfChecks.Left:
                    PositiveCheckLeft = false;
                    break;
                case TypesOfChecks.Down:
                    PositiveCheckDown = false;
                    break;
            }
        }
        else
        {
            switch (type)
            {
                case TypesOfChecks.Right:
                    PositiveCheckRight = true;
                    break;
                case TypesOfChecks.Left:
                    PositiveCheckLeft = true;
                    break;
                case TypesOfChecks.Down:
                    PositiveCheckDown = true;
                    break;
            }
        }

        


    }

    void OnDrawGizmos()
    {
        // Set the color of the Gizmo
        Gizmos.color = Color.red;

        // Draw a cube at the position where the OverlapBox is checked
        // Note: Gizmos.DrawWireCube draws from the center of the cube
        Gizmos.DrawWireCube(transform.position, new Vector3(0.1f, 0.1f, 0));
    }


}
