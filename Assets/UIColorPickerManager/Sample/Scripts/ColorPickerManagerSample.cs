using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPickerManagerSample : MonoBehaviour {
    [SerializeField] private GameObject obj;
    [SerializeField] private Image img;

	public void Show(){
		if(obj != null){
            Color currentColor = Color.white;
            var renderer = obj.GetComponent<Renderer>();
            if(renderer != null){
                currentColor = renderer.material.color;
            }

			UIColorPickerManager.Show(currentColor, OnSelectColor, OnFinish);
            // or
			// UIColorPickerManager.Show(currentColor, OnSelectColor, OnFinish, OnEarlierIOSVersions);
            // or
			// UIColorPickerManager.Show(currentColor, OnSelectRGBColor, OnFinish);
            // or
			// UIColorPickerManager.Show(currentColor, OnSelectRGBColor, OnFinish, OnEarlierIOSVersions);
		}
	}

    void OnSelectColor(Color color){
        if(obj != null){
            var renderer = obj.GetComponent<Renderer>();
            if(renderer != null){
                renderer.material.color = color;
            }
        }

        if(img != null){
            img.color = color;
        }
    }

    void OnFinish(){

    }

    /*
    void OnSelectRGBColor(float red, float green, float blue, float alpha){
        // Debug.LogFormat("OnSelectColor. red: {0}, green: {1}, blue: {2}, alpha: {3}", red, green, blue, alpha);

        if(obj != null){
            var renderer = obj.GetComponent<Renderer>();
            if(renderer != null){
                var currentColor = new Color(red, green, blue, alpha);
                renderer.material.color = currentColor;
            }
        }

        if(img != null){
            img.color = new Color(red, green, blue, alpha);
        }
    }
    */

    /*
    void OnEarlierIOSVersions(){
        // Fallback on earlier iOS versions
    }
    */
}
