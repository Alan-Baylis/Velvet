using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class BindingListModel : IEnumerable<VariableBinding> {

    public bool foldoutOpen;

    public bool HasEnumerator () {
        return null != this.bindings;
    }

    public IEnumerator<VariableBinding> GetEnumerator () {
        return this.bindings;
    }

    IEnumerator IEnumerable.GetEnumerator () {
        return this.GetEnumerator ();
    }

    public BindingListModel (IEnumerator<VariableBinding> bindings) {
        this.bindings = bindings;
        this.foldoutOpen = true;
    }

    private IEnumerator<VariableBinding> bindings;

}
