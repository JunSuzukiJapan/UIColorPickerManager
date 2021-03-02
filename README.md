# UIColorPickerManager

Unity Plugin to use UIColorPickerViewController.

# Requirement

iOS14 or higher.

# Sample

```csharp:
public void Show(){
    Color currentColor = Color.white;
    UIColorPickerManager.Show(currentColor, OnSelectColor, OnFinish);
}

// call when color selected.
void OnSelectColor(Color selectedColor){
    // ...
}

// call UIColorPickerViewController finished.
void OnFinish(){

}
```

# LICENSE

[MIT](LICENSE)
