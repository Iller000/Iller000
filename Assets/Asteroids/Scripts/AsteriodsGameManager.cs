using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteriodsGameManager : MonoBehaviour
{
    public static int lives = 3;

    public static void SpaceshipDied(Spaceship ship)
    {
        ship.gameObject.SetActive(true);

        lives--;
    }
}
