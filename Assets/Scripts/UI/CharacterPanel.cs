using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanel : UIBase
{
    private Transform BtnList;
    private Button LoadModelBtn;
    private Button RemoveModelBtn;

    private ModelBase model;

    void Awake()
    {
        BtnList = transform.Find("ScrollView/Viewport/Content");

        transform.Find<Button>("Back").onClick.AddListener(Close);

        LoadModelBtn = BtnList.Find<Button>("LoadModel");
        LoadModelBtn.onClick.AddListener(LoadModelBtnCall);
        RemoveModelBtn = BtnList.Find<Button>("RemoveModel");
        RemoveModelBtn.onClick.AddListener(RemoveModelBtnCall);
    }

    private void LoadModelBtnCall()
    {
        if (model != null)
        {
            Debug.Log("Already Added");
            return;
        }
        // 模拟这个模型的Excel表
        string partsIDList = "1001,2001,3001,4001,5001,";
        int[] iArray = partsIDList.SplitToInt(',');
        ModelPart parts = new ModelPart(iArray);
        model = ModelManager.Instance.CreateModel(parts);
        model.SetLocalRotation(new Vector3(0f, 180f, 0f));
    }

    private void RemoveModelBtnCall()
    {
        if (model != null)
        {
            Destroy(model.gameObject);
        }
    }
}
