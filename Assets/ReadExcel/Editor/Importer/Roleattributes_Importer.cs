using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

public class Roleattributes_Importer : AssetPostprocessor {
	private static readonly string filePath = "Assets/ReadExcel/Config/Excel/角色属性.xlsx";
	private static readonly string exportPath = "Assets/ReadExcel/Config/Resources/DataConfig/Roleattributes"+ ".asset";
	private static readonly string[] sheetNames = { "Sheet1", };
	
	static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		foreach (string asset in importedAssets) {
			if (!filePath.Equals (asset))
				continue;
			
			Directory.CreateDirectory("Assets/ReadExcel/Config/Resources/DataConfig/ ");
			Roleattributes data = (Roleattributes)AssetDatabase.LoadAssetAtPath (exportPath, typeof(Roleattributes));
			if (data == null) {
				data = ScriptableObject.CreateInstance<Roleattributes> ();
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

					Roleattributes.Sheet s = new Roleattributes.Sheet ();
					s.name = sheetName;
				
					for (int i=2; i<= sheet.LastRowNum; i++) {
						IRow row = sheet.GetRow (i);
						ICell cell = null;
						
						Roleattributes.Param p = new Roleattributes.Param ();
						
                        cell = row.GetCell(0); p.id = (int)(cell == null ? 0 : cell.NumericCellValue);
                        cell = row.GetCell(1); p.strength = (int)(cell == null ? 0 : cell.NumericCellValue);
                        cell = row.GetCell(2); p.agile = (int)(cell == null ? 0 : cell.NumericCellValue);
                        cell = row.GetCell(3); p.body = (int)(cell == null ? 0 : cell.NumericCellValue);
                        cell = row.GetCell(4); p.perception = (int)(cell == null ? 0 : cell.NumericCellValue);
                        cell = row.GetCell(5); p.intelligence = (int)(cell == null ? 0 : cell.NumericCellValue);
                        cell = row.GetCell(6); p.luck = (int)(cell == null ? 0 : cell.NumericCellValue);
                        cell = row.GetCell(7); p.hp = (int)(cell == null ? 0 : cell.NumericCellValue);
                        cell = row.GetCell(8); p.hpGrowth = (float)(cell == null ? 0 : cell.NumericCellValue);
                        cell = row.GetCell(9); p.mp = (int)(cell == null ? 0 : cell.NumericCellValue);
                        cell = row.GetCell(10); p.mpGrowth = (float)(cell == null ? 0 : cell.NumericCellValue);
                        cell = row.GetCell(11); p.attack = (int)(cell == null ? 0 : cell.NumericCellValue);
                        cell = row.GetCell(12); p.attackGrowth = (float)(cell == null ? 0 : cell.NumericCellValue);
                        cell = row.GetCell(13); p.Mattack = (int)(cell == null ? 0 : cell.NumericCellValue);
                        cell = row.GetCell(14); p.MattackGrowth = (float)(cell == null ? 0 : cell.NumericCellValue);
                        cell = row.GetCell(15); p.armor = (int)(cell == null ? 0 : cell.NumericCellValue);
                        cell = row.GetCell(16); p.MagicRes = (int)(cell == null ? 0 : cell.NumericCellValue);
                        cell = row.GetCell(17); p.Resistance = (int)(cell == null ? 0 : cell.NumericCellValue);
                        cell = row.GetCell(18); p.Dodge = (int)(cell == null ? 0 : cell.NumericCellValue);
                        cell = row.GetCell(19); p.DodgeGrowth = (float)(cell == null ? 0 : cell.NumericCellValue);
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
