using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BotSpawner), typeof(Scanner))]
public class Base : MonoBehaviour
{
    private List<Bot> _bots = new();

    private ResourceManager _resourceManager;
    private BotSpawner _botSpawner;
    private Scanner _scanner;

    private int _amountOfGold;

    public event Action<int> GoldChanged;

    private void Awake()
    {
        _botSpawner = GetComponent<BotSpawner>();
        _scanner = GetComponent<Scanner>();
    }

    private void OnDisable()
    {
        Cleanup();
    }

    public void Initialize(BotFactory botFactory, ResourceManager resourceManager)
    {
        _resourceManager = resourceManager;
        _botSpawner.Initialize(botFactory);

        InitBots();

        _scanner.ResourceDetected += _resourceManager.AddResource;
        _resourceManager.ResourceAvailable += OnResourceAvailable;
    }

    public void CollectResource(ICollectable resource)
    {
        _amountOfGold++;

        _resourceManager.RemoveResource(resource);

        GoldChanged?.Invoke(_amountOfGold);
    }

    private void InitBots()
    {
        _bots = _botSpawner.CreateStarterBots(transform.position);

        foreach (Bot bot in _bots)
        {
            bot.Freed += OnBotFreed;

            TrySetResource(bot);
        }
    }

    private void TrySetResource(Bot bot)
    {
        ICollectable resource = _resourceManager.GetResource();
        if (resource == null) return;

        bot.SetTarget(resource);
    }

    private void OnResourceAvailable()
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
        if (_resourceManager != null)
        {
            _scanner.ResourceDetected -= _resourceManager.AddResource;
            _resourceManager.ResourceAvailable -= OnResourceAvailable;
        }

        foreach (Bot bot in _bots)
        {
            bot.Freed -= OnBotFreed;
        }
    }
}
