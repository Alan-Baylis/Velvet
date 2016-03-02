[System.Serializable]
public class EditVariableBindingModel {

    public VariableBinding variable;
    public ChainHead head;

    public IVariableRandomization GetRandomization () {
        return this.head.GetRandomizationFor (this.variable);
    }

    public EditVariableBindingModel (ChainHead head, VariableBinding variable) {
        this.head = head;
        this.variable = variable;
    }

}