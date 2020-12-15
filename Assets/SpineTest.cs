using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class SpineTest : MonoBehaviour {
    [SpineEvent] public string footstepEventName = "footstep";
    // Start is called before the first frame update
    void Start () {
        var skeletonAnimation = GetComponent<SkeletonAnimation> ();

        if (skeletonAnimation == null) {
            return;
        }

        skeletonAnimation.state.Event += HandleEvent;

        skeletonAnimation.state.Start += delegate (Spine.TrackEntry state) {
            
        };

        skeletonAnimation.state.End += delegate {
            Debug.Log ("An animation ended!");
        };
    }

    // Update is called once per frame
    void Update () {

    }

    public void HandleEvent (Spine.TrackEntry trackEntry, Spine.Event e) {
        if (e.Data.Name == footstepEventName) { }
    }
}