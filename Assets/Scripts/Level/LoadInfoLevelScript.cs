using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Xml;
using UnityEditor;
public class LoadInfoLevelScript : MonoBehaviour{
	private static string LevelNameFile = "FileLevelInfo";
    //Read Records    	   
    public static void ReadEnemiesLevel(List<Enemy> listEnemy,int level){
    	listEnemy.Clear();
    	Enemy TempObject;
    	CheckFile(LevelNameFile);
		List<string> rows = ReadFile(LevelNameFile);
		foreach(string row in rows){
			if(row.Trim() != string.Empty){
				TempObject = JsonUtility.FromJson<Enemy>(row);
					listEnemy.Add(TempObject);
			}
		}
	}
//Help	
	private static List<string> ReadFile(string NameFile){
		List<string> ListResult = new List<string>(); 
		try{
			ListResult = new List<string>(File.ReadAllLines(Application.dataPath + "/" + NameFile + ".data"));
		}catch{}
		return ListResult;
	}
    public static void CheckFile(string NameFile){
    	if(!File.Exists(Application.dataPath + "/" + NameFile + ".data")){
    		CreateFile(NameFile);
    	}
    }
    public static void CreateFile(string NameFile){
        StreamWriter sw = CreateStream(NameFile, false);
        sw.Close();
    }
    private static StreamWriter CreateStream(string NameFile, bool AppendFlag){
    	return new StreamWriter(Application.dataPath + "/" + NameFile + ".data", append: AppendFlag);
    	
    }
}
