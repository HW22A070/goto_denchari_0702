using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class PlayerC : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Droped(int itemNumber)
    {
        Action(itemNumber);
    }

    public List<UnityEvent> OnEvent;

    public void Action(int id)
    {
        OnEvent[id].Invoke();
    }

}

#if UNITY_EDITOR
[ExecuteAlways]
[CustomEditor(typeof(PlayerC))]
public class MethodCallerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PlayerC t = target as PlayerC;
        if (t == null) return;
        if (t.OnEvent == null) return;
        if (t.OnEvent.Count > 1)
        {
            for (int i = 0; i < t.OnEvent.Count; i++)
            {
                if (t.OnEvent[i].GetPersistentEventCount() > 0 && t.OnEvent[i].GetPersistentMethodName(0).Length > 0)
                {
                    if (GUILayout.Button(t.OnEvent[i].GetPersistentMethodName(0)))
                    {
                        t.Action(i);
                    }
                }
            }
        }
    }
}
#endif


