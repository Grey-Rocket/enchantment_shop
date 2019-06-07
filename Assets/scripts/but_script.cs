using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class but_script : MonoBehaviour
{
    public Button exit_but, enchant, make, cancel;
    public GameObject make_obj, cancel_obj, wep_tex, arm_tex;

    public Text mouth;

    void Start()
    {
        exit_but.onClick.AddListener(exClick);
        enchant.onClick.AddListener(enchanting);
        make.onClick.AddListener(making);
        cancel.onClick.AddListener(back);
    }

    void back() {

        this.GetComponent<Connector>().clear();

        //hides buttons and text
        make_obj.SetActive(false);
        cancel_obj.SetActive(false);
        wep_tex.SetActive(false);
        arm_tex.SetActive(false);

        this.GetComponent<Connector>().is_enchanting = false;

    }

    void making() {
        string[] input = this.GetComponent<Connector>().make_selected();

        string wep = "";
        string arm = "";

        if (input == null)
        {
            mouth.text = "You have to chose both elements";
        }
        else
        {
            if (input[0].Equals("blood") || input[1].Equals("blood") || input[0].Equals("flesh") || input[1].Equals("flesh") || input[1].Equals("poison"))
            {
                mouth.text = "You have been found guilty of murder and hanged.";
                return;
            }

            if (input[0].Equals("acid"))
            {
                wep = "You have melted your weapon";
            }
            else if (input[0].Equals("bone") || input[0].Equals("gold") || input[0].Equals("stone"))
            {
                wep = "You have made a good weapon";
            }
            else if (input[0].Equals("explosion"))
            {
                wep = "You have destroyed your weapon";
            }
            else if (input[0].Equals("fire"))
            {
                wep = "Your weapon is too hot";
            }
            else if (input[0].Equals("darkness") || input[0].Equals("light") || input[0].Equals("ectoplasma") || input[0].Equals("steam"))
            {
                wep = "Your weapon disappeared";
            }
            else if (input[0].Equals("ice"))
            {
                wep = "Your weapon is too cold";
            }
            else if (input[0].Equals("wood"))
            {
                wep = "You have made a practice weapon";
            }
            else if (input[0].Equals("earth"))
            {
                wep = "Your weapon fell appart";
            }
            else if (input[0].Equals("water"))
            {
                wep = "Your weapon didn't change";
            }
            else if (input[0].Equals("poison")) {
                wep = "You have made a deadly weapon";
            }
            else if (input[0].Equals("dark-magic") || input[0].Equals("weird-magic")) {
                wep = "Your weapon is incomprehensive";
            }
            else if (input[0].Equals("life-magic")) {
                wep = "Your weapon cannot kill anything";
            }

            //armor
            if (input[1].Equals("acid")) {
                arm = "you have melted your armor.";
            }
            else if (input[1].Equals("bone") || input[1].Equals("earth") || input[1].Equals("stone") || input[1].Equals("wood") || input[1].Equals("gold")) {
                arm = "your armor is solid.";
            }
            else if (input[1].Equals("explosion")) {
                arm = "you have destroyed your armor.";
            }
            else if (input[1].Equals("fire")) {
                arm = "your armor is too hot.";
            }
            else if (input[1].Equals("darkness") || input[1].Equals("light") || input[1].Equals("ectoplasma") || input[1].Equals("steam")) {
                arm = "your armor disappeared.";
            }
            else if (input[1].Equals("ice")) {
                arm = "your armor is too cold.";
            }
            else if (input[1].Equals("water")) {
                arm = "your armor didn't change.";
            }
            else if (input[1].Equals("life-magic")) {
                arm = "your armor regenerates health.";
            }
            else if (input[1].Equals("weird-magic") || input[1].Equals("dark-magic")) {
                arm = "your armor is incomprehensive.";
            }

            mouth.text = wep + " and " + arm;
        }
    }

    void enchanting() {
        //shows buttons and text
        make_obj.SetActive(true);
        cancel_obj.SetActive(true);
        wep_tex.SetActive(true);
        arm_tex.SetActive(true);

        this.GetComponent<Connector>().is_enchanting = true;
    }

    void exClick()
    {
        Application.Quit();
    }

}
