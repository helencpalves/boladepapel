using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{

    public bool in_game = true;
    public bool win = false;
    private int score = 0;
    public int attempts = 15;
    private int initial_attempts;
    private int initial_stress;
    private int stress = 100;
    private int diff;
    private int level = 0;
    private int rangeini = 50;
    public TextMeshProUGUI scoretxt;
    public TextMeshProUGUI ballstxt;
    public TextMeshProUGUI stresstxt;
    public TextMeshProUGUI hitstxt;
    public TextMeshProUGUI endtxt;
    public TextMeshProUGUI finalmessagetxt;
    public TextMeshProUGUI leveltxt;
    public Vector3[] posicoes;
    public Transform trashcan;
    // Start is called before the first frame update
    void Start()
    {
        in_game = true;
        win = false;
        score = 0;
        level += 1;
        initial_attempts = attempts;
        stress = Random.Range(rangeini, rangeini+20);
        initial_stress = stress;
        trashcan.position = posicoes[Random.Range(0, posicoes.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        if (attempts <= 0 || stress >= 100){
            scoretxt.gameObject.SetActive(true);
            endtxt.gameObject.SetActive(true);
            finalmessagetxt.gameObject.SetActive(true);
            scoretxt.text = "Score: "+score;
            if (stress <= 50){
                win = true;
                endtxt.text = "WIN!";
                finalmessagetxt.text = "Congratz! Your stress decreased "+
                                        (initial_stress-stress)+
                                        "%\nPress \"Fire1\" to go to next level";
            } else {
                diff = initial_stress - stress;
                endtxt.text = "Game Over!";
                if (diff >= 0)
                    finalmessagetxt.text = "Your stress decreased "+
                                            diff+"%\n"+
                                            "but it's still over 50%\n"+
                                            "Press \"Fire1\" to restart game";
                else
                    finalmessagetxt.text = "Damn! Your stress increased " +
                                            (-diff) + "%\n" +
                                            "Press \"Fire1\" to restart game";
                if (stress >= 100)
                    finalmessagetxt.text = "Your stress has reached 100%\n"+
                                           "You had a heart attack and died!\n"+
                                           "Press \"Fire1\" to restart game";
            }
            in_game = false;
        } else {
            scoretxt.gameObject.SetActive(false);
            endtxt.gameObject.SetActive(false);
            finalmessagetxt.gameObject.SetActive(false);
        }

        ballstxt.text = "Remaining Balls: " + attempts;
        stresstxt.text = "Stress: "+stress+"%";
        hitstxt.text = "Hits: "+score+"/"+(initial_attempts-attempts);
        leveltxt.text = "Level "+level;
    }

    public void hit(){
        score += 1;
        attempts -= 1;
        stress -= 5;
        trashcan.position = posicoes[Random.Range(0, posicoes.Length)];
    }

    public void missed(){
        attempts -= 1;
        stress += 2;
    }

    public void restart(){
        attempts = initial_attempts;
        level = 0;
        rangeini = 50;
        Start();
    }

    public void nextlevel(){
        print("nextlevel");
        attempts = initial_attempts;
        rangeini += 10;
        Start();
    }
}
