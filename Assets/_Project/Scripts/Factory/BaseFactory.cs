using UnityEngine;

public class BaseFactory : Factory<Base>
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private BotFactory _botFactory;
    [SerializeField] private ResourceManager _resourceManager;

    private void Start()
    {
        Base newBase = Create(_spawnPoint);

        newBase.Initialize(_botFactory, _resourceManager);
    }
}
