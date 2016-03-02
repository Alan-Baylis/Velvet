using System.Collections.Generic;

[System.Serializable]
public class ChainHeadModel {

    public IEnumerator<VariableBinding> bindings;

    public ChainHeadModel (IEnumerator<VariableBinding> bindings) {
        this.bindings = bindings;
    }

}