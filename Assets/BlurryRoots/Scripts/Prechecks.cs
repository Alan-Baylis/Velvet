using System.Collections.Generic;

public static class Prechecks {
    
    public static bool IsNotDefault<TData> (params TData[] objs)
    where TData : class {
        foreach (var obj in objs) {
            if (EqualityComparer<TData>.Default.Equals (default (TData), obj)) {
                return false;
            }
        }

        return true;
    }

}

