using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class UIColorPickerManager {
    private static IntPtr gcHandleForSelectedCallback = IntPtr.Zero;
    private static IntPtr gcHandleForOnFinishCallback = IntPtr.Zero;
    private static IntPtr gcHandleForOnEarlierIOSVersionsCallback = IntPtr.Zero;

	// - colorPickerViewControllerDidSelectColor: 用コールバック
	public delegate void OnColorSelectedCallback (float r, float g, float b, float a);
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
        OnColorSelectedCallbackCaller selectedColorCallbackCaller,
        OnFinishCallbackCaller onFinishCallbackCaller,
        OnEarlierIOSVersionsCallbackCaller onEarlierIOSVersionsCallbackCaller);

	public static void Show (
        Color color,
        OnColorSelectedCallback colorSelectedCallback,
        OnFinishCallback onFinishCallback,
        OnEarlierIOSVersionsCallback onEarlierIOSVersionsCallback)
    {
        if(gcHandleForSelectedCallback != IntPtr.Zero || gcHandleForOnFinishCallback != IntPtr.Zero){
            return;
        }

		// コールバック関数をGCされないようにAllocしてハンドルを取得する。
		gcHandleForSelectedCallback = (IntPtr)GCHandle.Alloc(colorSelectedCallback, GCHandleType.Normal);
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
        OnColorSelectedCallback colorSelectedCallback,
        OnFinishCallback onFinishCallback)
    {
        if(gcHandleForSelectedCallback != IntPtr.Zero || gcHandleForOnFinishCallback != IntPtr.Zero){
            return;
        }

        OnEarlierIOSVersionsCallback earlierIOSVersionsCallback = DoNothing;

		// コールバック関数をGCされないようにAllocしてハンドルを取得する。
		gcHandleForSelectedCallback = (IntPtr)GCHandle.Alloc(colorSelectedCallback, GCHandleType.Normal);
		gcHandleForOnFinishCallback = (IntPtr)GCHandle.Alloc(onFinishCallback, GCHandleType.Normal);
        gcHandleForOnEarlierIOSVersionsCallback = (IntPtr)GCHandle.Alloc(earlierIOSVersionsCallback, GCHandleType.Normal);

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

	[AOT.MonoPInvokeCallbackAttribute(typeof(OnColorSelectedCallbackCaller))]
	static void CallColorSelectedCallback (float r, float g, float b, float a){
		GCHandle handle = (GCHandle)gcHandleForSelectedCallback;
		OnColorSelectedCallback callback = handle.Target as OnColorSelectedCallback;
		callback(r, g, b, a);
	}

	[AOT.MonoPInvokeCallbackAttribute(typeof(OnFinishCallbackCaller))]
	static void CallOnFinishCallback (){
		GCHandle handle = (GCHandle)gcHandleForOnFinishCallback;
		OnFinishCallback callback = handle.Target as OnFinishCallback;

		// 不要になったハンドルを解放する。
        CleanUpHandles();

		callback();
	}

	[AOT.MonoPInvokeCallbackAttribute(typeof(OnEarlierIOSVersionsCallbackCaller))]
    static void CallOnEarlierIOSVersionsCallback(){
        GCHandle handle = (GCHandle)gcHandleForOnEarlierIOSVersionsCallback;
        OnEarlierIOSVersionsCallback callback = handle.Target as OnEarlierIOSVersionsCallback;

		// 不要になったハンドルを解放する。
        CleanUpHandles();

        callback();
    }

    // 不要になったハンドルを解放する。
    static void CleanUpHandles(){
        ((GCHandle)gcHandleForSelectedCallback).Free();
        ((GCHandle)gcHandleForOnFinishCallback).Free();
        ((GCHandle)gcHandleForOnEarlierIOSVersionsCallback).Free();

        gcHandleForSelectedCallback = IntPtr.Zero;
        gcHandleForOnFinishCallback = IntPtr.Zero;
        gcHandleForOnEarlierIOSVersionsCallback = IntPtr.Zero;
    }
}