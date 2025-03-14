using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// PlayerPrefabs数据管理类 统一管理数据的存储和读取
/// </summary>
public class PlayerPManager 
{
    public static int ss;
    private int a ;
    public float b;
    private string c;
    private bool d;
    private static PlayerPManager instance = new PlayerPManager();

    public static PlayerPManager Instance
    {
         get
         {
             return instance;
         }
    }
    /// <summary>
    /// 存储数据
    /// </summary>
    /// <param name="obj">数据对象</param>
    /// <param name="keyName">数据对象唯一的key 自己控制</param>
    public void SaveData(object obj , string keyName)   //使用object 类型接收所有数据类型  keyName为键名区分数据
    {
        Debug.Log("执行SaveData");
        Type type = obj.GetType();
        FieldInfo[] fieldInfos = type.GetFields(BindingFlags.Public|BindingFlags.NonPublic|BindingFlags.Instance|BindingFlags.Static);
        for(int i = 0; i < fieldInfos.Length; i++)
        {
            //type.Name  对应类型(类或者结构体的名字)   keyname对应实例的名字  i为例区分数据
            if(fieldInfos[i].FieldType == typeof(int))
            {
                PlayerPrefs.SetInt(type.Name+""+keyName+i, (int)fieldInfos[i].GetValue(obj));   
                Debug.Log(type.Name+""+keyName+i);
            }
            if(fieldInfos[i].FieldType == typeof(float))
            {
                PlayerPrefs.SetFloat(type.Name+""+keyName+i, (float)fieldInfos[i].GetValue(obj));
                Debug.Log(type.Name+""+keyName+i);

            }
            if(fieldInfos[i].FieldType == typeof(string))
            {
                PlayerPrefs.SetString(type.Name+""+keyName+i, (string)fieldInfos[i].GetValue(obj));
                Debug.Log(type.Name+""+keyName+i);

            }
            if(fieldInfos[i].FieldType == typeof(bool))
            {
                PlayerPrefs.SetInt(type.Name+""+keyName+i, (bool)fieldInfos[i].GetValue(obj) ? 1 : 0);
                Debug.Log(type.Name+""+keyName+i);

            }
            if(fieldInfos[i].FieldType == typeof(double))
            {
                double a = (double)fieldInfos[i].GetValue(obj) ;
                PlayerPrefs.SetFloat(type.Name+""+keyName+i, (float)a);
                Debug.Log(type.Name+""+keyName+i);

            } 
            if(fieldInfos[i].FieldType == typeof(char))
            {
                PlayerPrefs.SetString(type.Name+""+keyName+i, fieldInfos[i].GetValue(obj).ToString());
                Debug.Log(type.Name+""+keyName+i);
            }   
        }

    }
    /// <summary>
    /// 读取数据 
    /// </summary>
    /// <param name="keyName"></param>
    /// <returns></returns> <summary>
    /// 
    /// </summary>
    /// <param name="keyName"></param>
    /// <returns></returns>
    public object LoadtData(Type type , string keyName)    // 使用Type类型接收所有数据类型  keyName为键名  区分数据
                                                           //使用Type 而不使用object类型  使用的时候不需要new一个对象来传入 而是直接传入类型  在内部动态创建返回相应的类型对象
    {
        Debug.Log("执行LoadtData");
        // Debug.Log(type);
        // Debug.Log(type.Name);
        object obj = Activator.CreateInstance(type);   //不确定是否实现了构造函数  否则直接传入构造函数即可   
        FieldInfo[] fieldInfos = type.GetFields(BindingFlags.Public|BindingFlags.NonPublic|BindingFlags.Instance|BindingFlags.Static);     
        //可以通过FieldInfo中的SetValue方法来设置对象的属性值
        for(int i = 0; i < fieldInfos.Length; i++)
        {
            if(fieldInfos[i].FieldType == typeof(int))
                 fieldInfos[i].SetValue(obj,PlayerPrefs.GetInt(type.Name+""+keyName+i));
            if(fieldInfos[i].FieldType == typeof(float))
                 fieldInfos[i].SetValue(obj,PlayerPrefs.GetFloat(type.Name+""+keyName+i));
            if(fieldInfos[i].FieldType == typeof(string))
                 fieldInfos[i].SetValue(obj,PlayerPrefs.GetString(type.Name+""+keyName+i));
            if(fieldInfos[i].FieldType == typeof(bool))
                 fieldInfos[i].SetValue(obj,PlayerPrefs.GetInt(type.Name+""+keyName+i) == 1 ? true : false);
            if(fieldInfos[i].FieldType == typeof(double))
                 fieldInfos[i].SetValue(obj,PlayerPrefs.GetFloat(type.Name+""+keyName+i));
            if(fieldInfos[i].FieldType == typeof(char))
                 fieldInfos[i].SetValue(obj,PlayerPrefs.GetString(type.Name+""+keyName+i)[0]);     
            Debug.Log(PlayerPrefs.GetString(type.Name+""+keyName+i));                      
        }
        
        return obj;
    }

    private PlayerPManager()
     {
        //保证外部不能new()
     }
}
