using System.Collections;
using System.Collections.Generic;
using PolyAndCode.UI;
using UnityEngine;

public class RecyclableInventoryManager : MonoBehaviour, IRecyclableScrollRectDataSource
{
    [SerializeField]
    RecyclableScrollRect _recyclableScrollRect;
    [SerializeField]
    private int _dataLength;

    private List<InvenItems> _inventItems = new List<InvenItems>();
    
    private void Awake()
    {

        _recyclableScrollRect.DataSource = this;
    }


    public int GetItemCount()
    {
        return _inventItems.Count;
    }

    public void SetCell(ICell cell, int index)
    {
   
        var item = cell as CellItemData;
        item.ConfigureCell(_inventItems[index], index);
    }

    private void Start()
    {
        List<InvenItems> invenItems = new List<InvenItems>();
        for(int i = 0; i < 50; i++)
        {
            invenItems.Add(new InvenItems() { name = "Item " + i, description = "Description for Item " + i });
        }

        SetListItems(invenItems);


    }

    public void SetListItems(List<InvenItems> invenItems)
    {
        _inventItems = invenItems;
        _recyclableScrollRect.ReloadData();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            InvenItems newItem = new InvenItems() { name = "New Item", description = "Description" };
            _inventItems.Add(newItem);
            _recyclableScrollRect.ReloadData();
        }
    }
}
