
public class CustomRandomizationViewAttribute : System.Attribute {

    public readonly System.Type type;

    public CustomRandomizationViewAttribute (System.Type type) {
        this.type = type;
    }

}
