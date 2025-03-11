using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 PlayerPrefs是什么
        // unity提供的可以用于存储读取玩家数据的公共类
        #endregion

        #region 知识点二 存储相关
        // PlayerPrefs的数据存储 类似于键值对存储 一个键对应一个值
        // 提供了存储3种数据的方法 int float string
        // 键：string类型
        // 值：int float string 对应3种API

        PlayerPrefs.SetInt("score", 100);  
        PlayerPrefs.SetFloat("money", 100.0f);
        PlayerPrefs.SetString("name", "张三");
        //直接调用Set相关方法 只会把数据存储到内存中
        // 当游戏结束时 unity会自动把内存中的数据存储到硬盘中
        //如果游戏异常结束 数据不会存储到硬盘中
        
        //调用Save方法就会立即存储到硬盘
        PlayerPrefs.Save();

        //具有局限性 只能存储3种类型数据
        //如果你想要存储别的类型的数据 只能降低精度 或者上升精度来进行存储
        
        // 同一键名存入数据将会覆盖  (存入相同类型和不同类型都会覆盖)、
        PlayerPrefs.SetFloat("score", 99.99f);  
        #endregion
    
        #region 知识点三 读取相关
        //注意 运行时 只要你set了对应键值对
        //即使你没有马上存储save在本地
        //也能够读取出信息     会读取内存和硬盘两个地方的数据

        // int
        int score = PlayerPrefs.GetInt("score");   //之前int的score已经被覆盖了   打印int的默认值0
        // int score = PlayerPrefs.GetInt("score",100);  //也可以在后面指定一个默认值  没有指定类型的值就返回指定默认值,进行初始化
        Debug.Log("之前int的score:"+score);
        float score2 = PlayerPrefs.GetFloat("score");
        Debug.Log("覆盖后float的score:"+score2);
        // float
        float money = PlayerPrefs.GetFloat("money");
        Debug.Log(money);
        // string
        string name = PlayerPrefs.GetString("name");
        Debug.Log(name);

        //判断是否存在
        if(PlayerPrefs.HasKey("score"))
        {
            Debug.Log("存在score");     //可以防止使用存在的键值进行覆盖 丢失数据
        }
        #endregion

        #region 知识点四 删除数据
        //删除指定键值对 
        PlayerPrefs.DeleteKey("score");
        //删除所有键值对
        PlayerPrefs.DeleteAll();

        //也存在set的问题  异常退出时 硬盘数据不会删除
        //可以手动调用save方法进行存储
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
