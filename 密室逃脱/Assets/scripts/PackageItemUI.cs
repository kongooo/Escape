using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackageItemUI : MonoBehaviour {

    public Image PackItemImage;

    private void Awake()
    {
        PackItemImage = gameObject. GetComponent<Image>();
    }

    public void updateImage(Sprite sprite)
    {
        PackItemImage.sprite = sprite;
    }

    
}
