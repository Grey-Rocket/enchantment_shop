using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Connector : MonoBehaviour
{
    public GameObject[] rightSide;
    public GameObject[] rightText;

    public GameObject[] leftSide;
    public GameObject[] leftText;

    GameObject right = null;
    GameObject left = null;

    public Text counter;

    int numFound = 2;

    public bool is_enchanting = false;

    void Update()
    {
        //here it checks if the left mouse button was pressed
        if (Input.GetMouseButtonDown(0)) {
            //here it gets screen position of the mouse 
            Vector3 v3 = Input.mousePosition;

            //here it gets the world position of the mouse
            v3 = Camera.main.ScreenToWorldPoint(v3);

            setObject(v3);

            if (!is_enchanting && right != null && left != null) {
                combine();
                counter.text = numFound + "/20";
            }
            

        }
    }

    private void setObject(Vector3 v3) {

        //here it goes through all the elements
        for (int i = 0; i < rightSide.Length; i++) {
            if (rightSide[i].activeSelf) {

                if (isInside(rightSide[i].GetComponent<Transform>().position, v3))
                {
                    setSlots(rightSide[i], true);
                }
                else if (isInside(leftSide[i].GetComponent<Transform>().position, v3))
                {
                    setSlots(leftSide[i], false);

                }
            }
        }

    }

    //here it checks if the player has clicked on the object
    private bool isInside(Vector3 elem, Vector3 mouse) {

        //this is needed, because position is only one point
        if (mouse.x < elem.x + 0.5f && elem.x - 0.5f < mouse.x) {
            if (mouse.y < elem.y + 0.5f && elem.y - 0.5f < mouse.y)
            {
                return true;
            }
        }

        return false;
    }

    //this sets which element is selected
    private void setSlots(GameObject element, bool isRight) {
        if (isRight)
        {
            //if the right slot is still empty, set this element as selected
            if (right == null)
            {
                right = element;
                GameObject child = element.transform.GetChild(2).gameObject;
                child.SetActive(true);
            }
            //if the current element is already selected, disable it
            else if (right == element)
            {
                right = null;
                GameObject child = element.transform.GetChild(2).gameObject;
                child.SetActive(false);
            }
            //if some other element is selected deselect it and select this one
            else {
                GameObject child = right.transform.GetChild(2).gameObject;
                child.SetActive(false);

                right = element;
                child = element.transform.GetChild(2).gameObject;
                child.SetActive(true);
            }
        }
        else {
            //if the right slot is still empty, set this element as selected
            if (left == null)
            {
                left = element;
                GameObject child = element.transform.GetChild(2).gameObject;
                child.SetActive(true);
            }
            //if the current element is already selected, disable it
            else if (left == element)
            {
                left = null;
                GameObject child = element.transform.GetChild(2).gameObject;
                child.SetActive(false);
            }
            //if some other element is selected deselect it and select this one
            else
            {
                GameObject child = left.transform.GetChild(2).gameObject;
                child.SetActive(false);

                left = element;
                child = element.transform.GetChild(2).gameObject;
                child.SetActive(true);
            }
        }
    }

    //this tries to combine the two things
    private void combine() {

        bool someNew = false;

        GameObject[] leftElemCombs = left.GetComponent<Element>().combinesWith;
        GameObject[] leftElemCreates = left.GetComponent<Element>().creates;

        for (int i = 0; i < leftElemCombs.Length; i++) {
            //if they combine 
            if (leftElemCombs[i].tag == right.tag)
            {
                //checks if the object was already created
                for (int k = 0; k < rightSide.Length; k++) {
                    if (rightSide[k].tag == leftElemCreates[i].tag) {

                        if (!rightSide[k].activeSelf) {
                            someNew = true;

                            //comes closer to final
                            right.GetComponent<Element>().increase();
                            left.GetComponent<Element>().increase();
                            if (right.tag != left.tag) {
                                for (int z = 0; z < leftSide.Length; z++) {
                                    if (right.tag == leftSide[z].tag) {
                                        leftSide[z].GetComponent<Element>().increase();
                                    }

                                    if (left.tag == rightSide[z].tag) {
                                        rightSide[z].GetComponent<Element>().increase();
                                    }

                                }
                            }

                            //activate them
                            rightSide[k].SetActive(true);
                            rightText[k].SetActive(true);

                            leftSide[k].SetActive(true);
                            leftText[k].SetActive(true);

                            //increase counter
                            numFound++;
                        }

                        break;
                    }
                }
            }
        }

        //clears chosen two items
        left.transform.GetChild(2).gameObject.SetActive(false);
        right.transform.GetChild(2).gameObject.SetActive(false);

        if (!someNew) {

            StartCoroutine(incorrect(left, right));
            
        }

        left = null;
        right = null;
    }

    IEnumerator incorrect(GameObject lef, GameObject rig) {
        lef.transform.GetChild(4).gameObject.SetActive(true);
        rig.transform.GetChild(4).gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        lef.transform.GetChild(4).gameObject.SetActive(false);
        rig.transform.GetChild(4).gameObject.SetActive(false);

    }

    public void clear() {
        if (right != null)
        {
            right.transform.GetChild(2).gameObject.SetActive(false);
            right = null;
        }

        if (left != null)
        {
            left.transform.GetChild(2).gameObject.SetActive(false);
            left = null;
        }
    }

    //this tells what option was chosen
    public string[] make_selected() {

        //result[0] is weapon, result[1] is armor
        string[] result = new string[2];

        if (right != null && left != null)
        {
            result[0] = left.tag;
            result[1] = right.tag;

            clear();

            return result;
        }
        else {

            clear();

            return null;
        }

    }
}
