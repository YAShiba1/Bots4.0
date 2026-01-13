using System;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;

    private int _amountOfGold;

    private List<Bot> _bots = new();

    private BotFactory _botFactory;
    private Scanner _scanner;

    public event Action<int> GoldChanged;

    private void OnDisable()
    {
        Cleanup();
    }

    public void Initialize(BotFactory botFactory, Scanner scanner)
    {
        _botFactory = botFactory;
        _scanner = scanner;

        CreateStarterBots();

        _scanner.Scanned += OnResourceDetected;
    }

    public void CollectGold()
    {
        _amountOfGold++;

        GoldChanged?.Invoke(_amountOfGold);
    }

    private void CreateStarterBots()
    {
        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            Bot bot = _botFactory.Create(_spawnPoints[i]);
            bot.Initialize(_spawnPoints[i].position, transform.position);

            bot.Freed += OnBotFreed;
            _bots.Add(bot);

            TrySetResource(bot);
        }
    }

    private void TrySetResource(Bot bot)
    {
        ICollectable resource = _scanner.GetResource();
        if (resource == null) return;

        bot.SetTarget(resource);
    }

    private void OnResourceDetected()
    {
        Bot bot = FindFreeBot();
        if (bot == null) return;

        TrySetResource(bot);
    }

    private void OnBotFreed(Bot bot)
    {
        TrySetResource(bot);
    }

    private Bot FindFreeBot()
    {
        foreach (Bot bot in _bots)
        {
            if (bot.HasTarget == false)
            {
                return bot;
            }
        }

        return null;
    }

    private void Cleanup()
    {
        _scanner.Scanned -= OnResourceDetected;

        foreach (Bot bot in _bots)
        {
            bot.Freed -= OnBotFreed;
        }
    }
}
