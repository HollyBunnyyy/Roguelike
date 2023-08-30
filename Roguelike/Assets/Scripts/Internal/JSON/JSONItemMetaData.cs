[System.Serializable]
public class JSONItemMetaData
{
    public int ID;
    public string Name;
    public string Sprite;
    public string Description;
    public int Rarity;

    public JSONItemMetaData( int ID, string Name, string Sprite, string Description, int Rarity )
    {
        this.ID             = ID;
        this.Name           = Name;
        this.Sprite         = Sprite;
        this.Description    = Description;
        this.Rarity         = Rarity;
    }
}
