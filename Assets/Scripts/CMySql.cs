using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//using System.Data;
using System.IO;
using UnityEngine.SceneManagement;

public class CMySql : MonoBehaviour
{
    //public static MySqlConnection dbConnection;//Just like MyConn.conn in StoryTools before    
    static string host = "127.0.0.1";
    static string id = "root";  //***不要变***  
    static string pwd = "";  //密码  
    static string database = "unity";//数据库名    
    static int dia = 0;
	static string result="";
	static string test_str = "";
	static string[] tower_arr = new string[6];
	static string[] info = new string[6];
	static int[] price = new int[6];
	static int filedia = -1;
	static string filetower_str = " ";
	static string[] filetower_arr = new string[6];
	static int flag = 0;

    private string strCommand = "Select ID from unity ;";
    //public static DataSet MyObj;

    void OnGUI()
    {
			createfile();
			if(flag == 0) {
				filedia = int.Parse(readfile("MLdata.txt"));
				filetower_arr = readfile("MLtower.txt").Split('-');
				info[0] = " Bowling \n\n Life: 100   Damage: 20 \n Speed: 10 Range: 10";
				info[1] = " Cannon \n\n Life: 200   Damage: 30 \n Speed: 20 Range: 20";
				info[2] = " Radio \n\n Life: 150   Damage: 40 \n Speed: 30 Range: 20";
				info[3] = " Flame Ejector \n\n Life: 160   Damage: 60 \n Speed: 50 Range: 30";
				info[4] = " Snow Man \n\n Life: 180   Damage: 80 \n Speed: 10 Range: 70";
				info[5] = " Tesla \n\n Life: 200   Damage: 20 \n Speed: 80 Range: 10";
				price[0] = 50;
				price[1] = 150;
				price[2] = 80;
				price[3] = 80;
				price[4] = 30;
				price[5] = 50;
				flag = 1;
			}
			
			
			 
        
            //string connectionString = string.Format("Server = {0}; Database = {1}; User ID = {2}; Password = {3};", host, database, id, pwd);
            //openSqlConnection(connectionString);
            //MyObj = GetDataSet(strCommand);

            //读取数据函数  
            //ReaderData();
		
		//GUI.color = Color.yellow;
		
		GUIStyle DiafontStyle = new GUIStyle();  
        DiafontStyle.normal.background = null;    //设置背景填充  
        DiafontStyle.normal.textColor= Color.yellow;   //设置字体颜色  
        DiafontStyle.fontSize = 20;  
        GUI.Label(new Rect(400,15,80,60),"Diamond: "+filedia, DiafontStyle);

        if (GUI.Button(new Rect(120, 10, 75, 35), "Back"))
        {
            SceneManager.LoadScene("MainScene");
        }

        GUIStyle InfofontStyle = new GUIStyle();  
        InfofontStyle.normal.background = null;    //设置背景填充  
        InfofontStyle.normal.textColor= Color.white;   //设置字体颜色  
        InfofontStyle.fontSize = 13;  
		
		GUIStyle ButtonfontStyle = new GUIStyle();    
        ButtonfontStyle.normal.textColor= Color.white;   //设置字体颜色  
        ButtonfontStyle.fontSize = 12;  
		
		GUI.Label(new Rect(150, 100, 100, 100), info[0],InfofontStyle);
		GUI.Label(new Rect(420,100, 100, 100), info[1],InfofontStyle);
		GUI.Label(new Rect(680, 100, 100, 100), info[2],InfofontStyle);
		GUI.Label(new Rect(150, 240, 100, 100), info[3],InfofontStyle);
		GUI.Label(new Rect(420, 240, 100, 100), info[4],InfofontStyle);
		GUI.Label(new Rect(680, 240, 100, 100), info[5],InfofontStyle);
		if(!filetower_arr[0].Equals("1")){
			if(filedia>=price[0]){
			GUI.backgroundColor = Color.green;
			if (GUI.Button(new  Rect(50, 170, 75, 35),"Buy $"+price[0])){
				buyIt(0);
			}
			}
			else {
				GUI.backgroundColor = Color.grey;
				if (GUI.Button(new  Rect(50, 170, 75, 35),"No Money")){}
			}
        }
		else{
			GUI.backgroundColor = Color.red;
			if(GUI.Button(new Rect(50, 170, 75, 35),"Sell $"+(price[0]/2))){
				sellIt(0);
			}
		}
		
		if(!filetower_arr[1].Equals("1")){
			if(filedia>=price[1]){
			GUI.backgroundColor = Color.green;
			if (GUI.Button(new  Rect(330, 170, 75, 35),"Buy $"+price[1])){
				buyIt(1);
			}
			}
			else {
				GUI.backgroundColor = Color.grey;
				if (GUI.Button(new  Rect(330, 170, 75, 35),"No Money")){}
			}
        }
		else{
			GUI.backgroundColor = Color.red;
			if(GUI.Button(new Rect(330, 170, 75, 35),"Sell $"+(price[1]/2))){
				sellIt(1);
			}
		}
		
		if(!filetower_arr[2].Equals("1")){
			if(filedia>=price[2]){
			GUI.backgroundColor = Color.green;
			if (GUI.Button(new  Rect(600, 170, 75, 35),"Buy $"+price[2])){
				buyIt(2);
			}
			}
			else {
				GUI.backgroundColor = Color.grey;
				if (GUI.Button(new  Rect(600, 170, 75, 35),"No Money")){}
			}
        }
		else{
			GUI.backgroundColor = Color.red;
			if(GUI.Button(new Rect(600,170,75,35),"Sell $"+(price[2]/2))){
				sellIt(2);
			}
		}
		
		
		
		if(!filetower_arr[3].Equals("1")){
			if(filedia>=price[3]){
			GUI.backgroundColor = Color.green;
			if (GUI.Button(new  Rect(50, 320, 75, 35),"Buy $"+price[3])){
				buyIt(3);
			}
			}
			else {
				GUI.backgroundColor = Color.grey;
				if (GUI.Button(new  Rect(50, 320, 75, 35),"No Money")){}
			}
        }
		else{
			GUI.backgroundColor = Color.red;
			if(GUI.Button(new Rect(50, 320, 75, 35),"Sell $"+(price[3]/2))){
				sellIt(3);
			}
		}
		
		if(!filetower_arr[4].Equals("1")){
			if(filedia>=price[4]){
			GUI.backgroundColor = Color.green;
			if (GUI.Button(new  Rect(330, 320, 75, 35),"Buy $"+price[4])){
				buyIt(4);
			}
			}
			else {
				GUI.backgroundColor = Color.grey;
				if (GUI.Button(new  Rect(330, 320, 75, 35),"No Money")){}
			}
        }
		else{
			GUI.backgroundColor = Color.red;
			if(GUI.Button(new Rect(330, 320, 75, 35),"Sell $"+(price[4]/2))){
				sellIt(4);
			}
		}
		
		if(!filetower_arr[5].Equals("1")){
			if(filedia>=price[5]){
			GUI.backgroundColor = Color.green;
			if (GUI.Button(new  Rect(600, 320, 75, 35),"Buy $"+price[5])){
				buyIt(5);
			}
			}
			else {
				GUI.backgroundColor = Color.grey;
				if (GUI.Button(new  Rect(600, 320, 75, 35),"No Money")){}
			}
        }
		else{
			GUI.backgroundColor = Color.red;
			if(GUI.Button(new Rect(600,320,75,35),"Sell $"+(price[5]/2))){
				sellIt(5);
			}
		}
		
    }

    // On quit    
    public static void OnApplicationQuit()
    {
        //closeSqlConnection();
		File.Delete("MLtower.txt");
		File.Delete("MLdata.txt");
		Onquitcreatefile();
    }

    
	void buyIt(int id){
	//	tower_arr[id] = "1";
	//	dia = dia-(id+1)*10;
	//	string update = "update test_user set tower = '"+tower_arr[0]+"-"+tower_arr[1]+"-"+tower_arr[2]+"-"+tower_arr[3]+"-"+tower_arr[4]+"-"+tower_arr[5]+"', dia = "+dia+";";
	//	MySqlCommand mySqlCommand = new MySqlCommand(update, dbConnection);
	//	MySqlDataReader reader = mySqlCommand.ExecuteReader();
		filetower_arr[id] = "1";
		filedia = filedia-price[id];
		updatefile();
	}
	
	void sellIt(int id){
	//	tower_arr[id] = "0";
	//	dia = dia+(id+1)*5;
	//	string update = "update test_user set tower = '"+tower_arr[0]+"-"+tower_arr[1]+"-"+tower_arr[2]+"-"+tower_arr[3]+"-"+tower_arr[4]+"-"+tower_arr[5]+"', dia = "+dia+";";
	//	MySqlCommand mySqlCommand = new MySqlCommand(update, dbConnection);
	//	MySqlDataReader reader = mySqlCommand.ExecuteReader();
		filetower_arr[id] = "0";
		filedia = filedia+price[id]/2;
		updatefile();
	}
	
	string readfile(string name){
		StreamReader sr;  
        if(File.Exists(name))  
        {  
            sr=File.OpenText(name);  
        }  
        else  
        {  
            Debug.LogWarning("Not find files!");  
            return null;  
        }    
        string str;  
        str=sr.ReadLine();//加上str的临时变量是为了避免sr.ReadLine()在一次循环内执行两次  
        sr.Close ();  
        sr.Dispose ();  
        return str; 
	}
	
	void createfile()  
    {  
         
        if(!File.Exists("MLdata.txt"))  
        {  
			StreamWriter sw; 
            sw=File.CreateText("MLdata.txt");//创建一个用于写入 UTF-8 编码的文本  
            Debug.Log("文件创建成功！"); 
			sw.WriteLine("200");//以行为单位写入字符串  
			sw.Close ();  
			sw.Dispose ();//文件流释放 			
        }  
		if(!File.Exists("MLtower.txt"))  
        {  
			StreamWriter sw; 
            sw=File.CreateText("MLtower.txt");//创建一个用于写入 UTF-8 编码的文本  
            Debug.Log("文件创建成功！"); 
			sw.WriteLine("1-0-0-0-0-0");//以行为单位写入字符串  
			sw.Close ();  
			sw.Dispose ();//文件流释放 			
        } 
         
    }
	
	public static void Onquitcreatefile(){
		if(!File.Exists("MLdata.txt"))  
        {  
			StreamWriter sw; 
            sw=File.CreateText("MLdata.txt");//创建一个用于写入 UTF-8 编码的文本  
            Debug.Log("文件创建成功！"); 
			sw.WriteLine(filedia+"");//以行为单位写入字符串  
			sw.Close ();  
			sw.Dispose ();//文件流释放 			
        }  
		if(!File.Exists("MLtower.txt"))  
        {  
			string res = "";
			for(int i = 0;i<5;i++){
				res = res+filetower_arr[i]+"-";
			}
			res = res+filetower_arr[5];
			StreamWriter sw; 
            sw=File.CreateText("MLtower.txt");//创建一个用于写入 UTF-8 编码的文本  
            Debug.Log("文件创建成功！"); 
			sw.WriteLine(res);//以行为单位写入字符串  
			sw.Close ();  
			sw.Dispose ();//文件流释放 			
        } 
	}
	
	public void updatefile(){
		if(File.Exists("MLdata.txt"))  
        {  
			StreamWriter sw; 
            sw=new StreamWriter("MLdata.txt",false);//创建一个用于写入 UTF-8 编码的文本  
			sw.WriteLine(filedia+"");//以行为单位写入字符串  
			sw.Close ();  
			sw.Dispose ();//文件流释放 			
        }  
		if(File.Exists("MLtower.txt"))  
        {  
			string res = "";
			for(int i = 0;i<5;i++){
				res = res+filetower_arr[i]+"-";
			}
			res = res+filetower_arr[5];
			StreamWriter sw; 
            sw=new StreamWriter("MLtower.txt",false);//创建一个用于写入 UTF-8 编码的文本  
			sw.WriteLine(res);//以行为单位写入字符串  
			sw.Close ();  
			sw.Dispose ();//文件流释放 			
        } 
	}
}
