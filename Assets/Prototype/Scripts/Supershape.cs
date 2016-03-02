using UnityEngine;

public struct Supershape {

    public Vector2 CalculatePoint (float angle) {
        var radians = (angle / 360f) * 2 * Mathf.PI;

        var rpart = (this.m * radians) / 4f;
        var apart = Mathf.Abs (Mathf.Cos (rpart) / (float)this.a);
        var bpart = Mathf.Abs (Mathf.Sin (rpart) / (float)this.b);

        var r = Mathf.Pow (
          Mathf.Pow (apart, this.n2) + Mathf.Pow (bpart, this.n3),
          -1f / (float)this.n1
        );

        var x = Mathf.Cos (radians) * r;
        var y = Mathf.Sin (radians) * r;

        return new Vector2 (x, y);
    }

    public Supershape (int m, int n1, int n2, int n3, int a, int b) {
        this.m = m;

        this.n1 = n1;
        this.n2 = n2;
        this.n3 = n3;

        this.a = a;
        this.b = b;
    }

    private int m;

    private int n1;
    private int n2;
    private int n3;

    private int a;
    private int b;

}