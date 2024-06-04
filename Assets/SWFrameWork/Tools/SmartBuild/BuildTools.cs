#if UNITY_EDITOR
using System;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;

namespace SWFrameWork.Tools.Build
{

    public class OptionsWindowOdin : OdinEditorWindow
    {
        private static readonly ValueDropdownList<BuildTarget> BuildTargets = new ValueDropdownList<BuildTarget>()
        {
            { "Standalone Windows", BuildTarget.StandaloneWindows },
            { "Standalone Windows 64", BuildTarget.StandaloneWindows64 },
            { "Android", BuildTarget.Android },
            { "iOS", BuildTarget.iOS },
            { "WebGL", BuildTarget.WebGL }
        };
        
        [Flags]
        public enum CommonBuildOptions
        {
            None = 0,
            Development = 1,
            AutoRunPlayer = 2,
            ShowBuiltPlayer = 4,
            BuildAdditionalStreamedScenes = 8,
            AcceptExternalModificationsToPlayer = 16,
            InstallInBuildFolder = 32,
            ForceEnableAssertions = 64,
            CompressWithLz4 = 128,
            StrictMode = 256
        }

        [HorizontalGroup("BuildTargetAndOutput",0.5f)]
        [ValueDropdown("BuildTargets")]
        public BuildTarget buildTarget = BuildTargets[0].Value;

        [HorizontalGroup("BuildTargetAndOutput",0.5f)]
        [FolderPath]
        public string outputPath = "Build/";
        
        [HorizontalGroup("BuildOptionsGroup",0.5f)]
        [BoxGroup("BuildOptionsGroup/Options")]
        [ToggleLeft]
        public bool Development;
        
        [BoxGroup("BuildOptionsGroup/Options")]
        [ToggleLeft]
        public bool AutoRunPlayer;
        
        [BoxGroup("BuildOptionsGroup/Options")]
        [ToggleLeft]
        public bool ShowBuiltPlayer;
        
        // [BoxGroup("BuildOptionsGroup/Options")]
        // [ToggleLeft]
        // public bool BuildAdditionalStreamedScenes;
        //
        // [BoxGroup("BuildOptionsGroup/Options")]
        // [ToggleLeft]
        // public bool AcceptExternalModificationsToPlayer;
        //
        // [BoxGroup("BuildOptionsGroup/Options")]
        // [ToggleLeft]
        // public bool InstallInBuildFolder;
        //
        // [BoxGroup("BuildOptionsGroup/Options")]
        // [ToggleLeft]
        // public bool ForceEnableAssertions;
        
        [BoxGroup("BuildOptionsGroup/Options")]
        [ToggleLeft]
        public bool CompressWithLz4;
        
        [BoxGroup("BuildOptionsGroup/Options")]
        [ToggleLeft]
        public bool StrictMode;
        
        [HorizontalGroup("BuildOptionsGroup",0.5f)]
        [BoxGroup("BuildOptionsGroup/Scenes")]
        [ListDrawerSettings(ShowIndexLabels = true, DraggableItems = true)]
        [AssetSelector(Filter = "t:SceneAsset")]
        public SceneAsset[] scenes;
        
        
        [Button(ButtonSizes.Large)]
        public void Build()
        {
            string[] scenePaths = new string[scenes.Length];
            for (int i = 0; i < scenes.Length; i++)
            {
                scenePaths[i] = AssetDatabase.GetAssetPath(scenes[i]);
            }

            BuildOptions options = (Development ? BuildOptions.Development : BuildOptions.None)
                                   | (AutoRunPlayer ? BuildOptions.AutoRunPlayer : BuildOptions.None)
                                   | (ShowBuiltPlayer ? BuildOptions.ShowBuiltPlayer : BuildOptions.None)
                                   | (CompressWithLz4 ? BuildOptions.CompressWithLz4 : BuildOptions.None) 
                                   | (StrictMode ? BuildOptions.StrictMode : BuildOptions.None);
            BuildPipeline.BuildPlayer(scenePaths, outputPath, buildTarget, options);
        }

        [MenuItem("SWFrameWork/BuildTools")]
        private static void OpenWindow()
        {
            GetWindow<OptionsWindowOdin>().Show();
        }
    }

}
#endif
