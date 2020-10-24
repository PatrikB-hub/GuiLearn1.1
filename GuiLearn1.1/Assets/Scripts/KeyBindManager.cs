using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class KeyBindManager : MonoBehaviour
{
    [SerializeField]
    public static Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();

    [System.Serializable] // allows us to see our struct when we use it in an array
    public struct KeyUISetup
    {
        public string keyName;
        public Text keyDisplayText;
        public string defaultKey;
    }
    // array of our Key Names, UI text for the Key and Default Key
    public KeyUISetup[] baseSetup;
    public GameObject currentKey;
    public Color32 changedKey = new Color32(39, 171, 249, 255);
    public Color32 selectedKey = new Color32(239, 116, 36, 255);


    void Start()
    {
        //for loop to add the keys to the dictionary with Save or Default depending on load
        for (int i = 0; i < baseSetup.Length; i++) //for all the keys in baseSetup array
        {
            //add key according to the saved string or default
            keys.Add(baseSetup[i].keyName, (KeyCode)System.Enum.Parse(typeof
                (KeyCode), PlayerPrefs.GetString(baseSetup[i].keyName, baseSetup[i].defaultKey)));

            // for all the UI Text change the display to what the bind is
            baseSetup[i].keyDisplayText.text = keys[baseSetup[i].keyName].ToString();

        }
    }

    public void SaveKeys()
    {
        foreach(var key in keys)
        {
            PlayerPrefs.SetString(key.Key, key.Value.ToString());
        }
        PlayerPrefs.Save();
    }

    public void ChangeKey(GameObject clickedKey)
    {
        currentKey = clickedKey;
        if (clickedKey != null) // if we have clicked a key and its selected
        {
            currentKey.GetComponent<Image>().color = selectedKey; // this is just visual to know
            //its clicked...like a debug
        }
    }

    private void OnGUI()// will allow us to run events
    {
        string newKey = "";
        Event e = Event.current;
        if (currentKey != null)//this will fix issues later, also the code will only run when needed
        {
            if (e.isKey)
            {
                newKey = e.keyCode.ToString();
            }
            // there is an issue getting the shift keys the following will fix that
            if (Input.GetKey(KeyCode.LeftShift))
            {
                newKey = "Leftshift";
            }
            if (Input.GetKey(KeyCode.RightShift))
            {
                newKey = "Rightshift";
            }
            if (newKey != "")// if we have set a key
            {
                keys[currentKey.name] = (KeyCode)System.Enum.Parse(typeof(KeyCode), newKey);
                // the above code changes out Key in the dictionary to the key we just pressed
                currentKey.GetComponentInChildren<Text>().text = newKey;
                //that changes the display text to match the new key
                currentKey.GetComponent<Image>().color = changedKey;//to show we changed it...debug
                currentKey = null;//reset and wait

            }
        }
    }
}
