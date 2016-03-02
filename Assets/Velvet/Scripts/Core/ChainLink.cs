using UnityEngine;
using BlurryRoots;
using BlurryRoots.Procedural;
using System.Collections.Generic;

[System.Serializable]
public abstract class ChainLink : BlurryBehaviour, IChainLink<List<GameObject>> {

    public abstract List<GameObject> Process (List<GameObject> input);

}
