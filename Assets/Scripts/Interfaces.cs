using System.Collections;
using UnityEngine;

public interface ICardPlayable // When Cards are played
{
    IEnumerator Play();
}

public interface ICardEventDrawn // Drawn from deck
{
    IEnumerator Drawn();
}