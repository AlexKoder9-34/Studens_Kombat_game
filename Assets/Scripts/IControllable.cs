using UnityEngine;

public interface IControllable
{
     void Move(Vector3 controlDirection);
     void Jump();
     void Sprint();
}

