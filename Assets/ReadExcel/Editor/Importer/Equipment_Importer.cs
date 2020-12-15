using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.Collections.Generic;

public class Equipment_Importer : AssetPostprocessor {
	private static readonly string filePath = "Assets/ReadExcel/Config/Excel/装备.xlsx";
	private static readonly string exportPath = "Assets/ReadExcel/Config/Resources/DataConfig/Equipment"+ ".asset";
	private static readonly string[] sheetNames = { "Sheet1", };
	
	static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		foreach (string asset in importedAssets) {
			if (!filePath.Equals (asset))
				continue;
			
			Directory.CreateDirectory("Assets/ReadExcel/Config/Resources/DataConfig/ ");
			Equipment data = (Equipment)AssetDatabase.LoadAssetAtPath (exportPath, typeof(Equipment));
			if (data == null) {
				data = ScriptableObject.CreateInstance<Equipment> ();
				AssetDatabase.CreateAsset ((ScriptableObject)data, exportPath);
				data.hideFlags = HideFlags.NotEditable;
			}
			
			ReadExcel.CreatDataInitCs();
			data.sheets.Clear ();
			using (FileStream stream = File.Open (filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
				IWorkbook book = null;
				if (Path.GetExtension (filePath) == ".xls") {
					book = new HSSFWorkbook(stream);
				} else {
					book = new XSSFWorkbook(stream);
				}
				
				foreach(string sheetName in sheetNames) {
					ISheet sheet = book.GetSheet(sheetName);
					if( sheet == null ) {
						Debug.LogError("[QuestData] sheet not found:" + sheetName);
						continue;
					}

					Equipment.Sheet s = new Equipment.Sheet ();
					s.name = sheetName;
				
					for (int i=2; i<= sheet.LastRowNum; i++) {
						IRow row = sheet.GetRow (i);
						ICell cell = null;
						
						Equipment.Param p = new Equipment.Param ();
						
                        cell = row.GetCell(0); p.id = (int)(cell == null ? 0 : cell.NumericCellValue);
                        cell = row.GetCell(1); p.equipTypeID = (int)(cell == null ? 0 : cell.NumericCellValue);
                        cell = row.GetCell(2); p.weight = (int)(cell == null ? 0 : cell.NumericCellValue);
                        cell = row.GetCell(3); p.attack = (int)(cell == null ? 0 : cell.NumericCellValue);
                        cell = row.GetCell(4); p.armor = (int)(cell == null ? 0 : cell.NumericCellValue);
                        cell = row.GetCell(5); p.MagicResistance = (int)(cell == null ? 0 : cell.NumericCellValue);
                        cell = row.GetCell(6); p.strength = (int)(cell == null ? 0 : cell.NumericCellValue);
                        cell = row.GetCell(7); p.agile = (int)(cell == null ? 0 : cell.NumericCellValue);
                        cell = row.GetCell(8); p.body = (int)(cell == null ? 0 : cell.NumericCellValue);
                        cell = row.GetCell(9); p.perception = (int)(cell == null ? 0 : cell.NumericCellValue);
                        cell = row.GetCell(10); p.intelligence = (int)(cell == null ? 0 : cell.NumericCellValue);
                        cell = row.GetCell(11); p.will = (int)(cell == null ? 0 : cell.NumericCellValue);
                        cell = row.GetCell(12); p.ability = (int)(cell == null ? 0 : cell.NumericCellValue);
                        cell = row.GetCell(13); p.propertyDecision = (int)(cell == null ? 0 : cell.NumericCellValue);
                        cell = row.GetCell(14); p.decisionTime = (int)(cell == null ? 0 : cell.NumericCellValue);
                        cell = row.GetCell(15); p.specialRequireID = (int)(cell == null ? 0 : cell.NumericCellValue);
                        cell = row.GetCell(16); p.forgingNum = (int)(cell == null ? 0 : cell.NumericCellValue);
                        p.forging1 = new List<string>();                        cell = row.GetCell(17);var forging1temp = (cell == null ? "" : cell.StringCellValue);string [] forging1temps=forging1temp.Split(',');for(int index =0,iMax=forging1temps.Length;index<iMax;index++){string val=forging1temps[index];p.forging1.Add(val);}
                        p.forging2 = new List<string>();                        cell = row.GetCell(18);var forging2temp = (cell == null ? "" : cell.StringCellValue);string [] forging2temps=forging2temp.Split(',');for(int index =0,iMax=forging2temps.Length;index<iMax;index++){string val=forging2temps[index];p.forging2.Add(val);}
                        p.forging3 = new List<string>();                        cell = row.GetCell(19);var forging3temp = (cell == null ? "" : cell.StringCellValue);string [] forging3temps=forging3temp.Split(',');for(int index =0,iMax=forging3temps.Length;index<iMax;index++){string val=forging3temps[index];p.forging3.Add(val);}
                        p.hasForging = new List<string>();                        cell = row.GetCell(20);var hasForgingtemp = (cell == null ? "" : cell.StringCellValue);string [] hasForgingtemps=hasForgingtemp.Split(',');for(int index =0,iMax=hasForgingtemps.Length;index<iMax;index++){string val=hasForgingtemps[index];p.hasForging.Add(val);}
						s.list.Add (p);
					}
					data.sheets.Add(s);
				}
			}

			ScriptableObject obj = AssetDatabase.LoadAssetAtPath (exportPath, typeof(ScriptableObject)) as ScriptableObject;
			EditorUtility.SetDirty (obj);
		}
	}
}
