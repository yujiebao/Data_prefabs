using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 这个类是用于下面的测试的 示例类
/// </summary>
class PlayerInfo
{
    public string name;
    public int HP;
    public int MP;
    public int level;
}


/// <summary>
/// PlayerPrefabs数据管理类 统一管理数据的存储和读取
/// </summary>
public class DataManager
{
    public static int ss;
    private int a ;
    public float b;
    private string c;
    private bool d;
    private static DataManager instance = new DataManager();

    public static DataManager Instance
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
        #region 第一步 获取传入数据的所有字段
         Type dataType = obj.GetType();
         //得到所有的字段
         FieldInfo[] infos = dataType.GetFields(BindingFlags.Public|BindingFlags.NonPublic|BindingFlags.Instance|BindingFlags.Static);
        #endregion

        #region 第二步 自己定义key的规则 进行数据存储
        //保证key的唯一性
        // keyName_数据类型_字段类型_字段名
        #endregion

        #region 第三步 遍历字段 进行数据存储
        string saveKeyName = "";
        for(int i = 0; i < infos.Length; i++)
        {
            //keyName_PlayerInfo_int32_HP
             saveKeyName = keyName+"_"+dataType.Name+"_"+infos[i].FieldType.Name+"_"+infos[i].Name;

             //得到key的规则
             //接下来进行playerprefs存储
            SaveValue(infos[i].FieldType,saveKeyName);
        }
        #endregion
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
         return null;
    }

    private DataManager()
     {
        //保证外部不能new()
     }

     private void SaveValue(object obj,string keyName)
     {
            //根据不同的数据类型选择相应的playerprefs的API进行存储
            Type fieldType = obj.GetType();
     }
}
