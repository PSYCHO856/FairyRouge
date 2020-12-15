using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

public class Forging_Importer : AssetPostprocessor {
	private static readonly string filePath = "Assets/ReadExcel/Config/Excel/装备改造.xlsx";
	private static readonly string exportPath = "Assets/ReadExcel/Config/Resources/DataConfig/Forging"+ ".asset";
	private static readonly string[] sheetNames = { "Sheet1", };
	
	static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		foreach (string asset in importedAssets) {
			if (!filePath.Equals (asset))
				continue;
			
			Directory.CreateDirectory("Assets/ReadExcel/Config/Resources/DataConfig/ ");
			Forging data = (Forging)AssetDatabase.LoadAssetAtPath (exportPath, typeof(Forging));
			if (data == null) {
				data = ScriptableObject.CreateInstance<Forging> ();
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

					Forging.Sheet s = new Forging.Sheet ();
					s.name = sheetName;
				
					for (int i=2; i<= sheet.LastRowNum; i++) {
						IRow row = sheet.GetRow (i);
						ICell cell = null;
						
						Forging.Param p = new Forging.Param ();
						
                        cell = row.GetCell(0); p.id = (int)(cell == null ? 0 : cell.NumericCellValue);
                        cell = row.GetCell(1); p.forgingType = (int)(cell == null ? 0 : cell.NumericCellValue);
                        cell = row.GetCell(2); p.Preconditions = (int)(cell == null ? 0 : cell.NumericCellValue);
                        cell = row.GetCell(3); p.Material1 = (int)(cell == null ? 0 : cell.NumericCellValue);
                        cell = row.GetCell(4); p.num1 = (int)(cell == null ? 0 : cell.NumericCellValue);
                        cell = row.GetCell(5); p.Material2 = (int)(cell == null ? 0 : cell.NumericCellValue);
                        cell = row.GetCell(6); p.num2 = (int)(cell == null ? 0 : cell.NumericCellValue);
                        cell = row.GetCell(7); p.Material3 = (int)(cell == null ? 0 : cell.NumericCellValue);
                        cell = row.GetCell(8); p.num3 = (int)(cell == null ? 0 : cell.NumericCellValue);
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
