using UnityEngine;
using System.Runtime.InteropServices;

public class Headers {

    [DllImport("__Internal")]
    private static extern void Hello();
}