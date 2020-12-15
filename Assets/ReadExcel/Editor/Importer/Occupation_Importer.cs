using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

public class Occupation_Importer : AssetPostprocessor {
	private static readonly string filePath = "Assets/ReadExcel/Config/Excel/职业.xlsx";
	private static readonly string exportPath = "Assets/ReadExcel/Config/Resources/DataConfig/Occupation"+ ".asset";
	private static readonly string[] sheetNames = { "Occupation", };
	
	static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		foreach (string asset in importedAssets) {
			if (!filePath.Equals (asset))
				continue;
			
			Directory.CreateDirectory("Assets/ReadExcel/Config/Resources/DataConfig/ ");
			Occupation data = (Occupation)AssetDatabase.LoadAssetAtPath (exportPath, typeof(Occupation));
			if (data == null) {
				data = ScriptableObject.CreateInstance<Occupation> ();
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

					Occupation.Sheet s = new Occupation.Sheet ();
					s.name = sheetName;
				
					for (int i=2; i<= sheet.LastRowNum; i++) {
						IRow row = sheet.GetRow (i);
						ICell cell = null;
						
						Occupation.Param p = new Occupation.Param ();
						
                        cell = row.GetCell(0); p.id = (int)(cell == null ? 0 : cell.NumericCellValue);
                        cell = row.GetCell(1); p.OccupationName = (cell == null ? "" : cell.StringCellValue);
                        cell = row.GetCell(2); p.featuresID1 = (int)(cell == null ? 0 : cell.NumericCellValue);
                        cell = row.GetCell(3); p.featuresID2 = (int)(cell == null ? 0 : cell.NumericCellValue);
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
