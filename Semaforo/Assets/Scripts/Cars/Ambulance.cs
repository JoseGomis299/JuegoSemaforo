using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ambulance : Car
{
   [Header("AI")]
   [SerializeField] private LayerMask streetLayer;

   private void OnEnable()
   {
      SetDirection();
   }

   protected override float GetSpeed()
   {
      return speed;
   }

   protected override void SetDirection()
   {
      RaycastHit2D hit = Physics2D.BoxCast(transform.position, Bounds.size, 0, Vector3.right, minDistance+Bounds.extents.x, carLayer);
      direction = Vector3.Lerp(direction, hit ? GetAvoidDirection() : Vector3.right, Time.deltaTime / 0.1f);
      direction.Normalize();
   }

   private Vector3 GetAvoidDirection()
   {
      RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.up, maxDistanceCheck, streetLayer);
      if(hit) return Vector3.down;
      hit = Physics2D.Raycast(transform.position, Vector3.down, maxDistanceCheck, streetLayer);
      if(hit) return Vector3.up;
      hit = Physics2D.Raycast(transform.position, Vector3.down + Vector3.right, maxDistanceCheck, carLayer);
      return hit ? Vector3.up : Vector3.down;
   }

   protected override void OnDrawGizmosSelected()
   {
      Gizmos.color = Color.green;
      Gizmos.DrawWireCube(transform.position + Vector3.right*(minDistance+Bounds.extents.x), Bounds.size);
   } 
}
