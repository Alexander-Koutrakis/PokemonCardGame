using UnityEngine;
using System;
using PokemonTcgSdk.Models;

public class PokemonCardData
{
    public string ID { private set; get;}
    public string Name { private set; get;}
    public Type Type { private set; get;}
    public Rarity Rarity { private set; get;}
    public Sprite Sprite { private set; get;}
    public int HP { private set; get; }
 
    public PokemonCardData(PokemonCard pokemon)
    {
        ID = pokemon.Id;
        Name = pokemon.Name;
        HP = Int32.Parse(pokemon.Hp);
        Type = (Type)Enum.Parse(typeof(Type), pokemon.Types[0]);
        Rarity = pokemon.Rarity.StringToRarity();
    }

    public void SetSprite(Sprite sprite)
    {
        Sprite = sprite;
    }


}



public enum Type {Grass, Fighting, Darkness, Psychic, Metal, Lightning, Water, Fire, Colorless, Fairy }

public enum Rarity { Uncommon, Common,Rare, Legend }
