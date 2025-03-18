using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Serialization;
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
            SaveValue(infos[i].GetValue(obj),saveKeyName);
        }
        #endregion
    }
    /// <summary>
    /// 读取数据 
    /// 使用Type类型接收所有数据类型  keyName为键名  区分数据
    ///使用Type 而不使用object类型  使用的时候不需要new一个对象来传入 而是直接传入类型  在内部动态创建返回相应的类型对象
    /// </summary>
    /// <param name="keyName"></param>
    /// <returns></returns> <summary>
    /// 
    /// </summary>
    /// <param name="keyName"></param>
    /// <returns></returns>
    public object LoadtData(Type type , string keyName)    
    {
        object data = Activator.CreateInstance(type);
        //得到所有字段
        FieldInfo[] infos = type.GetFields(BindingFlags.Public|BindingFlags.NonPublic|BindingFlags.Instance|BindingFlags.Static);
        //拼接字符串
        string loadKeyName = "";    
        for(int i = 0; i < infos.Length; i++)
        {
            loadKeyName = keyName+"_"+type.Name+"_"+infos[i].FieldType.Name+"_"+infos[i].Name;    //和存储时一致  才能读出
            infos[i].SetValue(data,LoadValue(infos[i].FieldType,loadKeyName));
        }
        return data;
    }

    /// <summary>
    /// 得到单个数据的方法
    /// </summary>
    /// <param name="fieldType"></param>
    /// <param name="keyName"></param>
    private object LoadValue(Type fieldType,string keyName)
    {
        //根据不同的数据类型选择相应的playerprefs的API进行存储
        if(fieldType == typeof(int))
        {
            return PlayerPrefs.GetInt(keyName);
        }
        else if(fieldType == typeof(float))
        {
            return PlayerPrefs.GetFloat(keyName);
        }
        else if(fieldType == typeof(string))
        {
           return PlayerPrefs.GetString(keyName);
        }
        else if(fieldType == typeof(bool))
        {
            return PlayerPrefs.GetInt(keyName) == 1 ? true : false;
        }
        else if(typeof(IList).IsAssignableFrom(fieldType))
        {
            int count = PlayerPrefs.GetInt(keyName);
            IList list = Activator.CreateInstance(fieldType) as IList;
            for(int i = 0; i < count; i++)
            {
                list.Add(LoadValue(fieldType.GetGenericArguments()[0],keyName+i));
            }
            return list;
        }
        else if(typeof(IDictionary).IsAssignableFrom(fieldType))
        {
            int count = PlayerPrefs.GetInt(keyName);
            IDictionary dic = Activator.CreateInstance(fieldType) as IDictionary;
            for(int i = 0; i < count; i++)
            {
                object key = LoadValue(fieldType.GetGenericArguments()[0],keyName+"_key_"+i);
                object value = LoadValue(fieldType.GetGenericArguments()[1],keyName+"_value_"+i);
                dic.Add(key,value);
            }
            return dic;
        }
        else
        {
            return LoadtData(fieldType,keyName);
        }
    }

    private DataManager()
     {
        //保证外部不能new()
     }

     private void SaveValue(object obj,string keyName)
     {
            //根据不同的数据类型选择相应的playerprefs的API进行存储
            Type fieldType = obj.GetType();
            //类型判断
            //是不是int
            if(fieldType == typeof(int))
            {
                PlayerPrefs.SetInt(keyName, (int)obj);
            }
            else if(fieldType == typeof(float))
            {
                PlayerPrefs.SetFloat(keyName, (float)obj);
            }
            else if(fieldType == typeof(string))
            {
                PlayerPrefs.SetString(keyName, (string)obj);
            }
            else if(fieldType == typeof(bool))
            {
                PlayerPrefs.SetInt(keyName, (bool)obj ? 1 : 0);    //自己定义规则存储bool
            }
            //如何判断 泛型类的类型呢
            //通过反射 判断 父子关系
            //这相当于是判断 字段是不是IList的子类
            else if(typeof(IList).IsAssignableFrom(fieldType))
            {
                  //里氏替换
                  IList list = obj as IList;
                  //先存储数目
                  PlayerPrefs.SetInt(keyName,list.Count);
                  int index = 0;
                  foreach(object item in list)
                  {
                      //存储数据
                        SaveValue(item,keyName+index);
                        ++index;    
                  }
            }
            //判断是不是Dictionary类型 通过Dictionary的父类来判断
            else if(typeof(IDictionary).IsAssignableFrom(fieldType))
            {
                IDictionary dic = obj as IDictionary;
                //先存储长度
                PlayerPrefs.SetInt(keyName,dic.Count);
                int index = 0;
                foreach(object key in dic.Keys)
                {
                    //存储key
                    SaveValue(key,keyName+"_key_"+index);
                    //存储value
                    SaveValue(dic[key],keyName+"_value_"+index);
                    ++index;
                }
            }
            else
            {
                //自定义类型
                //递归存储       -----相当于再次传入一个类(自定义类型)  进行存储
                SaveData(obj,keyName);
            }
     }
}