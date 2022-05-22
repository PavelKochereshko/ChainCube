using UnityEngine;

public class Game : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CubeSpawner _spawner;
    [SerializeField] private CubeSling _sling;
    [SerializeField] private CubeCombiner _combiner;

    private bool _isGameOver = false;
    private int _spawnCount = 0;
    private Cube _currentCube;

    private void Start()
    {
        _sling.Detached += OnCubeDetach;
        _combiner.Combined += OnCubeCombined;
    }

    private void OnDisable()
    {
        _sling.Detached -= OnCubeDetach;
        _combiner.Combined -= OnCubeCombined;
    }

    public void StartGame()
    {
        SpawnNewCube();
    }

    public void GameOver()
    {
        _isGameOver = true;
        Destroy(_currentCube.gameObject);
    }

    private void OnCubeDetach(Cube cube)
    {
        cube.Collide += OnCubeCollide;

        SpawnNewCube();
    }

    private void SpawnNewCube()
    {
        if (_isGameOver)
            return;

        var cube = _currentCube = _spawner.SpawnRandom();
        _sling.Attach(cube);

        _spawnCount++;
        if (_spawnCount % 20 == 0)
        {
            GoogleAds.Instance.ShowBanner();
        }
    }

    private void OnCubeCollide(Cube cube1, Cube cube2)
    {
        _combiner.Combine(cube1, cube2);

        cube1.Collide -= OnCubeCollide;
        cube2.Collide -= OnCubeCollide;
    }

    private void OnCubeCombined(Cube cube)
    {
        cube.Collide += OnCubeCollide;
    }
}