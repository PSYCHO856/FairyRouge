using UnityEngine;

public class ExcelDataInit
{
    public static bool isInit;
    public static void Init()
    {
        if(isInit)
        {
            return;
        }
        isInit=true;
		 Resources.Load<Effect>("DataConfig/Effect").SetDic();
 Resources.Load<Equipment>("DataConfig/Equipment").SetDic();
 Resources.Load<Forging>("DataConfig/Forging").SetDic();
 Resources.Load<Occupation>("DataConfig/Occupation").SetDic();
 Resources.Load<OccupationFeature>("DataConfig/OccupationFeature").SetDic();
 Resources.Load<Roleattributes>("DataConfig/Roleattributes").SetDic();
 Resources.Load<RoleInformation>("DataConfig/RoleInformation").SetDic();
 Resources.Load<Skill>("DataConfig/Skill").SetDic();
 Resources.Load<Talent>("DataConfig/Talent").SetDic();

        Resources.UnloadUnusedAssets();
    }
}
