using System.ComponentModel;
using UnityEngine;

public class InventoryItem<T> where T : class, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private InventoryItem<T> _item;
    public InventoryItem<T> Item 
    { 
        get { return _item; }
        set 
        {
            _item = value;

            OnPropertyChanged();

        }
    
    }

    protected void OnPropertyChanged()
    {
        PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( "_item" ) );

    }

    /*
    public readonly int ID;

    public Item( int id )
    {
        this.ID = id;

    }
    */



}
