using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DyingText : MonoBehaviour
{
    [SerializeField]
    private Text dyingtext;

    [SerializeField]
    private Text GameOverText;

    [SerializeField]
    private Text GameWonText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;
        if(dyingtext && Player.zombieCount > 0){
            dyingtext.text = ((int)((Player.zombieDeathTime)+0.5)).ToString();
        }
        if(GameOverText && Player.gameOver == true){
            
            StartCoroutine(GameOver());
        }
        if(GameWonText && Player.gameWon == true){
            
            StartCoroutine(GameWon());
        }
        restart();
    }
    private void restart(){
        if(Input.GetButtonDown("Restart")){
            SceneManager.LoadScene("Game");
            Player.zombieCount = 0;
            Player.zombieDeathTime = 15;
            Player.gameOver = false;
            Player.gameWon = false;
        }
    }
    IEnumerator GameWon(){
        yield return new WaitForSeconds(1);
        Destroy(dyingtext);
        GameWonText.text = "You Won!";
    }
    IEnumerator GameOver(){
        yield return new WaitForSeconds(1);
        Destroy(dyingtext);
        GameOverText.text = "Game Over";
    }
}
