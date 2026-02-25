using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class Map
{
    public List<TilemapDetails> map { get; set; }

    public Map()
    {
    }

    public Map(List<TilemapDetails> map)
    {
        this.map = map;
    }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }

    public int GetMapSize()
    {
        return map.Count;
    }
}
