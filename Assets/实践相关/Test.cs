using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;
using UnityEngine;
class pp
{
    public int a;
    public string b;
    public pp(int a, string b)
    {
        this.a = a;
        this.b = b;
    }
    public pp()
    {

    }
}

struct TestStruct
{
    public int a ;
    public string b;
    public double k;
    private char c;
    
    static double d ;

    public TestStruct(int a ,string b ,double k, char c,double d)
    {
        this.a = a;
        this.b = b;
        this.k = k;
        this.c = c;
        TestStruct.d = d;
    }

    public override string ToString()
    {
        return string.Format("{0},{1},{2},{3},{4}",a,b,k,c,d);
    }
}
public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Type type = typeof(PlayerPManager);
        // Debug.Log(type.Name);
        // FieldInfo[] fieldInfos = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic|BindingFlags.Instance|BindingFlags.Static);   
        //Instance 获取实例字段  static 获取静态字段
        // Type type = typeof(pp);
        // Debug.Log(type.Name);
        // FieldInfo[] fieldInfos = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic|BindingFlags.Instance|BindingFlags.Static);
        // Debug.Log(fieldInfos[0]);
        // Debug.Log(fieldInfos[0].FieldType);
        // Debug.Log(typeof(int).Name);
        // Debug.Log(fieldInfos[0].FieldType.Name);
        // Debug.Log(fieldInfos[0].FieldType.Name == typeof(int).Name);
        // Debug.Log(fieldInfos[0].Name);   //声明类型的名字
        //加载类数据
    //     object a = new pp(1,"2");

    //     Debug.Log(a.ToString());
    //     PlayerPManager.Instance.SaveData(a,"one");
    //     PlayerPManager.Instance.SaveData(a,"two");

    //     pp c = (PlayerPManager.Instance.LoadtData(typeof(pp),"one")) as pp;
    //     Debug.Log("加载pp的信息：");
    //     Debug.Log(c.a);
    //     Debug.Log(c.b);

        //加载结构体数据
        TestStruct a = new TestStruct(1,"one",5.0f,'c',5.0f);
        PlayerPManager.Instance.SaveData(a,"one");
        TestStruct ss = (TestStruct)PlayerPManager.Instance.LoadtData(typeof(TestStruct),"one");
        Debug.Log("-------------------------------------");
        Debug.Log(ss);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
