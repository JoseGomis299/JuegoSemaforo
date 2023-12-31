using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Ambulance : Car
{
   [Header("AI")]
   [SerializeField] private LayerMask streetLayer;

   [SerializeField] private GameObject lights;
   [SerializeField] private GameObject upLight;
   [SerializeField] private GameObject downLight;

   [SerializeField] private float lightBob = 0.08f;
   [SerializeField] private float lightPulse = 0.5f;
   private float lastPulse = 0f;
   private float lastBob = 0f;

   private float rayDist;
   private Transform lastHit;
   
   private Vector3 lastDir;

   private void OnEnable()
   {
      SetDirection();
      rayDist = 0.1f;
      lastDir = Vector3.up;
      lastPulse = Time.time;
      lastBob = Time.time;
   }

   protected override float GetSpeed()
   {
      Lights();
      
      if (isRush)
      {
         return 8f;
      }
      
      RaycastHit2D hit = Physics2D.BoxCast(center, Bounds.size/2f, 0, transform.right, minDistance+Bounds.extents.x, carLayer);
      if (hit)
      {
         if (GameManager.menuactive)
         {
            float newSpeed = speed * (hit.distance / (4 * Bounds.size.x));

            if (newSpeed < 0.5f)
            {
               newSpeed = 0f;
            }

            return newSpeed;
         }
         return speed * (hit.distance / Bounds.size.x);
      }
      
      return speed;
   }

   protected override void SetDirection()
   {
      if (isRush)
      {
         direction = new Vector3(1, 0, 0);
         return;
      }
      
      RaycastHit2D hit = Physics2D.BoxCast(center, Bounds.size/1.5f, 0, Vector3.right, rayDist+Bounds.extents.x, carLayer);
      if (hit)
      {
         rayDist = hit.distance;
         if (lastHit != hit.transform) rayDist = 0.1f;
         lastHit = hit.transform;
      }
      else
      {
         rayDist = 0.1f;
      }
      direction = Vector3.Lerp(direction, hit ? GetAvoidDirection() : Vector3.right, Time.deltaTime*10);
   }


   private Vector3 GetAvoidDirection()
   {
      if (GameManager.menuactive || isRush)
      {
         return new Vector3(1, 0, 0);
      }
      
      
      // RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.up, Bounds.extents.x, streetLayer);
      // if(hit) return Vector3.down;
      // hit = Physics2D.Raycast(transform.position, Vector3.down, Bounds.extents.x, streetLayer);
      // if(hit) return Vector3.up;
      //
      RaycastHit2D hitDown = Physics2D.Raycast(center, Vector3.down, Mathf.Infinity, carLayer | streetLayer);
      RaycastHit2D hitUp = Physics2D.Raycast(center, Vector3.up,Mathf.Infinity, carLayer | streetLayer);
      if (hitDown && hitUp)
      {
         float dist = Bounds.size.y / 1.5f;
         if (hitUp.distance <  dist && hitDown.distance < dist) return Vector3.zero;

         if (hitDown.distance < dist)
         {
            lastDir = Vector3.up;
            return Vector3.up; 
         }
         if (hitUp.distance < dist)
         {
            lastDir = Vector3.down;
            return Vector3.down;
         }

         if(lastDir == Vector3.up && hitUp.transform.CompareTag("Car") && hitDown.distance > hitUp.distance) lastDir = Vector3.down;
      }
      
      return lastDir;
   }

   protected override void OnDrawGizmosSelected()
   {
      Gizmos.color = Color.green;
      Gizmos.DrawWireCube(center + Vector3.right*(rayDist+Bounds.extents.x), Bounds.size/2.5f);
      
      Gizmos.color = Color.red;
      Gizmos.DrawLine(center+ Vector3.up*(Bounds.size.y), center+ Vector3.down*(Bounds.size.y));
   }

   void Lights()
   {
      if (Time.time - lastPulse >= lightPulse)
      {
         upLight.SetActive(!upLight.activeSelf);
         downLight.SetActive(!downLight.activeSelf);

         lastPulse = Time.time;
      }

      if (Time.time - lastBob >= lightBob)
      {
         float y = 1f/18f;
         lights.transform.localPosition = new Vector3(0, y, 0) - lights.transform.localPosition;

         lastBob = Time.time;
      }
   }
}
