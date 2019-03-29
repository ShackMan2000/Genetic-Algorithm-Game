using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeLocker : MonoBehaviour
{

    [SerializeField]
    private Sprite upgradeBack;



    [ContextMenu("Unlock")]
    public void Unlock(int levelNeeded)
    {
    Image image = GetComponent<Image>();

       if (SaveManager.Instance.saveData.levelsUnlocked >= levelNeeded)
       
        {
            image.color = new Color(1,1,1,1);
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
            image.sprite = upgradeBack;
        }
    }
}
