using UnityEngine;
namespace ReferenceCount
{
    public class Example : MonoBehaviour
    {
        Room room=new Room();
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                room.EnterPeople();
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                room.LeavePeople();
            }
        }
    }   
}
