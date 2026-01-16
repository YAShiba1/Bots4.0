using System.Collections.Generic;
using UnityEngine;

public class BotSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;

    private BotFactory _botFactory;

    public void Initialize(BotFactory botFactory)
    {
        _botFactory = botFactory;
    }

    public List<Bot> CreateStarterBots(Vector3 basePosition)
    {
        List<Bot> bots = new List<Bot>();

        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            Bot bot = _botFactory.Create(_spawnPoints[i]);
            bot.Initialize(_spawnPoints[i].position, basePosition);

            bots.Add(bot);
        }

        return bots;
    }
}
