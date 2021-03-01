using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPickerManagerSample : MonoBehaviour {
    [SerializeField] private GameObject obj;
    [SerializeField] private Image img;

	public void Show(){
		if(obj != null){
            Color color = Color.white;
            var renderer = obj.GetComponent<Renderer>();
            if(renderer != null){
                color = renderer.material.color;
            }

			// UIColorPickerManager.Show(color, OnSelectColor, OnFinish, OnEarlierIOSVersions);
			UIColorPickerManager.Show(color, OnSelectColor, OnFinish);
            // or
			// UIColorPickerManager.Show(color, OnSelectRGBColor, OnFinish, OnEarlierIOSVersions);
            // or
			// UIColorPickerManager.Show(color, OnSelectRGBColor, OnFinish);
		}
	}

    void OnSelectRGBColor(float red, float green, float blue, float alpha){
        // Debug.LogFormat("OnSelectColor. red: {0}, green: {1}, blue: {2}, alpha: {3}", red, green, blue, alpha);

        if(obj != null){
            var renderer = obj.GetComponent<Renderer>();
            if(renderer != null){
                var color = new Color(red, green, blue, alpha);
                renderer.material.color = color;
            }
        }

        if(img != null){
            img.color = new Color(red, green, blue, alpha);
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

    void OnEarlierIOSVersions(){
        // Fallback on earlier iOS versions
    }
}
