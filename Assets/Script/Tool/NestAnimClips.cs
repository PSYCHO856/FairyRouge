using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

/// <summary>
/// Created on: 2020-9-1
/// Author: D.xy
/// </summary>
public class NestAnimClips : MonoBehaviour {
    [MenuItem ("Assets/Anime/Nest Clips in Controller")]
    public static void nestAnimClips () {
        AnimatorController ctrl = Selection.activeObject as AnimatorController;

        if (null == ctrl) {
            return;
        }

        AnimationClip[] clips = ctrl.animationClips;

        for (int i = 0; i < clips.Length; ++i) {
            AnimationClip clip = clips[i];
            var assetPath = AssetDatabase.GetAssetPath (clip);
            if (assetPath.EndsWith (".anim")) {
                AnimationClip newClip = Object.Instantiate (clip);
                newClip.name = clip.name;
                AssetDatabase.AddObjectToAsset (newClip, ctrl);
                AssetDatabase.DeleteAsset (AssetDatabase.GetAssetPath (clip));
                replaceStateMotion (ctrl, clip, newClip);
            }
        }
        AssetDatabase.SaveAssets ();
    }

    [MenuItem ("Assets/Anime/Remove Nest Clip")]
    public static void removeNestAnimClip () {
        AnimationClip clip = Selection.activeObject as AnimationClip;
        if (clip == null) {
            return;
        }
        string ctrlPath = AssetDatabase.GetAssetPath (clip);
        // 保证只移除嵌套的clip，否则会导致动画状态错乱
        if (!ctrlPath.EndsWith (".controller")) {
            return;
        }

        AssetDatabase.RemoveObjectFromAsset (clip);
        Object.DestroyImmediate (clip);
        AssetDatabase.SaveAssets ();
    }

    /// <summary>
    /// 替换动画控制器中使用的Clip
    /// </summary>
    public static void replaceStateMotion (AnimatorController ctrl, AnimationClip oldClip, AnimationClip newClip, bool replaceAll = false) {
        var e = ctrl.layers.GetEnumerator ();
        while (e.MoveNext ()) {
            AnimatorControllerLayer layer = (AnimatorControllerLayer) e.Current;
            var child = layer.stateMachine.states.GetEnumerator ();
            while (child.MoveNext ()) {
                ChildAnimatorState childState = (ChildAnimatorState) child.Current;
                if (childState.state.motion == oldClip) {
                    ctrl.SetStateEffectiveMotion (childState.state, newClip);
                    if (!replaceAll) {
                        return;
                    }
                }
            }
        }
    }
}