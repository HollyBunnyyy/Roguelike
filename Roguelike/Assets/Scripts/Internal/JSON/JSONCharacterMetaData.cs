[System.Serializable]
public class JSONCharacterMetaData
{
    public int ID;
    public string Name;
    public string Sprite;
    public string Description;
    public int Heart;
    public int Ego;
    public int Dream;
    public int Flow;
    public int Pain;

    public JSONCharacterMetaData( 
        int id, string name, string sprite, string description, int heart, int ego, int dream, int flow, int pain )
    {
        this.ID          = id;
        this.Name        = name;
        this.Sprite      = sprite;
        this.Description = description;
        this.Heart       = heart;
        this.Ego         = ego;
        this.Dream       = dream;
        this.Flow        = flow;
        this.Pain        = pain;

    }
}
