using UnityEngine;

// TODO : Make GameManager class that inherits AssetManager - control logic will be for GameManager.
public class Roguelike : AssetManager 
{
    private static Roguelike _instance = null;
    public static Roguelike Instance
    {
        get 
        { 
            if( _instance == null )
            {
                if( !FindRoguelikeUniqueInstance( out Roguelike roguelikeInstance ) )
                {
                    GameObject singletonGameObject = new GameObject()
                    {
                        name = "Roguelike"

                    };

                    _instance = singletonGameObject.AddComponent<Roguelike>();

                } else {
                    _instance = roguelikeInstance;

                }

            }

            return _instance;

        }

    }

    public static bool FindRoguelikeUniqueInstance( out Roguelike roguelikeInstance )
    {
        roguelikeInstance = GameObject.FindObjectOfType<Roguelike>();

        return roguelikeInstance;

    }

}
