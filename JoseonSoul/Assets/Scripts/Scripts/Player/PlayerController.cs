using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player State")]
    [SerializeField] private int playerState = 0;
    [Header("Player Managers")]
    public PlayerHealthManager playerHealthManager;

    public static GameObject Instance { get; private set; }

    private Animator animator;
    // Start is called before the first frame update

    void Awake()
    {
        if (Instance == null)
        {
            Instance = gameObject;
            DontDestroyOnLoad(gameObject);  // 씬 전환 시에도 객체 유지
        }
        else
        {
            Destroy(gameObject);  // 이미 인스턴스가 있으면 새로 생성된 오브젝트 삭제
            return;
        }
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        if(animator == null)
            Debug.LogError("Animator Not Detected");
        
        playerHealthManager = GetComponent<PlayerHealthManager>();
        if(playerHealthManager == null)
            Debug.LogError("Health Manager Not Detected");

        Instance.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SyncAnimationState(){
        animator.SetInteger("player_state", playerState);
    }

    public void SetPlayerState(int state)
    {
        playerState = state;
        SyncAnimationState();
    }

    public int GetPlayerState()
    {
        return playerState;
    }

    public void InitPlayer(float currnetHP, int potionRemained, Vector3 position)
    {
        transform.position = position;
        playerHealthManager.updateCurrentHP(currnetHP,true);
        //playerPotionmanager.setPotionRemained(potionRemained);
    }
}
