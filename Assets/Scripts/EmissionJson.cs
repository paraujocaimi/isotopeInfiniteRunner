[System.Serializable]
public class EmissionList {

    public Isotope[] isotopes;
}

[System.Serializable]
public class Isotope {
    public int Z;
    public string Atomic;
    public int A;
    public bool isStable;
}