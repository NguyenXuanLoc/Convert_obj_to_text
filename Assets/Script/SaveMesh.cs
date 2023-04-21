using System.IO;
using System.Text;
using UnityEngine;

public class SaveMesh : MonoBehaviour
{
    public GameObject model;
    // Start is called before the first frame update
    void Start()
    {

    }

    public string V3ArrToStr(Vector3[] v3Arr, string splitStr)
    {
        StringBuilder sb = new StringBuilder();
        foreach (Vector3 v3 in v3Arr)
        {
            sb.Append(v3.x).Append(" ").Append(v3.y).Append(" ").Append(v3.z).Append(splitStr);
        }
        if (sb.Length > 0)
        {
            sb.Remove(sb.Length - splitStr.Length, splitStr.Length);
        }
        return sb.ToString();
    }
    public string intArrToStr(int[] intArr)
    {
        StringBuilder sb = new StringBuilder();
        foreach (int i in intArr)
        {
            sb.Append(i).Append(" ");
        }
        if (sb.Length > 0)
        {
            sb.Remove(sb.Length - 1, 1);
        }
        return sb.ToString();
    }

    public string ColourToStr(Color colour)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(colour.r).Append(" ").Append(colour.g).Append(" ").Append(colour.b).Append(" ").Append(colour.a);
        return sb.ToString();
    }
    public void OnClickSave()
    { 
        File.WriteAllText(Application.persistentDataPath + "/model.dabab", ModelToStr(ref model, "mo|"));
        Debug.Log(Application.persistentDataPath);
    }

    public string ModelToStr(ref GameObject model,string splitStr)
    {
        StringBuilder sb = new StringBuilder();
        for(int i = 0; i < model.GetComponentsInChildren<Renderer>().Length; i++)
        {
            Mesh mesh = model.GetComponentsInChildren<MeshFilter>()[i].mesh;
            Material material = model.GetComponentsInChildren<MeshRenderer>()[i].material;
            sb.Append(MeshToStr(mesh, material.color, "m|")).Append(splitStr);
        }
        if(sb.Length > 0)
        {
            sb.Remove(sb.Length - splitStr.Length, splitStr.Length);
        }
        return sb.ToString();
    }

    public string MeshToStr(Mesh mesh,Color meshColour,string splitStr)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(V3ArrToStr(mesh.vertices, "v|")).Append(splitStr).Append(V3ArrToStr(mesh.normals, "n|")).Append(splitStr).Append(intArrToStr(mesh.triangles)).
            Append(splitStr).Append(intArrToStr(mesh.GetIndices(0))).Append(splitStr).Append(mesh.GetTopology(0)).Append(splitStr).Append(ColourToStr(meshColour));
        return sb.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
