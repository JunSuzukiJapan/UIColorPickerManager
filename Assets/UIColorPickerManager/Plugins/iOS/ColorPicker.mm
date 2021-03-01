//
//  ColorPicker.m
//  ColorPicker
//
//  Created by jun suzuki on 2021/02/26.
//

#import <Foundation/Foundation.h>
#import "ColorPicker.h"

PickerViewController* __pickerViewControllerSharedInstance = NULL;

@interface PickerViewController ()

@end

@implementation PickerViewController

+ (PickerViewController*)sharedInstance {
    if(__pickerViewControllerSharedInstance == NULL){
        __pickerViewControllerSharedInstance = [[PickerViewController alloc] init];
    }
    
    return __pickerViewControllerSharedInstance;
}

- (void)colorPickerViewControllerDidSelectColor:(UIColorPickerViewController *)viewController  API_AVAILABLE(ios(14.0)){
    if(self.onColorSelected != NULL){
        UIColor* color = viewController.selectedColor;

        // UIColor 型の color から RGBA の値を取得します。
        CGFloat red;
        CGFloat green;
        CGFloat blue;
        CGFloat alpha;
        [color getRed:&red green:&green blue:&blue alpha:&alpha];

        (self.onColorSelected)(red, green, blue, alpha);
    }
}

- (void)colorPickerViewControllerDidFinish:(UIColorPickerViewController *)viewController  API_AVAILABLE(ios(14.0)){
    if(self.onFinish != NULL){
        (self.onFinish)();
    }
}

@end

//
// Plugin
//

extern UIViewController* UnityGetGLViewController();

void _CallColorPickerPlugin (
    float red,
    float green,
    float blue,
    float alpha,
    OnColorSelectedCallbackCaller onSelectedColorCallbackCaller,
    OnFinishCallbackCaller onFinishCallbackCaller,
    OnEarlierIOSVersions onEarlierIOSVersions)
{
    UIViewController* parent = UnityGetGLViewController();

    PickerViewController* vc = [PickerViewController sharedInstance];
    vc.onColorSelected = onSelectedColorCallbackCaller;
    vc.onFinish = onFinishCallbackCaller;
    
    if (@available(iOS 14.0, *)) {
        UIColorPickerViewController* pickerController = [[UIColorPickerViewController alloc] init];
        pickerController.delegate = vc;
        [parent presentViewController:pickerController animated:TRUE completion:nil];

    } else {
        // Fallback on earlier versions
        if(onEarlierIOSVersions != NULL){
            (onEarlierIOSVersions)();
        }
        return;
    }
}
