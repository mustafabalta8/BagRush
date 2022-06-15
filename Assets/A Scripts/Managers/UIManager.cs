using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Screens")]
    [SerializeField] private GameObject mainScreen;
    [SerializeField] private GameObject inGameScreen;
    [SerializeField] private GameObject successScreen;
    [SerializeField] private GameObject failScreen;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI inGameMoneyText;

    [SerializeField] private GameObject greenMoney, redMoney;
    
    private void Start()
    {
        Singelton();
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

    public void StartGame()
    {
        PlayerMovement.instance.transform.position = Vector3.zero;
        PlayerMovement.GameState = Screens.InGame;
        inGameScreen.SetActive(true);
    }
    public void OpenMainScreen()
    {
        mainScreen.SetActive(true);       
    }
    public void OpenSuccessScreen()
    {
        //PlayerMovement.GameState = Screens.Success;
        successScreen.SetActive(true);
        inGameScreen.SetActive(false);
    }

    public void ShowMoneyChangeInGame(int newAmount, int changeAmount, Vector3 position)
    {
        inGameMoneyText.text = newAmount + "";
        
        if (changeAmount > 0)
        {
            Instantiate(greenMoney, position, greenMoney.transform.rotation);
        }
        else
        {
            Instantiate(redMoney, position, greenMoney.transform.rotation);
        }
        
    }
    

}
