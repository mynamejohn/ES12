using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertItem : MonoBehaviour
{
    public Item item;
    public GameObject AlertControl;
    public Image ItemImage;
    public Text ItemName;

    public string ItemName_String;
    public string ItemImage_String;

    public bool isScreen = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        return;
    }

    public void SetMember(string itemname, string itempath)
    {
        ItemName_String = itemname;
        ItemImage_String = itempath;
    }
    public void PopUp()
    {
        ItemName.text = ItemName_String;
        ItemImage.sprite = Resources.Load<Sprite>(ItemImage_String);
        isScreen = true;
        AlertControl.SetActive(true);
    }

    public void Popdown()
    {
        AlertControl.SetActive(false);
        ItemName.text = "";
        ItemImage.sprite = null;
        item.isSpecial = false;
        isScreen = false;
    }
} 
