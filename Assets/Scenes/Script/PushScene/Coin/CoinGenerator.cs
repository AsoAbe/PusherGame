using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    public CoinController coinController;
    int GoalCoin;
    public int AddCount = 5;
    public int AddCoin = 5;
    public float RandomPos_x = 1;

    public AudioSource audioSource;
    public AudioClip SE;

    public Transform AddCoinPosition;

   void Start()
   {
        GoalCoin = coinController.CoinCount + AddCount;
   }

   void Update()
   {
        if (coinController.CoinCount >= GoalCoin) 
        {

            for (int i = 0; i < AddCoin; i++)
            {
                var pos = AddCoinPosition.position;
                pos.x += Random.Range(-RandomPos_x, RandomPos_x);
                AddCoinPosition.position = pos;
               
                // ƒ‰ƒ“ƒ_ƒ€‚É‘I‘ð
                int randomIndex =
                    Random.Range(0, coinController.Coin.Length);

                Instantiate(coinController.Coin[randomIndex],
                    AddCoinPosition.position, Quaternion.identity);
                
                audioSource.PlayOneShot(SE);
            }
            GoalCoin += AddCount;
        }
   }
}
