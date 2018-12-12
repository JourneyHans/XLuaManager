using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelManager : Singleton<ModelManager>
{
    public ModelBase CreateModel(ModelPart parts)
    {
        Object obj = Resources.Load("Equipment/Hero/aa001/md_c_aa001");
        GameObject model = Object.Instantiate(obj) as GameObject;
        ModelBase modelBase = model.GetComponent<ModelBase>();
        modelBase.GenerateByModelPart(parts);
        return modelBase;
    }
}
