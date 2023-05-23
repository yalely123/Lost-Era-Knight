using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyRoom : Room
{
    // inherit room class to make empty room/ no door room  can add in rooms list of type Room
    // this class will do nothing

    protected override void Start() {  }

    protected override void Update() {  }

    public override string GetName()
    {
        return "wall";
    }
}
