using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class OpenMesh : MonoBehaviour
{
    public GameObject model;
    // Start is called before the first frame update

    public Vector3[] StrToV3Arr(string arrStr,string splitStr)
    {
        string[] v3StrASrr = arrStr.Split(splitStr);
        Vector3[] result = new Vector3[v3StrASrr.Length];
        for(int i = 0; i < v3StrASrr.Length; i++)
        {
            string[] valueStr = v3StrASrr[i].Split(' ');
            if(valueStr.Length != 3)
            {
                Debug.Log("Expected 3 component but got " + valueStr.Length);
            }
            result[i] = new Vector3(float.Parse(valueStr[0]), float.Parse(valueStr[1]), float.Parse(valueStr[2]));
           
        }
        return result;
    }
    public int[] StrToIntArr(string arrStr)
    {
        string[] intStrArr = arrStr.Split(' ');
        int[] result = new int[intStrArr.Length];
        for(int i = 0; i < intStrArr.Length; i++)
        {
            result[i] = int.Parse(intStrArr[i]);
        }
        return result;
    }
    public Color StrToColour(string colourStr)
    {
        Color resultColour = new Color();
        string[] colourStrArr = colourStr.Split(' ');
        foreach(char i in colourStr)
        {
            Debug.Log("TAG COLOR: " + i);
        }
        if(colourStr.Length != 4)
        {
            Debug.Log("StrToColour count mismatch. Expected 4 components but got " + colourStrArr.Length);
            resultColour.r = Color.white.r;
            resultColour.g = Color.white.g;
            resultColour.b = Color.white.b;
            resultColour.a = Color.white.a;
        }
        else
        {
            resultColour.r = float.Parse(colourStrArr[0]);
            resultColour.g = float.Parse(colourStrArr[1]);
            resultColour.b = float.Parse(colourStrArr[2]);
            resultColour.a = float.Parse(colourStrArr[3]);
        }
        return resultColour;
    }
    public void OnClickOpen()
    {
        string path = Application.persistentDataPath + "/model.dabab";
     //   string path = "C:/Users/Admin/Desktop/model.dabab";
        Debug.Log("OPEN FILE" +Application.persistentDataPath);
        if (File.Exists(path)){ 
            if (model != null)
            {
                foreach(Transform child in model.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }
            } 
          model =  StrToModel(File.ReadAllText(path), "mo|");
          var gameObject = Instantiate(model);
          gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("TAG file is EMPTY");
        }
    }
    public GameObject StrToModel(string modelStr,string splitStr)
    {
        GameObject model = new GameObject();
        model.SetActive(false);
        string[] modelStrArr = modelStr.Split(splitStr);
        for(int i = 0; i < modelStrArr.Length; i++)
        {
            GameObject resultObj = StrToGameObjectMesh(modelStrArr[i], "m|");
            resultObj.transform.parent = model.transform;
        }
      //  model.transform.localScale = new Vector3(1, 1, 1);
        return model;
    }
    public GameObject StrToGameObjectMesh(string meshStr, string splitStr)
    {
        GameObject meshObj = new GameObject();
        Mesh resultMesh = new Mesh();
        resultMesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        Color resultColor = new Color();
        string[] meshStrArr = meshStr.Split(splitStr);
        if (meshStrArr.Length != 6)
        {
            Debug.Log("StrToGameObjecMesh Waring. Number of element in string array is: " + meshStrArr.Length + ". No mesh found!");
        }
        else
        {
            Vector3[] resultVertices = StrToV3Arr(meshStrArr[0], "v|");
            Vector3[] resultNormal = StrToV3Arr(meshStrArr[1], "n|");
            int[] resultTriangles = StrToIntArr(meshStrArr[2]);
            int[] resultIndices = StrToIntArr(meshStrArr[3]);
            string meshTopoStr = meshStrArr[4];
            resultColor = StrToColour(meshStrArr[5]);
            resultMesh.vertices = resultVertices;
            resultMesh.normals = resultNormal;
            resultMesh.triangles = resultTriangles;
            if (meshTopoStr == "Lines")
            {
                resultMesh.SetIndices(resultIndices, MeshTopology.Lines, 0);
            }
            else
            {
                resultMesh.SetIndices(resultIndices, MeshTopology.Triangles, 0);
            }
        }
            meshObj.AddComponent<MeshFilter>();
            meshObj.AddComponent<MeshRenderer>();
            meshObj.GetComponent<MeshFilter>().mesh = resultMesh;
            meshObj.GetComponent<MeshRenderer>().material.color = resultColor;
            return meshObj; 
  
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
