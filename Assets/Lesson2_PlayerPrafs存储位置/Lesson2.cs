using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 playerprefs存储的数据在哪里
        // 不同平台不一样
        
        #region Windows
        //PlayerPrefs 存储在
        //HKCU\software\[公司名称]\[产品名称]项下的注册表中    ----注册表中的路径
        //其中公司和产品名称是 在“Project settings”中设置的名称
        #endregion
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
