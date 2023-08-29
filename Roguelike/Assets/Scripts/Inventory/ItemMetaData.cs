[System.Serializable]
public class ItemMetaData
{
    public int     ID;
    public string  Name;
    public string  Sprite;
    public string  Description;
    public int     Rarity;

    public ItemMetaData( int ID, string Name, string Sprite, string Description, int Rarity )
    {
        this.ID             = ID;
        this.Name           = Name;
        this.Sprite         = Sprite;
        this.Description    = Description;
        this.Rarity         = Rarity;
    }
}
