using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ambulance : Car
{
   [Header("AI")]
   [SerializeField] private LayerMask streetLayer;

   private float rayDist;

   private void OnEnable()
   {
      SetDirection();
      rayDist = 0.5f;
      _bot = _top = false;
   }

   protected override float GetSpeed()
   {
      return speed;
   }

   protected override void SetDirection()
   {
      RaycastHit2D hit = Physics2D.BoxCast(center, Bounds.size/2.5f, 0, Vector3.right, rayDist+Bounds.extents.x, carLayer);
      if (hit)
      {
         rayDist = hit.distance + hit.transform.GetComponent<Renderer>().bounds.extents.x;
      }
      else
      {
         _bot = _top = false;
         rayDist = 0.5f;
      }
      direction = Vector3.Lerp(direction, hit ? GetAvoidDirection() : Vector3.right, Time.deltaTime*10);
   }

   private Vector3 _lastTop;
   private Vector3 _lastBot;

   private bool _bot;
   private bool _top;

   private Vector3 GetAvoidDirection()
   {
      // RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.up, Bounds.extents.x, streetLayer);
      // if(hit) return Vector3.down;
      // hit = Physics2D.Raycast(transform.position, Vector3.down, Bounds.extents.x, streetLayer);
      // if(hit) return Vector3.up;
      //
      RaycastHit2D hit;
      hit = Physics2D.Raycast(center, Vector3.down, Bounds.extents.y + 0.1f, carLayer | streetLayer);
      if (hit)
      {
         _bot = true;
         _lastBot = hit.point;
         return Vector3.up;
      }

      hit = Physics2D.Raycast(center, Vector3.up,Bounds.extents.y + 0.1f, carLayer | streetLayer);
      if(hit)
      {
         _top = true;
         _lastTop = hit.point;
         return Vector3.down;
      }
         
      return _bot ? Vector3.up : Vector3.down;
   }

   protected override void OnDrawGizmosSelected()
   {
      Gizmos.color = Color.green;
      Gizmos.DrawWireCube(center + Vector3.right*(rayDist+Bounds.extents.x), Bounds.size/2.5f);
   } 
}
