using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class UIColorPickerManager {
    private static IntPtr gcHandleForRGBColorSelectedCallback = IntPtr.Zero;
    private static IntPtr gcHandleForColorSelectedCallback = IntPtr.Zero;
    private static IntPtr gcHandleForOnFinishCallback = IntPtr.Zero;
    private static IntPtr gcHandleForOnEarlierIOSVersionsCallback = IntPtr.Zero;

	// - colorPickerViewControllerDidSelectColor: 用コールバック
	public delegate void OnRGBColorSelectedCallback (float r, float g, float b, float a);
	public delegate void OnRGBColorSelectedCallbackCaller(float r, float g, float b, float a);
    public delegate void OnColorSelectedCallback(Color color);
    public delegate void OnColorSelectedCallbackCaller(float r, float g, float b, float a);

    // - colorPickerViewControllerDidFinish: 用コールバック
    public delegate void OnFinishCallback();
    public delegate void OnFinishCallbackCaller();

    // @available(iOS 14.0, *) がfalseの時用コールバック
    public delegate void OnEarlierIOSVersionsCallback();
    public delegate void OnEarlierIOSVersionsCallbackCaller();

	[DllImport("__Internal")]
	private static extern void _CallColorPickerPlugin (
        float red,
        float green,
        float blue,
        float alpha,
        OnRGBColorSelectedCallbackCaller onRGBColorSelectedColorCallbackCaller,
        OnFinishCallbackCaller onFinishCallbackCaller,
        OnEarlierIOSVersionsCallbackCaller onEarlierIOSVersionsCallbackCaller);

	public static void Show (
        Color color,
        OnRGBColorSelectedCallback onRGBColorSelectedCallback,
        OnFinishCallback onFinishCallback,
        OnEarlierIOSVersionsCallback onEarlierIOSVersionsCallback)
    {
        // if(gcHandleForRGBColorSelectedCallback != IntPtr.Zero ||
        //    gcHandleForColorSelectedCallback != IntPtr.Zero ||
        //    gcHandleForOnFinishCallback != IntPtr.Zero)
        // {
        //     return;
        // }
        CleanUpHandles();

		// コールバック関数をGCされないようにAllocしてハンドルを取得する。
		gcHandleForRGBColorSelectedCallback = (IntPtr)GCHandle.Alloc(onRGBColorSelectedCallback, GCHandleType.Normal);
		gcHandleForOnFinishCallback = (IntPtr)GCHandle.Alloc(onFinishCallback, GCHandleType.Normal);
        gcHandleForOnEarlierIOSVersionsCallback = (IntPtr)GCHandle.Alloc(onEarlierIOSVersionsCallback, GCHandleType.Normal);

		// 普通の引数 + コールバック関数のハンドル + コールバック関数を呼び出すためのstaticなメソッド
		_CallColorPickerPlugin (
            color.r,
            color.g,
            color.b,
            color.a,
            CallRGBColorSelectedCallback,
            CallOnFinishCallback,
            CallOnEarlierIOSVersionsCallback);
	}

	public static void Show (
        Color color,
        OnRGBColorSelectedCallback onRGBColorSelectedCallback,
        OnFinishCallback onFinishCallback)
    {
        // if(gcHandleForRGBColorSelectedCallback != IntPtr.Zero ||
        //    gcHandleForColorSelectedCallback != IntPtr.Zero ||
        //    gcHandleForOnFinishCallback != IntPtr.Zero)
        // {
        //     return;
        // }
        CleanUpHandles();

        OnEarlierIOSVersionsCallback onEarlierIOSVersionsCallback = DoNothing;

		// コールバック関数をGCされないようにAllocしてハンドルを取得する。
		gcHandleForRGBColorSelectedCallback = (IntPtr)GCHandle.Alloc(onRGBColorSelectedCallback, GCHandleType.Normal);
		gcHandleForOnFinishCallback = (IntPtr)GCHandle.Alloc(onFinishCallback, GCHandleType.Normal);
        gcHandleForOnEarlierIOSVersionsCallback = (IntPtr)GCHandle.Alloc(onEarlierIOSVersionsCallback, GCHandleType.Normal);

		// 普通の引数 + コールバック関数のハンドル + コールバック関数を呼び出すためのstaticなメソッド
		_CallColorPickerPlugin (
            color.r,
            color.g,
            color.b,
            color.a,
            CallRGBColorSelectedCallback,
            CallOnFinishCallback,
            CallOnEarlierIOSVersionsCallback);
	}

	public static void Show (
        Color color,
        OnColorSelectedCallback onColorSelectedCallback,
        OnFinishCallback onFinishCallback,
        OnEarlierIOSVersionsCallback onEarlierIOSVersionsCallback)
    {
        // if(gcHandleForRGBColorSelectedCallback != IntPtr.Zero ||
        //    gcHandleForColorSelectedCallback != IntPtr.Zero ||
        //    gcHandleForOnFinishCallback != IntPtr.Zero)
        // {
        //     return;
        // }
        CleanUpHandles();

		// コールバック関数をGCされないようにAllocしてハンドルを取得する。
		gcHandleForColorSelectedCallback = (IntPtr)GCHandle.Alloc(onColorSelectedCallback, GCHandleType.Normal);
		gcHandleForOnFinishCallback = (IntPtr)GCHandle.Alloc(onFinishCallback, GCHandleType.Normal);
        gcHandleForOnEarlierIOSVersionsCallback = (IntPtr)GCHandle.Alloc(onEarlierIOSVersionsCallback, GCHandleType.Normal);

		// 普通の引数 + コールバック関数のハンドル + コールバック関数を呼び出すためのstaticなメソッド
		_CallColorPickerPlugin (
            color.r,
            color.g,
            color.b,
            color.a,
            CallColorSelectedCallback,
            CallOnFinishCallback,
            CallOnEarlierIOSVersionsCallback);
	}

	public static void Show (
        Color color,
        OnColorSelectedCallback onColorSelectedCallback,
        OnFinishCallback onFinishCallback)
    {
        // if(gcHandleForRGBColorSelectedCallback != IntPtr.Zero ||
        //    gcHandleForColorSelectedCallback != IntPtr.Zero ||
        //    gcHandleForOnFinishCallback != IntPtr.Zero)
        // {
        //     return;
        // }
        CleanUpHandles();

        OnEarlierIOSVersionsCallback onEarlierIOSVersionsCallback = DoNothing;

		// コールバック関数をGCされないようにAllocしてハンドルを取得する。
		gcHandleForColorSelectedCallback = (IntPtr)GCHandle.Alloc(onColorSelectedCallback, GCHandleType.Normal);
		gcHandleForOnFinishCallback = (IntPtr)GCHandle.Alloc(onFinishCallback, GCHandleType.Normal);
        gcHandleForOnEarlierIOSVersionsCallback = (IntPtr)GCHandle.Alloc(onEarlierIOSVersionsCallback, GCHandleType.Normal);

		// 普通の引数 + コールバック関数のハンドル + コールバック関数を呼び出すためのstaticなメソッド
		_CallColorPickerPlugin (
            color.r,
            color.g,
            color.b,
            color.a,
            CallColorSelectedCallback,
            CallOnFinishCallback,
            CallOnEarlierIOSVersionsCallback);
	}

    static void DoNothing(){
        // do nothing
    }

	[AOT.MonoPInvokeCallbackAttribute(typeof(OnRGBColorSelectedCallbackCaller))]
	static void CallRGBColorSelectedCallback (float r, float g, float b, float a){
		GCHandle handle = (GCHandle)gcHandleForRGBColorSelectedCallback;
		OnRGBColorSelectedCallback callback = handle.Target as OnRGBColorSelectedCallback;
		callback(r, g, b, a);
	}

	[AOT.MonoPInvokeCallbackAttribute(typeof(OnColorSelectedCallbackCaller))]
    static void CallColorSelectedCallback(float red, float green, float blue, float alpha){
		GCHandle handle = (GCHandle)gcHandleForColorSelectedCallback;
		OnColorSelectedCallback callback = handle.Target as OnColorSelectedCallback;

        var color = new Color(red, green, blue, alpha);
		callback(color);
	}

	[AOT.MonoPInvokeCallbackAttribute(typeof(OnFinishCallbackCaller))]
	static void CallOnFinishCallback (){
		GCHandle handle = (GCHandle)gcHandleForOnFinishCallback;
		OnFinishCallback callback = handle.Target as OnFinishCallback;

		// 不要になったハンドルを解放する。
        // CleanUpHandles();

		callback();
	}

	[AOT.MonoPInvokeCallbackAttribute(typeof(OnEarlierIOSVersionsCallbackCaller))]
    static void CallOnEarlierIOSVersionsCallback(){
        GCHandle handle = (GCHandle)gcHandleForOnEarlierIOSVersionsCallback;
        OnEarlierIOSVersionsCallback callback = handle.Target as OnEarlierIOSVersionsCallback;

		// 不要になったハンドルを解放する。
        // CleanUpHandles();

        callback();
    }

    // 不要になったハンドルを解放する。
    static void CleanUpHandles(){
        if(gcHandleForRGBColorSelectedCallback != IntPtr.Zero){
            ((GCHandle)gcHandleForRGBColorSelectedCallback).Free();
        }
        if(gcHandleForColorSelectedCallback != IntPtr.Zero){
            ((GCHandle)gcHandleForColorSelectedCallback).Free();
        }
        if(gcHandleForOnFinishCallback != IntPtr.Zero){
            ((GCHandle)gcHandleForOnFinishCallback).Free();
        }
        if(gcHandleForOnEarlierIOSVersionsCallback != IntPtr.Zero){
            ((GCHandle)gcHandleForOnEarlierIOSVersionsCallback).Free();
        }

        gcHandleForRGBColorSelectedCallback = IntPtr.Zero;
        gcHandleForColorSelectedCallback = IntPtr.Zero;
        gcHandleForOnFinishCallback = IntPtr.Zero;
        gcHandleForOnEarlierIOSVersionsCallback = IntPtr.Zero;
    }
}