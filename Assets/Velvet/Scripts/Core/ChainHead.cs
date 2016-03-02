using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlurryRoots.Procedural;
using BlurryRoots.Storage;

[System.Serializable]
public class ChainHead : ChainLink, ISerializationCallbackReceiver {

    public enum ProcessingOrder {
        Pre,
        Post,
    }

    public ProcessingOrder order;

    public int repeatTimes;

    public IEnumerator<VariableBinding> GetVariableBindings () {
        return this.customVariables.GetEnumerator ();
    }

    public void ClearBindings () {
        this.customVariables.Clear ();
        this.randomizations.Clear ();
        this.serializableRandomizations.Clear ();
    }

    public override List<GameObject> Process (List<GameObject> input) {
        var result = new List<GameObject> ();

        System.Func<List<GameObject>> processingStrategy;
        switch (this.order) {
            case ProcessingOrder.Pre:
                processingStrategy = () => {
                    return this.ProcessChildChainHeads (this.ProcessOwnChain (input));
                };
                break;
            case ProcessingOrder.Post:
                processingStrategy = () => {
                    return this.ProcessOwnChain (this.ProcessChildChainHeads (input));
                };
                break;
            default:
                throw new System.Exception ("Panic! Unknown ProcessingOrder " + this.order);
        }
        
        // process at least once plus as many times as the user specifies
        for (var i = 0; i < this.repeatTimes; ++i) {
            // randomize bindings
            for (var j = 0; j < this.customVariables.Count; ++j) {
                var variable = this.customVariables[j];

                if (!variable.randomize) {
                    continue;
                }

                var type = variable.target.GetType ();
                var options = System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance;
                var field = type.GetField (variable.name, options);

                var randomization = this.GetRandomizationFor (variable);
                var value = randomization.GetValue (BlurryRoots.Randomizer.Randomizer.Global);
                field.SetValue (variable.target, value);
            }

            result.AddRange (processingStrategy ());
        }

        return result;
    }

    private List<GameObject> ProcessOwnChain (List<GameObject> input) {
        var chainLinks = new List<IChainLink<List<GameObject>>> ();
        // retrieve all modules currently associated with this game object
        this.GetComponents<IChainLink<List<GameObject>>> (chainLinks);

        // remove self from list - or else infinity awaits you
        chainLinks.Remove (this);

        // create new chain and link all modules
        var chain = new Chain<List<GameObject>> ();
        foreach (var c in chainLinks) {
            chain.Link (c);
        }

        // yield chain result
        return chain.Process (input);
    }

    private List<GameObject> ProcessChildChainHeads (List<GameObject> input) {
        var childCount = this.transform.childCount;

        // if this is a leaf, or empty chain. just bypass the input
        if (0 == childCount) {
            return input;
        }

        // else create a collective result list and process the chain heads
        var result = new List<GameObject> ();
        // get all chainheads in the next layer of children
        for (var i = 0; i < childCount; ++i) {
            var child = this.transform.GetChild (i);
            var head = child.GetComponent<ChainHead> ();

            if (null == head) {
                // no head attached
                continue;
            }

            var headResult = head.Process (input);
            result.AddRange (headResult);
        }

        // remove duplicate references to game objects in result
        return result.Distinct ().ToList ();
    }

    public void AddCustomVariable (string name, Object target, string viewName) {
        var variable = new VariableBinding () {
            name = name,
            target = target,
            viewName = viewName
        };

        this.customVariables.Add (variable);
        var index = this.customVariables.IndexOf (variable);
        try {
            var rand = RandomizationFactory.CreateFromBinding (variable);
            this.randomizations.Insert (index, rand);
        }
        catch (UnityException e) {
            var msg = new StringBuilder ()
                .Append ("No randomization implementation found for type ")
                .Append (variable.target.GetType ())
                .Append (". Skipping generation of randomization for ")
                .Append (variable.viewName)
                .Append (" (")
                .Append (variable.name)
                .Append (").")
                .ToString ()
                ;

            Debug.LogWarning (msg);
        }
    }

    public void RemoveVariable (VariableBinding variable) {
        var index = this.customVariables.IndexOf (variable);
        this.customVariables.Remove (variable);
        this.randomizations.RemoveAt (index);
    }

    public IVariableRandomization GetRandomizationFor (VariableBinding variable) {
        var varindex = this.customVariables.IndexOf (variable);

        return this.randomizations[varindex];
    }

    public void OnBeforeSerialize () {
        // unity is about to read the serializedNodes field's contents. lets make sure
        // we write out the correct data into that field "just in time".

        if (null == this.randomizations) {
            return;
        }

        this.serializableRandomizations = new List<VariableRandomizationSerializable> ();
        for (var i = 0; i < this.randomizations.Count; ++i) {
            var r = this.randomizations[i];
            var data = new UniversalBase64DeSerializer (r).Serialized;
            this.serializableRandomizations.Add (new VariableRandomizationSerializable () {
                index = i,
                data = data
            });
        }
    }

    public void OnAfterDeserialize () {
        // Unity has just written new data into the serializedNodes field.
        // let's populate our actual runtime data with those new values.
        if (null == this.serializableRandomizations) {
            return;
        }

        this.randomizations = new List<IVariableRandomization> ();
        for (var i = 0; i < this.serializableRandomizations.Count; ++i) {
            var s = this.serializableRandomizations[i];
            var r = new UniversalBase64DeSerializer (s.data).Data;
            this.randomizations.Insert (s.index, (IVariableRandomization)r);
        }
    }

    protected override void OnValueChanged () {
        this.repeatTimes = 1 > this.repeatTimes
            ? 1
            : this.repeatTimes
            ;
    }

    protected override void OnActivate (bool enabled) {
        if (enabled && null == this.customVariables) {
            this.customVariables = new List<VariableBinding> ();
        }
    }

    // TODO: Figure out if randomization info has to be stored seperatly to allow for proper serialization
    private List<IVariableRandomization> randomizations;

    [HideInInspector]
    [SerializeField]
    private List<VariableRandomizationSerializable> serializableRandomizations;

    [HideInInspector]
    [SerializeField]
    private List<VariableBinding> customVariables;

}

