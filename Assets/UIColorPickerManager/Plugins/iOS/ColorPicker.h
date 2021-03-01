//
//  ColorPicker.h
//  ColorPicker
//
//  Created by jun suzuki on 2021/02/26.
//

#ifndef ColorPicker_h
#define ColorPicker_h

#import <UIKit/UIKit.h>


extern "C" {
    typedef void (*OnColorSelectedCallbackCaller)(float r, float g, float b, float a);
    typedef void (*OnFinishCallbackCaller)();
    typedef void (*OnEarlierIOSVersions)();

    void _CallColorPickerPlugin (
        float red,
        float green,
        float blue,
        float alpha,
        OnColorSelectedCallbackCaller selectedColorCallbackCaller,
        OnFinishCallbackCaller onFinishCallbackCaller,
        OnEarlierIOSVersions onEarlierIOSVersions);
}

@interface PickerViewController : NSObject <UIColorPickerViewControllerDelegate>

@property (nonatomic) OnColorSelectedCallbackCaller onColorSelected;
@property (nonatomic) OnFinishCallbackCaller onFinish;

+ (PickerViewController*)sharedInstance;

@end


#endif /* ColorPicker_h */
