using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ModelPart
{
    public int HeadID;      // 头
    public int BodyID;      // 身
    public int HandID;      // 手
    public int FootID;      // 脚
    public int WeaponRID;   // 主武器
    public int WeaponLID;   // 副武器
    
    public ModelPart(int[] idList)
    {
        HeadID = idList[0];
        BodyID = idList[1];
        HandID = idList[2];
        FootID = idList[3];
        WeaponRID = idList[4];
        WeaponLID = idList[5];
    }

    public int[] GetValues()
    {
        int[] ids = 
        {
            HeadID, BodyID, HandID, FootID, WeaponRID, WeaponLID,
        };
        return ids;
    }
}

public class ModelBase : MonoBehaviour
{
    private ModelPart _parts;

    // 模拟Excel表
    private Dictionary<int, string> idToPath = new Dictionary<int, string>
    {
        {1001, "Equipment/Hero/aa026/md_c_aa026_head"},
        {2001, "Equipment/Hero/aa026/md_c_aa026_body"},
        {3001, "Equipment/Hero/aa026/md_c_aa026_hand"},
        {4001, "Equipment/Hero/aa026/md_c_aa026_foot"},
        {5001, "Equipment/Hero/fa0006/md_b_fa0006_weapon"},
    };

    /// <summary>
    /// 根据ModelPart结构体生成模型
    /// </summary>
    public void GenerateByModelPart(ModelPart parts)
    {
        _parts = parts;

        // 角色主体骨骼上的所有挂载点
        Transform[] jointList = GetComponentsInChildren<Transform>();
        int[] partIDList = _parts.GetValues();  // 部件ID数组
        int partCount = partIDList.Length;      // 部件数量

        SkinnedMeshRenderer[] meshList = new SkinnedMeshRenderer[partCount];    // 部件Mesh
        GameObject[] partGOList = new GameObject[partCount];                    // 部件GameObject

        // 收集Mesh
        for (int i = 0; i < partCount; i++)
        {
            int partID = partIDList[i];
            if (partID == 0)
            {
                continue;
            }
            GameObject tempRes = Resources.Load<GameObject>(idToPath[partID]);
            partGOList[i] = Instantiate(tempRes);
            meshList[i] = partGOList[i].GetComponentInChildren<SkinnedMeshRenderer>();

            // 处理装备上的粒子特效
            Dictionary<GameObject, string> particleMap = FindParticles(partGOList[i]);
            foreach (var particlePair in particleMap)
            {
                Transform particleRoot = particlePair.Key.transform;
                string jointName = particlePair.Value;
                foreach (var joint in jointList)
                {
                    if (joint.name == jointName)
                    {
                        particleRoot.SetParent(joint, false);
                    }
                }
            }
        }
        // 合并Mesh
        CombineSkinnedMgr.Instance.CombineObject(gameObject, meshList, true);

        // 删除老部件
        foreach (GameObject partGO in partGOList)
        {
            if (partGO != null)
            {
                DestroyImmediate(partGO);
            }
        }
    }

    /// <summary>
    /// 设置坐标（local）
    /// </summary>
    public void SetLocalPosition(Vector3 pos)
    {
        transform.localPosition = pos;
    }

    /// <summary>
    /// 设置旋转（local/欧拉角）
    /// </summary>
    public void SetLocalRotation(Vector3 rotation)
    {
        transform.localEulerAngles = rotation;
    }

    /// <summary>
    /// 设置旋转（local/四元数）
    /// </summary>
    public void SetLocalRotation(Quaternion quaternion)
    {
        transform.localRotation = quaternion;
    }

    /// <summary>
    /// 查找root节点下的例子特效根节点，例子特效根节点命名格式为：fx_
    /// 返回一个字典，Key为查找到的根节点，Value为挂载在模型上的transform的名称
    /// </summary>
    Dictionary<GameObject, string> FindParticles(GameObject root)
    {
        Dictionary<GameObject, string> particleRootMap = new Dictionary<GameObject, string>();
        Transform[] allChildren = root.transform.GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            if (child.name.Contains("fx"))
            {
                particleRootMap.Add(child.gameObject, child.parent.name);
            }
        }
        return particleRootMap;
    }
}
