using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

public class RoleInformation_Importer : AssetPostprocessor {
	private static readonly string filePath = "Assets/ReadExcel/Config/Excel/角色信息.xlsx";
	private static readonly string exportPath = "Assets/ReadExcel/Config/Resources/DataConfig/RoleInformation"+ ".asset";
	private static readonly string[] sheetNames = { "Sheet1", };
	
	static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		foreach (string asset in importedAssets) {
			if (!filePath.Equals (asset))
				continue;
			
			Directory.CreateDirectory("Assets/ReadExcel/Config/Resources/DataConfig/ ");
			RoleInformation data = (RoleInformation)AssetDatabase.LoadAssetAtPath (exportPath, typeof(RoleInformation));
			if (data == null) {
				data = ScriptableObject.CreateInstance<RoleInformation> ();
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

					RoleInformation.Sheet s = new RoleInformation.Sheet ();
					s.name = sheetName;
				
					for (int i=2; i<= sheet.LastRowNum; i++) {
						IRow row = sheet.GetRow (i);
						ICell cell = null;
						
						RoleInformation.Param p = new RoleInformation.Param ();
						
                        cell = row.GetCell(0); p.id = (int)(cell == null ? 0 : cell.NumericCellValue);
                        cell = row.GetCell(1); p.name = (cell == null ? "" : cell.StringCellValue);
                        cell = row.GetCell(2); p.sex = (cell == null ? "" : cell.StringCellValue);
                        cell = row.GetCell(3); p.age = (int)(cell == null ? 0 : cell.NumericCellValue);
                        cell = row.GetCell(4); p.occupationID = (int)(cell == null ? 0 : cell.NumericCellValue);
                        cell = row.GetCell(5); p.height = (cell == null ? "" : cell.StringCellValue);
                        cell = row.GetCell(6); p.weight = (cell == null ? "" : cell.StringCellValue);
                        cell = row.GetCell(7); p.skillID1 = (int)(cell == null ? 0 : cell.NumericCellValue);
                        cell = row.GetCell(8); p.skillID2 = (int)(cell == null ? 0 : cell.NumericCellValue);
                        cell = row.GetCell(9); p.MentalID = (int)(cell == null ? 0 : cell.NumericCellValue);
                        cell = row.GetCell(10); p.describe = (cell == null ? "" : cell.StringCellValue);
                        cell = row.GetCell(11); p.title = (int)(cell == null ? 0 : cell.NumericCellValue);
                        cell = row.GetCell(12); p.Level = (int)(cell == null ? 0 : cell.NumericCellValue);
                        cell = row.GetCell(13); p.Fetters = (int)(cell == null ? 0 : cell.NumericCellValue);
                        cell = row.GetCell(14); p.EquipmentID = (int)(cell == null ? 0 : cell.NumericCellValue);
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
