
public class CustomRandomizerAttribute : System.Attribute {

    public readonly System.Type type;

    public CustomRandomizerAttribute (System.Type type) {
        this.type = type;
    }

}