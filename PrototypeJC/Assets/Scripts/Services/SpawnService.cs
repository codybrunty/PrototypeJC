using System.Collections.Generic;
using UnityEngine;

public class SpawnService {
    private readonly EnvironmentFactory environmentFactory;
    private readonly PathFactory pathFactory;

    public SpawnService(EnvironmentFactory environmentFactory, PathFactory pathFactory) {
        this.environmentFactory = environmentFactory;
        this.pathFactory = pathFactory;
    }

    public void SpawnLevel(LevelConfiguration config) {
        //environment
        GameObject env = environmentFactory.Create(config.env);

        //path
        List<Transform> path = pathFactory.Create(config.path);
        //send to whoever needs path
    }

}
