using UnityEngine;
using Unity.GraphToolkit.Editor;
using UnityEditor;
[SerializeField]
[Graph(AssetExtention)]
public class DialogueGraph : Graph
{
    public const string AssetExtention = "dialoguegraph"; //file extention, file will be called .dialoguegraph

    [MenuItem("Assets/Create/Dialogue Graph", false)] //menu attribute 

    private static void CreateAssetFile()
    {
        GraphDatabase.PromptInProjectBrowserToCreateNewAsset<DialogueGraph>(); //This line creates the graph asset
    }


}
