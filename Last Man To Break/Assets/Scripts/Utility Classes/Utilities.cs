using System.Collections;
using System.Collections.Generic;

public class Utilities
{
    public static int Modulus(int a, int b) //for a%b
    {
        if(a<0)
            //return (Mathf.Abs(a * b) + a) % b;
            return ((a % b) + b) % b;
        
        return a%b;
    }
}
