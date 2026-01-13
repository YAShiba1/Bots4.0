using UnityEngine;

public class BaseFactory : Factory<Base>
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private BotFactory _botFactory;
    [SerializeField] private Scanner _scanner;

    private void Start()
    {
        Base newBase = Create(_spawnPoint);

        newBase.Initialize(_botFactory, _scanner);
    }
}
