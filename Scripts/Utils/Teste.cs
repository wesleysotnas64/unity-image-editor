using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;

public class Teste : MonoBehaviour
{
    public List<int> l1;
    void Start()
    {
        System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
        sw.Start();
        l1 = new List<int>();
        sw.Stop();
        Debug.Log(sw.Elapsed.TotalMilliseconds);
        l1.Add(8);
        l1.Add(3);
        l1.Add(4);
        l1.Add(9);

        PrintList(l1);

        List<int> l2 = new List<int>();
        l2 = l1;

        l2.Sort((a, b) => a.CompareTo(b));
        PrintList(l2);
    }

    private void PrintList(List<int> l)
    {
        foreach(int i in l)
        {
            Debug.Log(i);
        }
    }

}
