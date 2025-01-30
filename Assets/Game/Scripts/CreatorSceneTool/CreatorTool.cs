using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Collections;

#if(UNITY_EDITOR)
public class CreatorTool : EditorWindow
{
    private GameObject objetoAPoner;
    private int layerIndex;
    //Button OnOff
    private bool OnOff;
    private string turnOnOff_Btn_Txt = "OFF";
    private GUIStyle clearAll_Btn_Style;
    //Button Mode
    private bool eraseMode;
    private GUIStyle eraseMode_Btn_Style;
    private GUIStyle createMode_Btn_Style;


    private List<GameObject> boxes = new List<GameObject>();
    private LevelManager lvlManager;

    [MenuItem("Window/CreatorTool")]
    public static void ShowWindow()
    {
        GetWindow<CreatorTool>("CreatorToolScene");
    }

    private void OnGUI()
    {
        //Button OnOff
        clearAll_Btn_Style = new GUIStyle(GUI.skin.button);
        clearAll_Btn_Style.normal.textColor = OnOff ? Color.green : Color.red;
        clearAll_Btn_Style.fixedHeight = 50;
        if (GUILayout.Button(turnOnOff_Btn_Txt, clearAll_Btn_Style))
        {
            turnOnOff_Btn_Txt = OnOff ? "OFF" : "ON";
            OnOff = !OnOff;
            Init();
            SceneView.lastActiveSceneView.in2DMode = true; //Pone el Editor en 2dMOde
            SceneView.lastActiveSceneView.showGrid = true; //Prende la grilla
        }

        // Variables
        objetoAPoner = (GameObject)EditorGUILayout.ObjectField("GameObject a poner: ", objetoAPoner, typeof(GameObject), true);
        layerIndex = (int)EditorGUILayout.IntField("Layer Index: ",layerIndex);

        //ClearAll Button
        EditorGUILayout.Space(10);
        if (GUILayout.Button("Clear All"))
        {
            foreach (GameObject obj in boxes) {DestroyImmediate(obj); }
            boxes.Clear();
            lvlManager.ClearAllBoxInLayer();
        }


        //Erasemode and Create Button
        EditorGUILayout.Space(10);
        EditorGUILayout.BeginVertical();
        eraseMode_Btn_Style = new GUIStyle(GUI.skin.button);
        eraseMode_Btn_Style.fixedHeight = 25;
        eraseMode_Btn_Style.fixedWidth = 100;
        eraseMode_Btn_Style.normal.textColor = eraseMode ? Color.green : Color.red;
        EditorGUILayout.Space(10);
        if (GUILayout.Button("Erase Mode", eraseMode_Btn_Style))
        {
            eraseMode = true;
        }
        createMode_Btn_Style = new GUIStyle(GUI.skin.button);
        createMode_Btn_Style.fixedHeight = 25;
        createMode_Btn_Style.fixedWidth = 100;
        createMode_Btn_Style.normal.textColor = eraseMode ? Color.red:Color.green;
        if (GUILayout.Button("Create Mode", createMode_Btn_Style))
        {
            eraseMode = false;
        }
        EditorGUILayout.EndVertical();
        
        EditorGUILayout.Space(20);
        if (GUILayout.Button("New LvlManager",GUILayout.Width(120),GUILayout.Height(25)))
        {
            CreateNewLvlManager();
        }

    }
    private void OnEnable()
    {
        SceneView.duringSceneGui += OnSceneGUI;
        Init();
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }

    void Init()
    {
        if (OnOff) return;
        
        lvlManager = FindAnyObjectByType<LevelManager>(); //Busca algun objeto en escena que tenga el CReatorToolScene y si no hay lo crea.
        
        if (lvlManager == null)
            CreateNewLvlManager();
    }

    private void CreateNewLvlManager()
    {
        lvlManager = new GameObject().AddComponent<LevelManager>();
        lvlManager.gameObject.name = "LevelManager";
        lvlManager.InitToCreatorTool();
    }

    private void OnSceneGUI(SceneView sceneView)
    {
        if (!OnOff) return;
           
        Event e = Event.current;
        if (e.type == EventType.MouseDown && e.button == 0)
        {
            GUIUtility.hotControl = 1; //Hace mientras estemos tocando el boton del mouse, la herramienta de agarrar no se active.
            if (!eraseMode) InstantiateObjectOnScene();
            else EraseObjectOnScene();
        }
        else GUIUtility.hotControl = 0;

    }

    private void InstantiateObjectOnScene()
    {
        //Toma la posicion de mouse en la ventana del Editor
        Vector3 mousePosition = Event.current.mousePosition;
        Ray ray = HandleUtility.GUIPointToWorldRay(mousePosition);
        Vector3 pos = ray.origin;
        
        //Verifica si hay otro cubo en el mismo lugar para no ponerlo excepto que se enceuntre en otro layer.
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
            if (hit.transform.position.z == layerIndex) return;
        

        //Intancia una box en el lugar donde este el mouse
        if (objetoAPoner != null)
        {
            GameObject clon = Instantiate(objetoAPoner, SnapPosOnGrid(pos), Quaternion.identity);
            boxes.Add(clon);
            lvlManager.AddNewBoxInLayer(layerIndex, clon);
        }
           
    }

    private void EraseObjectOnScene()
    {
        //Toma la posicion de mouse en la ventana del Editor
        Vector3 mousePosition = Event.current.mousePosition;
        Ray ray = HandleUtility.GUIPointToWorldRay(mousePosition);
        Vector3 pos = ray.origin;

        //Verifica si hay otro cubo en el mismo lugar para no ponerlo excepto que se enceuntre en otro layer.
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.position.z != layerIndex) return;
            GameObject box = hit.transform.gameObject;
            boxes.Remove(box);
            lvlManager.RemoveBox(box.GetComponent<Box>());
            DestroyImmediate(box);  
        }

    }

    private Vector3 SnapPosOnGrid(Vector3 pos) //Spanea la posicion para que este prolijos
    {
        Vector3 posSnapped = new Vector3(
                Mathf.RoundToInt(pos.x ),
                Mathf.RoundToInt(pos.y ),
                layerIndex
            );

        return posSnapped;
    }

}
#endif  