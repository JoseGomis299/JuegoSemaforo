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

   // protected override float GetSpeed()
   // {
   //    return speed;
   // }

   protected override void SetDirection()
   {
      RaycastHit2D hit = Physics2D.BoxCast(transform.position, Bounds.size, 0, Vector3.right, 0.5f+Bounds.extents.x, carLayer);
      direction = Vector3.Lerp(direction, hit ? GetAvoidDirection() : Vector3.right, Time.deltaTime/0.05f);
      direction.Normalize();
   }

   private Vector3 _lastTop;
   private Vector3 _lastBot;

   private Vector3 GetAvoidDirection()
   {
      // RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.up, Bounds.extents.x, streetLayer);
      // if(hit) return Vector3.down;
      // hit = Physics2D.Raycast(transform.position, Vector3.down, Bounds.extents.x, streetLayer);
      // if(hit) return Vector3.up;
      //
      RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, Mathf.Max(Bounds.extents.x + 0.5f,Mathf.Abs(_lastBot.y - transform.position.y)),
         carLayer | streetLayer);
      if (hit)
      {
         _lastBot = hit.point;
         return Vector3.up;
      }

      hit = Physics2D.Raycast(transform.position, Vector3.up, Mathf.Max(Bounds.extents.x + 0.5f,Mathf.Abs(_lastTop.y - transform.position.y)),
         carLayer | streetLayer);
      {
         _lastTop = hit.point;
         return Vector3.down;
      }
      return Vector3.up;
   }

   protected override void OnDrawGizmosSelected()
   {
      Gizmos.color = Color.green;
      Gizmos.DrawWireCube(transform.position + Vector3.right*(0.5f+Bounds.extents.x), Bounds.size);
   } 
}
