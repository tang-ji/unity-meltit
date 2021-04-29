using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.Universal;

public class ExplodableFragments : ExplodableAddon{
    public override void OnFragmentsGenerated(List<GameObject> fragments)
    {
        foreach (GameObject fragment in fragments)
        {
            Explodable fragExp = fragment.AddComponent<Explodable>();
			// ExplodeOnDoubleClick fragExpdc = fragment.AddComponent<ExplodeOnDoubleClick>();
            // fragExpdc.doubleTouchDelay = 0.8f;
            fragment.tag = "Clone";
            Light2D light = fragment.AddComponent<Light2D>();
            light.intensity = 0.01f;
            fragExp.shatterType = explodable.shatterType;
            fragExp.fragmentLayer = explodable.fragmentLayer;
            fragExp.sortingLayerName = explodable.sortingLayerName;
            fragExp.orderInLayer = explodable.orderInLayer;

            fragment.layer = explodable.gameObject.layer;

            fragExp.fragmentInEditor();
        }
    }
}
