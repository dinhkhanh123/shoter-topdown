using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] public bool alarmSounded;
    [SerializeField] private SceneAsset nextLevel;
    [SerializeField] private float secondBeforeNextLevel;
    [SerializeField] private float graceTimeAtEndOfLevel;


    private void Awake()
    {
        Reference.levelManager = this;
    }
    void Start()
    {
        alarmSounded = false;
        secondBeforeNextLevel = graceTimeAtEndOfLevel;
    }

    // Update is called once per frame
    void Update()
    {
        //If all enemies are dead,
        if (Reference.allEnemies.Count < 1)
        {
            secondBeforeNextLevel -=Time.deltaTime;
            if (secondBeforeNextLevel <= 0)
            {
                SceneManager.LoadScene(nextLevel.name);
            }

        }
        else
        {
            secondBeforeNextLevel = graceTimeAtEndOfLevel;
        }
    }
}
