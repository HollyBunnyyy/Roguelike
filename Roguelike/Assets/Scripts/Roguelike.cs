using UnityEngine;

public class Roguelike : MonoBehaviour
{



    private static Roguelike _instance = null;
    public static Roguelike Instance
    {
        get 
        { 
            if( _instance == null )
            {
                if( FindObjectsOfType<Roguelike>() == null )
                {
                    GameObject singletonGameObject = new GameObject()
                    {
                        name = "Roguelike"
                        
                    };

                    _instance = singletonGameObject.AddComponent<Roguelike>();

                }
            }

            return _instance;

        }

    }


}
