using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : MonoBehaviour
{
    public static Grounded Instance;


    [SerializeField] private Transform feetTransform;
    [SerializeField] private Vector2 collisionArea = new Vector2(0, 0);
    [SerializeField] private LayerMask collisionLayerMask;
    [SerializeField] private LayerMask rocks;

    public void Awake()
    {
        if (Instance == null) { Instance = this; }

    }

    public bool IsGrounded()
    {
        // this combines the collisionLayerMask and rocks layer mask
        LayerMask combinedLayerMask = collisionLayerMask | rocks;
        Collider2D isGrounded = Physics2D.OverlapBox(feetTransform.position, collisionArea, 0, combinedLayerMask);
        return isGrounded;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(feetTransform.position, collisionArea);
    }
}
