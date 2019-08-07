using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;
using UnityEngine.UIElements;

public class CreateNPCEditorWindow : EditorWindow
{
    GameObject go = null;

    string NPCName;
    string NPCLevel;
    string NPCExpMin;
    string NPCExpMax;
    string NPCStartHealth;
    string NPCHealth;
    string NPCAttackMin;
    string NPCAttackMax;
    string NPCStopDistance;
    string NPCAct;
    NPC.NPCTypes NPCType;

    [MenuItem("Window/Create a New NPC")]

    public static void ShowWindow()
    {
        GetWindow<CreateNPCEditorWindow>("Create New NPC");
    }

    void OnGUI()
    {
        Repaint();

        if (Selection.activeGameObject == null)
        {
            GUILayout.BeginHorizontal("box");
            GUILayout.Label("Select a 3D model or Gameobject.", EditorStyles.centeredGreyMiniLabel); // Title
            GUILayout.EndHorizontal();
            return;
        }

        GUI.color = Color.cyan;

        GUILayout.BeginHorizontal("box");
        GUILayout.FlexibleSpace();
        GUILayout.Label("Create a New NPC", EditorStyles.whiteBoldLabel); // Title
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUI.color = Color.green;

        GUILayout.BeginHorizontal("box");
        NPCType = (NPC.NPCTypes)EditorGUILayout.EnumPopup("Race", NPCType);
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUI.color = Color.white;

        GUILayout.BeginHorizontal("box");
        NPCName = EditorGUILayout.TextField("Name", NPCName); // Choose Enemy Name
        go = Selection.activeGameObject;
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal("box");
        NPCLevel = EditorGUILayout.TextField("Level", NPCLevel); // Choose Enemy Level
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal("box");
        NPCExpMin = EditorGUILayout.TextField("EXP Minimum", NPCExpMin); // Choose Enemy Minimum EXP Gained
        NPCExpMax = EditorGUILayout.TextField("EXP Maximum", NPCExpMax); // Choose Enemy Maximum EXP Gained
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal("box");
        NPCStartHealth = EditorGUILayout.TextField("Starting Health", NPCStartHealth); // Choose Enemy Name
        NPCHealth = EditorGUILayout.TextField("Health", NPCHealth); // Choose Enemy Name
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal("box");
        NPCAttackMin = EditorGUILayout.TextField("Attack Damage Minimum", NPCAttackMin); // Choose Enemy Attack Damage 5 REFERS to 5 HP
        NPCAttackMax = EditorGUILayout.TextField("Attack Damage Maximum", NPCAttackMax); // Choose Enemy Attack Damage 5 REFERS to 5 HP
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal("box");
        NPCStopDistance = NPCStopDistance = EditorGUILayout.TextField("Distance To Stop", NPCStopDistance); // Distance to Player for NavMeshAgent to HALT
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal("box");
        NPCAct = EditorGUILayout.TextField("Act Number", NPCAct); // Monster is an ACT 01 monster (Diablo style of Acts)
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Confirm Creation"))
        {
            go.AddComponent<Animator>();

            NavMeshAgent agent = go.AddComponent<NavMeshAgent>();
            agent.speed = 1; agent.angularSpeed = 999; agent.acceleration = 999; agent.stoppingDistance = int.Parse(NPCStopDistance);

            CapsuleCollider col = go.AddComponent<CapsuleCollider>();
            col.isTrigger = true;

            NPCObject NPC = go.AddComponent<NPCObject>();

            NPC newNPC = ScriptableObject.CreateInstance<NPC>();
            AssetDatabase.CreateAsset(newNPC, "Assets/Objects/" + NPCName + ".asset");
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            NPC.NPCType = newNPC;

            Selection.activeObject = NPC;
            Selection.activeObject.name = NPCName;

            CreateObject(newNPC);
        }
    }

    public void CreateObject(NPC newNPC)
    {
        newNPC.NPC_Name = NPCName;
        newNPC.NPC_Type = NPCType;
        newNPC.NPC_Level = int.Parse(NPCLevel);
        newNPC.NPC_ExpMin = int.Parse(NPCExpMin);
        newNPC.NPC_ExpMax = int.Parse(NPCExpMax);
        newNPC.NPC_StartHealth = int.Parse(NPCStartHealth);
        newNPC.NPCHealth = int.Parse(NPCHealth);
        newNPC.NPC_AttackDmgMin = int.Parse(NPCAttackMin);
        newNPC.NPC_AttackDmgMin = int.Parse(NPCAttackMax);
        newNPC.NPC_StopDistance = int.Parse(NPCStopDistance);
        newNPC.NPC_ActID = int.Parse(NPCAct);

        string localPath = null;
        string TypeOfNPC = newNPC.NPC_Type.ToString();

        if (TypeOfNPC == "Human")
            localPath = "Assets/Prefabs/AI/Humans/" + NPCName + ".prefab";
        else if (TypeOfNPC == "Monster")
            localPath = "Assets/Prefabs/AI/Monsters/" + NPCName + ".prefab";
        else if (TypeOfNPC == "Alien")
            localPath = "Assets/Prefabs/AI/Aliens/" + NPCName + ".prefab";
        else if (TypeOfNPC == "Demon")
            localPath = "Assets/Prefabs/AI/Demons/" + NPCName + ".prefab";

        AssetDatabase.GenerateUniqueAssetPath(localPath);
        PrefabUtility.SaveAsPrefabAssetAndConnect(go, localPath, InteractionMode.UserAction);
    }
}
