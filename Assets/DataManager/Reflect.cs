using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
class father
{
}

class son:father
{
     
}

public class Reflect : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 知识点回顾
        // 1T 2A
        // Type 
        // Assembly
        // Activator
        #endregion

        #region 知识点二 判断一个类型的对象是否可以让另一个类型为自己分配空间
        //父类装子类  
        Type fatherType = typeof(father);
        Type sonType = typeof(son);
        //调用者 通过该方法判断一个类型的对象是否可以通过传入的类型为自己 分配空间
        if(fatherType.IsAssignableFrom(sonType))
        {
            Debug.Log("可以分配空间");
            father f = Activator.CreateInstance(sonType) as father;  //用father类型装son类型
        }
        #endregion
    
        #region 知识点三 通过反射获取泛型类型
        List<string> list = new List<string>();
        Type listType = list.GetType();   //先获得List的类型
        Type[] types = listType.GetGenericArguments();    //再得到该类型的泛型类型
        Debug.Log(types[0]);

        Dictionary<int,string> dict = new Dictionary<int, string>();
        Type dictType = dict.GetType();
        Debug.Log(dictType);
        types = dictType.GetGenericArguments();
        Debug.Log(types[0]);
        Debug.Log(types[1]);
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
