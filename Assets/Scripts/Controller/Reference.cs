using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Reference 
{
    public static GameObject thePlayer;
    public static GameObject canvas;
    public static List<EnemySpawner> spawner = new List<EnemySpawner>();
    public static List<EnemyController> allEnemies = new List<EnemyController>();
    public static LevelManager levelManager;

    public static List<NavPoint> navPoints = new List<NavPoint>();

    public static ScreenShake screenShake;  
    public static float maxDistanceInALevel = 1000;

    public static LayerMask wallLayer = LayerMask.GetMask("Walls");
    public static LayerMask enemiesLayer = LayerMask.GetMask("Enemies");
}
