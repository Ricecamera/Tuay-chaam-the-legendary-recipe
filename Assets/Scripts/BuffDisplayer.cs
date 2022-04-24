using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffDisplayer : MonoBehaviour
{
    public int maxRow = 2;
    public int maxCol = 5;
    
    public Image[] images;

    public void UpdateBuffImage(List<Sprite> buffSprite) {
        int i = 0;         // Set initial index

        // Update buff sprites
        for (; i < buffSprite.Count && i < images.Length; i++) {
            images[i].sprite = buffSprite[i];
        }

        // Cleanup the rest which is not assign sprite
        for (; i < images.Length; i++) {
            images[i].sprite = null;
        }
    }
}
