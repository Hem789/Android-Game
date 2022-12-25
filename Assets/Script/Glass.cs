using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass : MonoBehaviour
{
   void OnCollisionEnter(Collision a)
   {
       if(a.transform.gameObject.tag=="Bullet" || a.transform.gameObject.tag=="Bullet0" ||a.transform.gameObject.tag=="BigBullet"||a.transform.gameObject.tag=="BigBullet2"||a.transform.gameObject.tag=="Sniper")
       {
           Destroy(gameObject);
       }
   }
}
