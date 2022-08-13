using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public void Awake()
    {
        instance = this;
    }

    [SerializeField] public GameObject pickups;
    [SerializeField] public GameObject destructibleTerrain;
    [SerializeField] public OnScreenMessageSystem onScreenMessageSystem;
    [SerializeField] public MainHealthBar mainHealthBar;
    [SerializeField] public Cinemachine.CinemachineVirtualCamera vcam;    
    [SerializeField] public CooldownIcon dashCooldownIcon;
    [SerializeField] public SingleDigitScorePanel singleDigitScorePanel;
    [SerializeField] public MultiDigitScorePanel MultiDigitScorePanel;
    [SerializeField] public List<GameObject> players;

}
