using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
class Data
{
    public int ab;
    public static int ss;
    public float b;
    public List<int> list;

    public Dictionary<string , int > dic ;
    public Iteminfo item = new Iteminfo("d",1,2,3);
    public List<Iteminfo> items = new List<Iteminfo>(){new Iteminfo("a",1,2,3),new Iteminfo("b",2,3,4),new Iteminfo("c",3,4,5)};

    public Dictionary<int,Iteminfo> itemdic = new Dictionary<int, Iteminfo>()
    {
        {1,new Iteminfo("a",1,2,3)},
        {2,new Iteminfo("b",2,3,4)},
        {3,new Iteminfo("c",3,4,5)}
    };
    public Data()
    {

    }

    public Data(int ab, int ss, float b, List<int> list, Dictionary<string, int> dic, Iteminfo item, List<Iteminfo> items, Dictionary<int, Iteminfo> itemdic)
    {
        this.ab = ab;
        Data.ss = ss;
        this.b = b;
        this.list = list;
        this.dic = dic;
        this.item = item;
        this.items = items;
        this.itemdic = itemdic;
    }

    public void Print()
    {
        Debug.Log("ab = "+ab+" ss = "+ss+" b = "+b+" list = "+list+" dic = "+dic+" item = "+item+" items = "+items+" itemdic = "+itemdic);
    }
}

public class Iteminfo
{
    public string name;
    public int HP;
    public int MP;
    public int level;

    public Iteminfo(string name, int HP, int MP, int level)
    {
        this.name = name;
        this.HP = HP;
        this.MP = MP;
        this.level = level;
    }

    public Iteminfo()
    {
    }
}

class TestData 
{
    public int a ;
    public float b;
    private string c;
    public TestData()
    {

    }

    public TestData(int a , float b , string c)
    {
        this.a = a;
        this.b = b;
        this.c = c;
    }

    public void Print()
    {
        Debug.Log("a = "+a+" b = "+b+" c = "+c);
    }
}
public class TestDataM : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        Data data = new Data(1,+2,3.0f,new List<int>(){1,2,3},new Dictionary<string, int>(){ {"a",1},{"b",2},{"c",3} },new Iteminfo("a",1,2,3),new List<Iteminfo>(){new Iteminfo("a",1,2,3),new Iteminfo("b",2,3,4),new Iteminfo("c",3,4,5)},new Dictionary<int, Iteminfo>()
        {
            {1,new Iteminfo("a",1,2,3)},
            {2,new Iteminfo("b",2,3,4)},
            {3,new Iteminfo("c",3,4,5)}
        });
        DataManager.Instance.SaveData(data,"data");
        Data data1 = DataManager.Instance.LoadtData(typeof(Data),"data") as Data;
        data1.Print();
        // PlayerPrefs.DeleteAll();    清除注册表中playerprefs的数据
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
