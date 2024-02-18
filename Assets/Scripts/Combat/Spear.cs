using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
   public float life = 5;

   void Awake()
   {
    Destroy(gameObject, life);
   }


}
