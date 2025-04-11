using UnityEngine;

public class BombGenerator : ObjectGenerator<Bomb>
{
    [SerializeField] private CubeGenerator _cubeGenerator;

    private void OnEnable()
    {
        _cubeGenerator.CubeDropped += AssignDropBombDelegate;
    }

    private void OnDisable()
    {
        _cubeGenerator.CubeDropped -= AssignDropBombDelegate;
    }

    private void AssignDropBombDelegate(PoolableObject cube)
    {
        cube.Deactivated += DropBomb;
        //cube.SetDeactivateInPositionDelegate(DropBomb);
    }

    public void DropBomb(PoolableObject cube)
    {
        cube.Deactivated -= DropBomb;

        Bomb bomb = Pool.Get();
        bomb.transform.position = cube.transform.position;
        bomb.Arm();
    }
}
