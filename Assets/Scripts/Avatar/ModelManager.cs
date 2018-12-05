using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelManager : Singleton<ModelManager>
{
    // 模型部件
    enum PartIdx
    {
        WeaponR,
        WeaponL,
        Head,
        Body,
        Hand,
        Foot,
    }

    public void CreateModel(int skeletonID, int[] partIDList)
    {

    }
}
