using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HumanController : MonoBehaviour
{
    public float range = 3f;
    public GameObject choiceUI;
    public Manusia manusia;
    ChoiceController choiceController;

    // Start is called before the first frame update
    void Start()
    {
        choiceController = choiceUI.GetComponent<ChoiceController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckItem() != null)
        {
            if (CheckItem().transform.parent == null && !choiceUI.activeInHierarchy)
            {
                Time.timeScale = 0;
                choiceUI.SetActive(true);
                Tuple<Manusia, string>[] jokesTuple = CheckItem().GetComponent<Item>().jokesTuple;
                Destroy(CheckItem().gameObject);
                choiceController.CheckJokes(jokesTuple, manusia);
            }
        }
    }

    private Collider CheckItem() {
        Collider[] cols = Physics.OverlapSphere(transform.position, range, LayerMask.GetMask("kertas"));
        if (cols.Length == 0)
        {
            return null;
        } else {
            return cols[0];
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
