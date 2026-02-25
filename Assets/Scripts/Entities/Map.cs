using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class Map
{
    public List<TilemapDetails> _listTileMapDetail { get; set; }

    public Map()
    {
    }

    public Map(List<TilemapDetails> listTileMapDetail)
    {
        this._listTileMapDetail = listTileMapDetail;
    }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }

    public int GetMapSize()
    {
        return _listTileMapDetail.Count;
    }
}
