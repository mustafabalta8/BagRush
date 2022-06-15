using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    private const string levelIndex = "level";
    private static int levelInfo=0;

    [SerializeField] private GameObject interactiveObjects;
    [Header("Levels Array")]
    [SerializeField] private Level[] levels;

    [SerializeField] private TextMeshProUGUI levelText;
    
    public static int LevelInfo { get => levelInfo; set => levelInfo = value; }

    private void Start()
    {
        Singelton();
        PlayerPrefs.SetInt(levelIndex, 0);
        LevelInfo = 0;//PlayerPrefs.GetInt(levelIndex);
        CreateLevel(LevelInfo);//LevelInfo
    }
    private void Singelton()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void UpdateLevel()//call when finish line passed
    {
        LevelInfo++;
        if (levelInfo == 6) { levelInfo = 0; }
        int nextLevel = LevelInfo;
        //PlayerPrefs.SetInt(levelIndex, nextLevel);// close for test   
    }
    public void CreateNextLevel()//call when finish line passed
    {
        ObjectPooler.instance.DeactivateAllPools();
        CreateLevel(LevelInfo);
        //PlayerMovement.GameState = Screens.InGame;
        
    }

    public void CreateLevel(int levelIndex)
    {
        levelText.text = "Level " + (levelInfo + 1);
        for (int i= 0;i< levels[levelIndex].collectibleBagPositions.Length;i++)
        {
            GameObject newCake = ObjectPooler.instance.GetPooledObject(GameObjects.Bag);
            
            newCake.transform.position = levels[levelIndex].collectibleBagPositions[i];
            newCake.SetActive(true);
        }

        //interactiveObjects part
        Destroy(interactiveObjects);
        interactiveObjects = Instantiate(levels[levelIndex].interactiveObjectsSet);      
    }

}
