using Runningboy.Data;
using Runningboy.Manager;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Runningboy.Map
{
    public class CheckPoint : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                if (MapManager.instance.currentSection.sectionData.sectionNumber == 1)
                {
                    PlayerData.instance.SetLifeMax();
                }
                GameManager.instance.SaveGame();
            }
        }
    }
}