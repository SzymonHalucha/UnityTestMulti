using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class SOArchitectureGeneratorWindow : EditorWindow
{
    private static readonly string[] NUMERIC_OPERATORS = { "op_Addition", "op_Subtraction", "op_Multiply", "op_Division" };

    private class Template
    {
        public Template(string editorName, string name, string body, Func<Type, bool> canGenerate)
        {
            EditorName = editorName;
            Name = name;
            Body = body;
            CanGenerate = canGenerate;
        }

        public Template(string editorName, string name, string body) : this(editorName, name, body, (x) => true)
        {

        }

        public string EditorName { private set; get; }
        public string Name { private set; get; }
        public string Body { private set; get; }
        public bool IsGenerating { get; set; }
        public Func<Type, bool> CanGenerate { private set; get; }
    }

    private string path = "Scripts/SO Architecture/";
    private string typeName = "";

    private readonly Template[] templates =
    {
        new Template("List", "List", "[UnityEngine.CreateAssetMenu(menuName = SOArchitectureDirectories.LIST_COLLECTION + \"%NAME%\")]\npublic class %NAME%List : ListCollection<%NAME%>\n{\n\n}"),
        new Template("Stack", "Stack", "[UnityEngine.CreateAssetMenu(menuName = SOArchitectureDirectories.STACK_COLLECTION + \"%NAME%\")]\npublic class %NAME%Stack : StackCollection<%NAME%>\n{\n\n}"),
        new Template("VariableReference", "VariableReference", "public class %NAME%VariableReference : VariableReference<%NAME%>\n{\n\n}"),
        new Template("ListReference", "ListReference", "public class %NAME%ListReference : ListReference<%NAME%>\n{\n\n}"),
        new Template("VariableInstance", "VariableInstance", "public class %NAME%VariableInstance : VariableInstance<%NAME%>\n{\n\n}"),
        new Template("ListInstance", "ListInstance", "public class %NAME%ListInstance : ListInstance<%NAME%>\n{\n\n}"),

        new Template("Variable - Serialize Field", "Variable", "[UnityEngine.CreateAssetMenu(menuName = SOArchitectureDirectories.VARIABLE_SUBMENU + \"%NAME%\")]\npublic class %NAME%Variable : SerializeFieldClassVariable<%NAME%>\n{\n\n}", (x) => !x.IsValueType && !x.IsEnum && !x.IsInterface),
        new Template("Variable - Serialize Reference", "Variable", "[UnityEngine.CreateAssetMenu(menuName = SOArchitectureDirectories.VARIABLE_SUBMENU + \"%NAME%\")]\npublic class %NAME%Variable : SerializeReferenceClassVariable<%NAME%>\n{\n\n}", (x) => !x.IsValueType && !x.IsEnum && x.IsAbstract),
        new Template("Variable - Unity Object Interface", "Variable", "[UnityEngine.CreateAssetMenu(menuName = SOArchitectureDirectories.VARIABLE_SUBMENU + \"%NAME%\")]\npublic class %NAME%Variable : UnityObjectInterfaceVariable<%NAME%>\n{\n\n}", (x) => x.IsInterface),
        new Template("Variable - Struct", "Variable","[UnityEngine.CreateAssetMenu(menuName = SOArchitectureDirectories.VARIABLE_SUBMENU + \"%NAME%\")]\npublic class %NAME%Variable : StructVariable<%NAME%>\n{\n\n}", (x) => x.IsValueType && !x.IsEnum && NUMERIC_OPERATORS.Where(op => x.GetMethods().Where(x => x.Name == op).Count() > 0).Count() != 4),
        new Template("Variable - Numeric Struct", "Variable", "[UnityEngine.CreateAssetMenu(menuName = SOArchitectureDirectories.VARIABLE_SUBMENU + \"%NAME%\")]\npublic class %NAME%Variable : NumericStructVariable<%NAME%>\n{\n\n}", (x) => x.IsValueType && !x.IsEnum && NUMERIC_OPERATORS.Where(op => x.GetMethods().Where(x => x.Name == op).Count() > 0).Count() == 4),

        new Template("GameEvent", "GameEvent", "[UnityEngine.CreateAssetMenu(menuName = SOArchitectureDirectories.TYPE_GAME_EVENT + \"%NAME%\")]\npublic class %NAME%GameEvent : TypeGameEvent1<%NAME%>\n{\n\n}"),
        new Template("EventListener", "EventListener", "public class %NAME%EventListener : TypeEventListener1<%NAME%>\n{\n\n}")
    };

    [MenuItem("Tools/SO Architecture Generator")]
    private static void OpenWindow()
    {
        var window = GetWindow<SOArchitectureGeneratorWindow>();
        window.minSize = new Vector2(300f, 200f);
        window.Show();
    }

    private void OnGUI()
    {
        path = EditorGUILayout.TextField("Path", path);

        EditorGUI.BeginChangeCheck();
        typeName = EditorGUILayout.TextField("Class name", typeName);
        if (EditorGUI.EndChangeCheck())
            foreach (var item in templates)
                item.IsGenerating = false;

        var type = GetType(typeName);

        foreach (var item in templates)
        {
            var toBeGenerated = GetType(typeName + item.Name);
            EditorGUI.BeginDisabledGroup(!(!(type == null || toBeGenerated != null) && item.CanGenerate.Invoke(type)));
            item.IsGenerating = EditorGUILayout.Toggle(item.EditorName, item.IsGenerating);
            EditorGUI.EndDisabledGroup();
        }

        if (type != null && GUILayout.Button("Build Object"))
            for (int i = 0; i < templates.Length; i++)
                if (templates[i].IsGenerating)
                    Generate(i);
    }

    private Type GetType(string type)
    {
        if (string.IsNullOrEmpty(type))
            return null;

        foreach (var item in AppDomain.CurrentDomain.GetAssemblies())
            if (item.GetType(type) != null)
                return item.GetType(type);

        return null;
    }

    private void Generate(int index)
    {
        string fullPath = Application.dataPath + "/" + path + templates[index].Name + "/" + typeName + templates[index].Name + ".cs";
        Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
        File.WriteAllText(fullPath, templates[index].Body.Replace("%NAME%", typeName));
    }
}